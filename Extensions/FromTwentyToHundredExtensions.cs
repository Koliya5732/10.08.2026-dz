namespace _10._08._2026_dz
{
    // розширювальний метод для підключення middleware у конвеєр обробки запитів
    // цей клас має бути у тій же області імен, що й middleware, щоб розширювальний метод був видимий
    // !!! зазвичай такі класи розміщують у окремих файлах, але для простоти хай буде тут
    // цей клас підключає middleware для обробки чисел від 20 до 100
    // саме він дозволяє використовувати метод UseFromTwentyToHundred у Program.cs
    // без нього довелось би писати app.UseMiddleware<...>() - нудно і громіздко
    public static class FromTwentyToHundredExtensions
    {
        // розширювальний метод для IApplicationBuilder, який підключає наш middleware у конвеєр
        // якщо підзабули, що таке розширювальний метод - гляньте https://gist.github.com/sunmeat/75d1693cb6e23e7979c8701b116718c1
        public static IApplicationBuilder UseFromTwentyToHundred(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromTwentyToHundredMiddleware>();
        }
    }

    // middleware для обробки чисел від 20 до 100, запускається першим у конвеєрі
    
            
        
    
}