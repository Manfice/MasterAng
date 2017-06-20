using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Web.Model;
using Web.Model.Users;

namespace Web.Infrastructure
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtProvider
    {
        private readonly RequestDelegate _next;

        private TimeSpan _tokenExpiration;
        private readonly SigningCredentials _signingCredentials;
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        private const string PrivateKey = "private_key_1234567890";
        public static readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(PrivateKey));
        public static readonly string Issuer = "iMaster";
        public static string TockenEndPoint = "/api/connect/tocken";


        public JwtProvider(RequestDelegate next, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _next = next;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenExpiration = TimeSpan.FromMinutes(10);
            _signingCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Path.Equals(TockenEndPoint, StringComparison.Ordinal))
            {
                return _next(httpContext);
            }

            if (httpContext.Request.Method.Equals("post", StringComparison.CurrentCultureIgnoreCase) &&
                httpContext.Request.HasFormContentType)
            {
                return CreateTocken(httpContext);
            }
            httpContext.Response.StatusCode = 400;
            return httpContext.Response.WriteAsync("Bad request.");
        }

        private async Task CreateTocken(HttpContext httpContext)
        {
            var ctx = httpContext.Request;
            try
            {
                var username = ctx.Form["username"];
                var password = ctx.Form["password"];

                var user = username.Contains("@") ? await _userManager.FindByEmailAsync(username): await _userManager.FindByNameAsync(username);

                var success = user!=null && await _userManager.CheckPasswordAsync(user, password);
                if (success)
                {
                    var dtNow = DateTime.UtcNow;
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Iss, Issuer), 
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id), 
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(dtNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
                    };
                    var token = new JwtSecurityToken(claims: claims, notBefore: dtNow, expires: dtNow.Add(_tokenExpiration), signingCredentials: _signingCredentials);
                    var encToken = new JwtSecurityTokenHandler().WriteToken(token);
                    var jwt = new {access_token = encToken, expiration = (int) _tokenExpiration.TotalSeconds};
                    httpContext.Response.ContentType = "application/json";
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(jwt));
                }
            }
            catch (Exception)
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Invalid username / password");
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JwtProviderExtensions
    {
        public static IApplicationBuilder UseJwtProvider(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtProvider>();
        }
    }
}
