using System;
using System.Collections.Generic;
using System.Text;

namespace AVA.ForBrain.ApplicationCore.AppSetting
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string Credentials { get; set; }
        public string Passoword { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string SendGridApiKey { get; set; }
        public string SendGridFrom { get; set; }
    }
    public class AppSettingsSecret
    {
        public string Secret { get; set; }
    }
}
