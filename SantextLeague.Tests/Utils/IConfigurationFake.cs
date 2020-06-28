using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace SantextLeague.Tests.Utils
{
    public class IConfigurationFake
    {
        public static IConfiguration Get() 
        {
            var configurationMock = Substitute.For<IConfiguration>();
            configurationMock["ApiToken"] = "010bad0e5a2243afb6bd7f43bb0f7da0";
            return configurationMock;
        }
    }
}
