using Exortech.NetReflector;
using SlackPublisher.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtWorks.CruiseControl.Remote;

namespace ThoughtWorks.CruiseControl.Core.Publishers
{
    [ReflectorType("slackPublisher")]
    public class SlackPublisher : ITask
    {
        private IMessageSender messageSender;

        public SlackPublisher() : this(new HttpPostMessageSender()) { }

        internal SlackPublisher(IMessageSender messageSender)
        {
            this.messageSender = messageSender;
        }

        [ReflectorProperty("webhookUrl")]
        public string WebhookUrl { get; set; }

        [ReflectorProperty("statuses")]
        public string RawStatusesList { get; set; }

        private IEnumerable<IntegrationStatus> StatusesToConsume => this.GetStatusesToConsume();        

        public void Run(IIntegrationResult result)
        {
            if (!this.IsConfigurationValid())
                return;

            if (!this.StatusesToConsume.Contains(result.Status))
                return;

            var message = this.FormatText(result);

            var payload = new Payload(message);
            this.messageSender.Send(this.WebhookUrl, payload);
        }

        private IEnumerable<IntegrationStatus> GetStatusesToConsume()
        {
            if (string.IsNullOrEmpty(this.RawStatusesList))                            
                yield break;
            
            var splitStatuses = this.RawStatusesList.Split(';');

            foreach (var rawStatus in splitStatuses)
            {
                if (Enum.IsDefined(typeof(IntegrationStatus), rawStatus))
                {
                    yield return (IntegrationStatus)Enum.Parse(typeof(IntegrationStatus), rawStatus);
                }
            }
        }

        private bool IsConfigurationValid()
        {
            return !string.IsNullOrEmpty(this.WebhookUrl);
        }

        private string FormatText(IIntegrationResult result)
        {
            return string.Format("<{0}|{1}> {2} {3} {4}",
                result.ProjectUrl,
                result.ProjectName,
                result.Label,
                result.Status,
                result.Succeeded ? ":white_check_mark:" : ":interrobang:");
        }
    }
}
