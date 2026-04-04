using System.Text;
using System.Text.Json;
using System.Security.Cryptography;

namespace LowLevelDotNET.Security
{
    public static class SimpleJWT
    {
        public static string CreateToken(string secret, object payload)
        {
            //Header
            var header = new {alg = "HS256", typ = "JWT"};
            string headerJson = JsonSerializer.Serialize(header);
            string headerBase64 = Base64UrlEncode(Encoding.UTF8.GetBytes(headerJson));

            //Payload
            string payloadJson = JsonSerializer.Serialize(payload);
            string payloadBase64 = Base64UrlEncode(Encoding.UTF8.GetBytes(payloadJson));

            //Signature
            string unsignedToken = $"{headerBase64}.{payloadBase64}";
            string signature = ComputeHMACSHA256(unsignedToken, secret);

            return $"{unsignedToken}.{signature}";
        }

        //-------
        // Helpers
        //-------
        private static string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }

         private static string ComputeHMACSHA256(string data, string key)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return Base64UrlEncode(hash);
        }
    }
}