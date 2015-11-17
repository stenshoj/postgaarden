using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgaardenMail.Dummy
{
    public class Mail
    {
        public string UserName { get; set; } = "mhhsec3@gmail.com";
        public string Password { get; set; } = "Znote031";
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Signature { get; set; }
    }
}
