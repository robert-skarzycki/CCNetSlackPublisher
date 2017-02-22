namespace SlackPublisher.Messaging
{
    public class HttpPostMessageSender : IMessageSender
    {
        //http://www.hanselman.com/blog/HTTPPOSTsAndHTTPGETsWithWebClientAndCAndFakingAPostBack.aspx

        public void Send(string URI, Payload payload)
        {
            var Parameters = payload.ToJson();

            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            req.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            //Add these, as we're doing a POST
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
            req.ContentLength = bytes.Length;
            using (System.IO.Stream os = req.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length); //Push it out there
                os.Close();
                using (System.Net.WebResponse resp = req.GetResponse())
                using (var sr = new System.IO.StreamReader(resp.GetResponseStream()))
                {
                    sr.ReadToEnd().Trim();
                }
            }
        }
    }
}
