using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionsApp.Models
{
    public class AppSettings
    {
        public OAuthServer OAuthServers { get; set; }
    }

    public class OAuthServer
    {
        public ThirdPartyOAuth QQ { get; set; }

        public ThirdPartyOAuth WeChat { get; set; }

        public ThirdPartyOAuth Weibo { get; set; }
    }

    public class ThirdPartyOAuth
    {
        public string OAuthUrl { get; set; }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

    }
}
