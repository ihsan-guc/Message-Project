using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Message.Api.Core
{
    public interface ITokenGenerator
    {
        string Token(string userName, string email);
    }
    public class TokenGenerator : ITokenGenerator
    {
        public string Token(string userName, string email)
        {
            var token = new JwtSecurityToken(
                issuer: "Message Test",
                audience: userName,
                expires: DateTime.Now,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(email + "Test Verisidir")),
                    SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
