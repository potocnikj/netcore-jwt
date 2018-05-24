using System;
using System.Net.Mime;
using Microsoft.IdentityModel.Protocols;

namespace Services
{
    public class JwtService
    {
        private static string header = "ewAiAHQAeQBwACIAOgAgACIASgBXAFQAIgAsACIAYQBsAGcAIgA6ACIASABTADIANQA2ACIAfQA=";
        
        public string Encode(string payload)
        {
            var tknPayload = Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(payload));
            
            return header + "." + tknPayload;
        }

        public string Decode()
        {
            return "test";
        }
    }
}