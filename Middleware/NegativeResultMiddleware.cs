namespace _10._08._2026_dz
{
    public class NegativeResultMiddleware
    {
        private readonly RequestDelegate _next;

        public NegativeResultMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Запоминаем оригинальный поток ответа
            var originalBody = context.Response.Body;

            using var memoryStream = new MemoryStream();

            // Временно направляем ответ в память
            context.Response.Body = memoryStream;

            await _next(context);

            // Получаем готовый текст
            memoryStream.Seek(0, SeekOrigin.Begin);

            using var reader = new StreamReader(memoryStream);

            string responseText = await reader.ReadToEndAsync();

            // Возвращаем оригинальный поток
            context.Response.Body = originalBody;

            // Проверяем, было ли число отрицательным
            string? negative = context.Session.GetString("negative");

            if (negative == "true" &&
                !string.IsNullOrWhiteSpace(responseText))
            {
                responseText = responseText.Replace(
                    "Ваше число - ",
                    "Ваше число - мінус "
                );
            }

            await context.Response.WriteAsync(responseText);
        }
    }
}