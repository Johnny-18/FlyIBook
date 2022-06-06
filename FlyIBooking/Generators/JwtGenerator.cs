using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FlyIBooking.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace FlyIBooking.Generators
{
    public sealed class JwtGenerator
    {
        public string? GenerateJwt(AccountDto dto, string securityKey)
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, dto.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

            var jwt = new JwtSecurityToken(
                notBefore: DateTime.UtcNow,
                claims: identity.Claims,
                expires: DateTime.UtcNow.AddDays(1d),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}