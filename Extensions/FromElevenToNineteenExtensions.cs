namespace _10._08._2026_dz
{
    // розширення для підключення middleware (обробка чисел 11–19)
    public static class FromElevenToNineteenExtensions
    {
        public static IApplicationBuilder UseFromElevenToNineteen(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromElevenToNineteenMiddleware>();
        }
    }



}