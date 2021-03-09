using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;  
using System.Collections.Generic;  
using System.Linq;
using System.Text;
using System.Threading.Tasks;  
  
namespace TNPLCommerce.Domain.Models
{
    public class SiteKeys
    {
        private static IConfigurationSection _configuration;
        public static void Configure(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public static string Key => _configuration["SecretKey"];
        public static string Issuer => _configuration["Issuer"];
        public static string Audience => _configuration["Audience"];

    }
}