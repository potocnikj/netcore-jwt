using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class JwtService
    {

        private readonly HMACSHA256 _signature;
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {   
            _configuration = configuration;
            _signature = new HMACSHA256(Encoding.UTF8.GetBytes(_configuration["HashSecret"]));
        }

        public string Encode(string payload)
        {
            var cleanToken = this._configuration["TokenHeader"] + "." + Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(payload));
            
            return cleanToken + "." + Convert.ToBase64String(this._signature.ComputeHash(Encoding.UTF8.GetBytes(cleanToken)));
        }

        public string Decode(string jwtToken)
        {
            return jwtToken;
        }
    }
}