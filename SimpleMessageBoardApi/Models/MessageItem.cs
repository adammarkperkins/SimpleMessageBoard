using System;

#nullable disable

namespace SimpleMessageBoardApi.Models
{
    public partial class MessageItem
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
        public DateTime Added { get; set; }
    }
}
