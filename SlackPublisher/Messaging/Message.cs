using System.Collections.Generic;

namespace SlackPublisher.Messaging
{
    internal class Message
    {
        public string ProjectUrl { get; set; }
        public string ProjectName { get; set; }
        public string Label { get; set; }
        public string Status { get; set; }
        public bool Succeeded { get; set; }
        public IEnumerable<MergeRequest> SuspectedMergeRequests { get; set; }

        public override string ToString()
        {
            return string.Format("<{0}|{1}> {2} {3} {4}",
                this.ProjectUrl,
                this.ProjectName,
                this.Label,
                this.Status,
                this.Succeeded ? ":white_check_mark:" : ":interrobang:");
        }
    }
}
