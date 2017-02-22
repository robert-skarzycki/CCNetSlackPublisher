using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlackPublisher.Messaging
{
    internal interface IMessageSender
    {
        void Send(string url, Payload payload);
    }
}
