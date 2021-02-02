using System;
using System.IO;
using OCR_School_Web_App.
using Mic;

namespace Custom
{
    static class ConfigurationManager: 
    {
        public static IConfiguration AppSetting { get; }
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("YouAppSettingFile.json")
                    .Build();
        }
    }
}
