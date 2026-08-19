namespace _10._08._2026_dz
{
    public static class NegativeResultExtensions
    {
        public static IApplicationBuilder UseNegativeResult(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<NegativeResultMiddleware>();
        }
    }
}