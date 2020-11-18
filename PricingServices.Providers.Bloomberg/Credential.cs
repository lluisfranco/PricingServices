using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg
{
    public class Credential
    {
        // 1970-Jan-01 00:00:00 UTC
        private static readonly DateTimeOffset UnixEpoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);

        // accout region, use 'default' value unless being instructed otherwise
        private const string d_region = "default";

        // Once created each token will be valid only for the following specified period(in seconds).
        // This value is ajustable on client side, but note that increasing this value too much 
        // might not suffice for security concerns, as well as too little value could lead to 
        // JWT token invalidation before it being received by the beap server due to network delays.
        // You can adjust this value for your need or use default value unless you definitely know 
        // that you need to change this value.
        private const int d_lifetime = 25;

        /// <summary>
        /// Provides access to client id parsed from credential.txt file.
        /// </summary>
        [JsonPropertyNameAttribute("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Accepts client secret form json deserializer and decodes it to bytes.
        /// </summary>
        [JsonPropertyNameAttribute("client_secret")]
        public string ClientSecret { set { DecodedSecret = FromHexString(value); } }

        /// <summary>
        /// Provides access to decoded client secret.
        /// </summary>
        public byte[] DecodedSecret { get; private set; }

        /// <summary>
        /// Accepts the expiration date from a json deserializer and converts it to a convenient representation.
        /// </summary>
        /// <value>The expiration date as a timestamp in milliseconds.</value>
        [JsonPropertyNameAttribute("expiration_date")]
        public long ExpirationDate { set { ExpiresAt = UnixEpoch.AddMilliseconds(value); } }

        /// <summary>
        /// Provides access to the expiration time.
        /// </summary>
        /// <value>The expiration date.</value>
        public DateTimeOffset ExpiresAt { get; private set; }

        /// <summary>
        /// Converts hexadecimal string to bytes.
        /// </summary>
        /// <param name="s">Input hexadecimal string.</param>
        /// <returns></returns>
        static private byte[] FromHexString(string input)
        {
            return Enumerable.Range(0, input.Length)
                     .Where(charIdx => charIdx % 2 == 0)
                     .Select(charIdx => Convert.ToByte(input.Substring(charIdx, 2), 16))
                     .ToArray();
        }

        /// <summary>
        /// Loads credentials from credential.txt file.
        /// </summary>
        /// <param name="logger"> An object to be used to log messages. </param>
        /// <param name="credentialPath">Path to credential.json file.</param>
        /// <returns></returns>
        internal static Credential LoadCredential(ILogger logger, string credentialPath = "credential.json")
        {
            try
            {
                var jsonInput = File.ReadAllText(credentialPath);
                var clientCredential = JsonSerializer.Deserialize<Credential>(jsonInput);
                string clientSecret = Convert.ToBase64String(clientCredential.DecodedSecret);

                logger.Information("client id: {clientId}", clientCredential.ClientId);

                var now = DateTimeOffset.UtcNow;
                var expiresIn = clientCredential.ExpiresAt - now;
                if (clientCredential.ExpiresAt < now)
                {
                    logger.Warning("Credentials expired {expiresIn} days ago", -expiresIn.Days);
                }
                else if (clientCredential.ExpiresAt < now.AddMonths(1))
                {
                    logger.Warning("Credentials expiring in {expiresIn} days", expiresIn.Days);
                }

                return clientCredential;

            }
            catch (JsonException error)
            {
                logger.Fatal("Cannot read credential file, probably not in JSON format: {error}", error.Message);
            }
            catch (UnauthorizedAccessException)
            {
                logger.Fatal("Cannot access credential file, check file permissions.");
            }
            catch (ArgumentException error)
            {
                logger.Fatal("Cannot load credentials: {error}", error.Message);
            }
            catch (IOException error)
            {
                logger.Fatal("Cannot open credential file: {error}", error.Message);
            }
            Environment.Exit(-1);
            return null;
        }

        /// <summary>
        /// Creates new JWT token for the given input request parameters.
        /// </summary>
        /// <param name="host">the beap host being accessed.</param>
        /// <param name="path">URI path of the accessed endpoint.</param>
        /// <param name="method">HTTP method used to access the endpoint.</param>
        /// <returns></returns>
        public string CreateToken(string host, string path, string method)
        {
            string guid = Guid.NewGuid().ToString();
            // Get unix timestamp
            var issueTime = (long)(DateTimeOffset.UtcNow - UnixEpoch).TotalSeconds;
            // Create key for JWT signature
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(DecodedSecret);
            // Define JWT signing key and algorythm
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Create JWT header container object
            var header = new JwtHeader(signingCredentials);
            // Create JWT payload container object
            var payload = new JwtPayload
            {
                { JwtRegisteredClaimNames.Iss, ClientId },
                { JwtRegisteredClaimNames.Iat, issueTime },
                { JwtRegisteredClaimNames.Nbf, issueTime },
                { JwtRegisteredClaimNames.Exp, issueTime + d_lifetime },
                { "host", host },
                { "path", path },
                { "region", d_region },
                { "jti", guid },
                { "method", method },
                { "client_id", ClientId }
            };
            // Create JWT token object
            JwtSecurityToken jwtToken = new JwtSecurityToken(header, payload);
            // Serialize JWT token object to base64 encoded string
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }

}
