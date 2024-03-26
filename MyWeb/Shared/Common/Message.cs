namespace Shared.Common
{
    public class Message
    {
        public Message() {
            Error = false;
        }
        public bool Error { get; set; }
        public string Title { get; set; }
    }
}