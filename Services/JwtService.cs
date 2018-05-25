using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Rest.TransientFaultHandling;

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
            var cleanToken = this._configuration["TokenHeader"] + "." +
                             Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(payload));

            return cleanToken + "." +
                   Convert.ToBase64String(this._signature.ComputeHash(Encoding.UTF8.GetBytes(cleanToken)));
        }

        public string Decode(string jwtToken)
        {
            string[] splitToken = jwtToken.Split('.');
            
            if (splitToken.Length != 3)
            {
                throw new HttpRequestWithStatusException("token contains an invalid number of segments");
            }
            
            var payloadHash =
                Convert.ToBase64String(
                    this._signature.ComputeHash(Encoding.UTF8.GetBytes(splitToken[0] + "." + splitToken[1])));

            if (payloadHash != splitToken[2])
            {
                throw new Exception("Signature check has failed");
            }
            

            return Encoding.UTF8.GetString(Convert.FromBase64String(splitToken[1]));
        }
    }
}