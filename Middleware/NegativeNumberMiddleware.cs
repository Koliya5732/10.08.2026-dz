namespace _10._08._2026_dz
{
    public class NegativeNumberMiddleware
    {
        private readonly RequestDelegate _next;

        public NegativeNumberMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string? token = context.Request.Query["number"];

            if (int.TryParse(token, out int number))
            {
                if (number < 0)
                {
                    context.Session.SetString("negative", "true");
                }
                else
                {
                    context.Session.Remove("negative");
                }
            }

            await _next(context);
        }
    }
}