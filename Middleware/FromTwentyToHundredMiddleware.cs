namespace _10._08._2026_dz
{
    public class FromTwentyToHundredMiddleware
    {
        private readonly RequestDelegate _next;

        public FromTwentyToHundredMiddleware(RequestDelegate next)
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

            // Проверяем, пришли ли мы сюда от middleware сотен
            string? remainderString = context.Session.GetString("remainder");

            if (remainderString != null &&
                int.TryParse(remainderString, out int remainder))
            {
                number = remainder;
            }

            // Числа меньше 20 обрабатываются следующими middleware
            if (number < 20)
            {
                await _next(context);
                return;
            }

            // Числа больше 100 здесь не обрабатываем
            if (number > 100)
            {
                await _next(context);
                return;
            }

            string[] tens =
            {
                "двадцять",
                "тридцять",
                "сорок",
                "п'ятдесят",
                "шістдесят",
                "сімдесят",
                "вісімдесят",
                "дев'яносто"
            };

            // Ровные десятки: 20, 30, 40...
            if (number % 10 == 0)
            {
                string result = tens[number / 10 - 2];

                // Если есть сотни — добавляем их
                string? hundreds = context.Session.GetString("hundreds");

                if (hundreds != null)
                {
                    result = hundreds + " " + result;
                }

                context.Response.ContentType =
                    "text/plain; charset=utf-8";

                await context.Response.WriteAsync(
                    $"Ваше число - {result}"
                );

                return;
            }

            // Сохраняем десятки для дальнейшей обработки единиц
            context.Session.SetString(
                "tens",
                tens[number / 10 - 2]
            );

            await _next(context);

            // Получаем единицы
            string? units = context.Session.GetString("number");

            string finalResult = tens[number / 10 - 2];

            if (units != null)
            {
                finalResult += " " + units;
            }

            // Получаем сотни
            string? hundredsResult =
                context.Session.GetString("hundreds");

            if (hundredsResult != null)
            {
                finalResult =
                    hundredsResult + " " + finalResult;
            }

            context.Response.ContentType =
                "text/plain; charset=utf-8";

            await context.Response.WriteAsync(
                $"Ваше число - {finalResult}"
            );
        }
    }
}