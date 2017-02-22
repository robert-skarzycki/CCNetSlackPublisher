using ThoughtWorks.CruiseControl.Core;

namespace SlackPublisher.Messaging
{
    interface IMessageComposer
    {
        Message CreateMessage(IIntegrationResult result);
    }
}
