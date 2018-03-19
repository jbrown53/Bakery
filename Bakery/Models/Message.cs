using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bakery.Models
{
    public class Message
    {
        public string MessageText { get; set; }
        public Message() { }
        public Message(string msgText)
        {
            MessageText = msgText;
        }
    }
}