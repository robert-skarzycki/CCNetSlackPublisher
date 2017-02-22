using Moq;
using NUnit.Framework;
using SlackPublisher.Messaging;
using ThoughtWorks.CruiseControl.Core;
using ThoughtWorks.CruiseControl.Remote;

namespace SlackPublisher.Tests.Unit
{
    [TestFixture]
    public class SlackPublisherTests
    {
        [Test]
        public void WhenStatusConfiguredToBeConsumedThenSendsMessage()
        {
            var status = IntegrationStatus.Failure;

            var mockResult = new Mock<IIntegrationResult>();
            mockResult.SetupGet(m => m.Status).Returns(status);

            var messageSender = new Mock<IMessageSender>();

            var sut = this.GetSut(messageSender.Object);
            sut.RawStatusesList = status.ToString();

            sut.Run(mockResult.Object);

            messageSender.Verify(m => m.Send(It.IsAny<string>(), It.IsAny<Payload>()));
        }

        [Test]
        public void WhenStatusNotConfiguredToBeConsumedThenDoesntSendMessage()
        {
            var status = IntegrationStatus.Failure;

            var mockResult = new Mock<IIntegrationResult>();
            mockResult.SetupGet(m => m.Status).Returns(status);

            var messageSender = new Mock<IMessageSender>(MockBehavior.Strict);

            var sut = this.GetSut(messageSender.Object);
            sut.RawStatusesList = "SomeOtherStatus";

            sut.Run(mockResult.Object);            
        }

        private ThoughtWorks.CruiseControl.Core.Publishers.SlackPublisher GetSut(IMessageSender fakeMessageSender)
        {
            var messageSender = fakeMessageSender ?? new Mock<IMessageSender>().Object;
            var messageComposer = new Mock<IMessageComposer>();
            messageComposer.Setup<Messaging.Message>(m => m.CreateMessage(It.IsAny<IIntegrationResult>())).Returns(new Messaging.Message());

            return new ThoughtWorks.CruiseControl.Core.Publishers.SlackPublisher(messageSender, messageComposer.Object)
            {
                WebhookUrl = "some url"
            };
        }
    }
}
