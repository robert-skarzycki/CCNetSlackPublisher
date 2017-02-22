using ThoughtWorks.CruiseControl.Core;

namespace SlackPublisher.Messaging
{
    internal class MessageComposer : IMessageComposer
    {
        public string CreateMessage(IIntegrationResult result)
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
