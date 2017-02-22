using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThoughtWorks.CruiseControl.Core;

namespace SlackPublisher.Messaging
{
    interface IMessageComposer
    {
        string CreateMessage(IIntegrationResult result);
    }
}
