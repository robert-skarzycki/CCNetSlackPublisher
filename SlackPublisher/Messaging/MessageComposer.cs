using ThoughtWorks.CruiseControl.Core;

namespace SlackPublisher.Messaging
{
    internal class MessageComposer : IMessageComposer
    {
        public Message CreateMessage(IIntegrationResult result)
        {
            return new Message
            {
                Label = result.Label,
                ProjectName = result.ProjectName,
                ProjectUrl = result.ProjectUrl,
                Status = result.Status.ToString(),
                Succeeded = result.Succeeded
            };
        }
    }
}
