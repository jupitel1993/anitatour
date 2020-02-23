using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using API.Common.Extensions;
using API.Settings;
using Application.Common.Interfaces;
using Domain.Constants;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace API.Services
{
    public class TokenService : ITokenService
    {

        private readonly AppConfiguration _appConfiguration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IDistributedCache _distributedCache;

        public TokenService(IOptions<AppConfiguration> appConfiguration, IHttpContextAccessor httpContextAccessor, IDistributedCache distributedCache)
        {
            _appConfiguration = appConfiguration.Value;
            _httpContextAccessor = httpContextAccessor;
            _distributedCache = distributedCache;
        }

        private ClaimsPrincipal User => _httpContextAccessor?.HttpContext?.User;

        public int GetUserId() => User.GetId();
        
        public (string, DateTime) GetSecurityToken(int id, string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appConfiguration.Audience.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim(CustomClaimTypes.Id, id.ToString()),
                    new Claim(CustomClaimTypes.UserName, username),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_appConfiguration.Audience.TokenExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appConfiguration.Audience.Iss,
                Audience = _appConfiguration.Audience.Aud,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return (tokenHandler.WriteToken(token), token.ValidTo);
        }


        public string GetUserName() => User.GetUserName();

        public async Task<bool> IsCurrentTokenActive() => await IsTokenActiveAsync(GetCurrentTokenAsync());

        public async Task DeactivateCurrentTokenAsync() => await DeactivateTokenAsync(GetCurrentTokenAsync());

        private async Task<bool> IsTokenActiveAsync(string token) => await _distributedCache.GetStringAsync(GetKey(token)) == null;

        private async Task DeactivateTokenAsync(string token)
            => await _distributedCache.SetStringAsync(GetKey(token),
                " ", new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow =
                        TimeSpan.FromMinutes(_appConfiguration.Audience.TokenExpiryMinutes)
                });

        private string GetCurrentTokenAsync()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }
        
        private static string GetKey(string token) => $"tokens:{token}:deactivated";
    }
}
