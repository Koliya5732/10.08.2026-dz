namespace _10._08._2026_dz
{
    public class FromHundredToThousandMiddleware
    {
        private readonly RequestDelegate _next;

        public FromHundredToThousandMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string? token = context.Request.Query["number"];

            if (!int.TryParse(token, out int number))
            {
                await _next(context);
                return;
            }

            number = Math.Abs(number);

            if (number < 101 || number > 999)
            {
                await _next(context);
                return;
            }

            string[] hundreds =
            {
                "сто",
                "двісті",
                "триста",
                "чотириста",
                "п'ятсот",
                "шістсот",
                "сімсот",
                "вісімсот",
                "дев'ятсот"
            };
            int hundred = number / 100;
            int remainder = number % 100;

            string result = hundreds[hundred - 1];

            if (remainder == 0)
            {
                context.Response.ContentType =
                    "text/plain; charset=utf-8";

                await context.Response.WriteAsync(
                    $"Ваше число - {result}"
                );

                return;
            }

            context.Session.SetString("hundreds", result);
            context.Session.SetString("remainder", remainder.ToString());

            await _next(context);

            string? remainderResult =
                context.Session.GetString("finalNumberPart");

            if (remainderResult == null)
            {
                remainderResult = result;
            }

            context.Session.SetString(
                "finalNumberPart",
                result + " " + remainderResult
            );
        }
    }
}