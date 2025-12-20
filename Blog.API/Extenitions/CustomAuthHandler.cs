using Blog.Infrastructure.ExternalService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Blog.API.Extenitions
{
    public class CustomAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthClient _authClient;

        public CustomAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock,IAuthClient authClient) : base(options, logger, encoder, clock)
        {
            _authClient = authClient;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(token)) return AuthenticateResult.Fail("Unauthorized");
            var user = await _authClient.Getme(token);
            if (user is null) return AuthenticateResult.Fail("Invalid Token");
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("EmailVerifiy",user.Verified.ToString()),
                new Claim("DisplayName",user.DisplayName),

            };
            var userRoles = user.Roles;
            foreach(var role in userRoles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role,role.ToLower()));
            }
            var identity = new ClaimsIdentity(AuthClaims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
