using System;
using System.Collections.Generic;
using System.Text;

namespace SQS.Listener.Lambda
{
    public class AppSettings
    {
        public string ForwardingUrl { get; private set; }
    }
}
