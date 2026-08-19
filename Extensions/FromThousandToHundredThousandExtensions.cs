namespace _10._08._2026_dz
{
    public static class FromThousandToHundredThousandExtensions
    {
        public static IApplicationBuilder UseFromThousandToHundredThousand(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromThousandToHundredThousandMiddleware>();
        }
    }
}