using Blog.Infrastructure.ExternalService;

namespace Blog.API.Middelwares
{
    public class AuthMiddelware
    {
        private readonly RequestDelegate _next;

        public AuthMiddelware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext Context,IAuthClient authClient)
        {
            var token = Context.Request.Headers["Authorization"].FirstOrDefault().Split(" ").LastOrDefault();
            if (string.IsNullOrEmpty(token))
            {
                Context.Response.StatusCode = 401;
                await Context.Response.WriteAsync("Unauthorized");
                return;
            }
            var userInfo = await authClient.Getme(token);
            if (userInfo is null)
            {
                Context.Response.StatusCode = 401;
                await Context.Response.WriteAsync("Invalid Token");
                return;
            }
            Context.Items["User"] = userInfo;
            await _next(Context);
        }
    }
}
