using PeaLearning.Common.Configurations;
using PeaLearning.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeaLearning.Common
{
    public static class StaticVariable
    {
        public static string StaticVersion = DateTime.Now.Ticks.ToString();
        public static string BaseUrl = AppSettings.Get<string>("SiteConfigs:BaseUrl");
        public static string BaseUrlNoSlash = AppSettings.Get<string>("SiteConfigs:BaseUrlNoSlash");
        public static string Domain = AppSettings.Get<string>("SiteConfigs:Domain");
        public static string DomainImage = AppSettings.Get<string>("SiteConfigs:DomainImage");

        public static MomoConfiguration MomoConfigs = AppSettings.Get<MomoConfiguration>("MomoConfigs");


    }
}
