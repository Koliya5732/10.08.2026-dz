    namespace _10._08._2026_dz
    {
        public static class FromHundredToThousandExtensions
        {
            public static IApplicationBuilder UseFromHundredToThousand(
                this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<FromHundredToThousandMiddleware>();
            }
        }
    }

    



 