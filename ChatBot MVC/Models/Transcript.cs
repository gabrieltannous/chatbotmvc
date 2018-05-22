using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatBot_MVC.Models
{
    // an object to hold an individual message sent by either the bot or the user
    public class Transcript
    {
        public string Name { get; set; }
        public string Message { get; set; }
    }
}