using Exortech.NetReflector;
using SlackPublisher;

namespace ThoughtWorks.CruiseControl.Core.Publishers
{
    [ReflectorType("slackPublisher")]
    public class SlackPublisher : ITask
    {
        [ReflectorProperty("webhookUrl")]
        public string WebhookUrl { get; set; }        

        public void Run(IIntegrationResult result)
        {
            if (!this.IsConfigurationValid())
                return;

            var message = this.FormatText(result);

            var payload = new Payload(message);
            HttpPostHelper.HttpPost(WebhookUrl, payload.ToJson());
        }

        private bool IsConfigurationValid()
        {
            return !string.IsNullOrEmpty(WebhookUrl);
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
