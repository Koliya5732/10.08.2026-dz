namespace _10._08._2026_dz
{
    public static class NegativeNumberExtensions
    {
        public static IApplicationBuilder UseNegativeNumber(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<NegativeNumberMiddleware>();
        }
    }
}