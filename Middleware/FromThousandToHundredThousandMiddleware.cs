namespace _10._08._2026_dz
{
    public class FromThousandToHundredThousandMiddleware
    {
        private readonly RequestDelegate _next;

        public FromThousandToHundredThousandMiddleware(RequestDelegate next)
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

            // Это middleware обрабатывает только 1000–100000
            if (number < 1000 || number > 100000)
            {
                await _next(context);
                return;
            }

            context.Response.ContentType =
                "text/plain; charset=utf-8";

            // 100000
            if (number == 100000)
            {
                await context.Response.WriteAsync(
                    "Ваше число - сто тисяч"
                );

                return;
            }

            int thousands = number / 1000;
            int remainder = number % 1000;

            string result = ConvertThousands(thousands);

            // Если после тысяч ничего нет
            if (remainder == 0)
            {
                await context.Response.WriteAsync(
                    $"Ваше число - {result}"
                );

                return;
            }

            // Добавляем сотни/десятки/единицы
            result += " " + ConvertBelowThousand(remainder);

            await context.Response.WriteAsync(
                $"Ваше число - {result}"
            );
        }

        private string ConvertThousands(int number)
        {
            string[] ones =
            {
                "",
                "одна",
                "дві",
                "три",
                "чотири",
                "п'ять",
                "шість",
                "сім",
                "вісім",
                "дев'ять"
            };

            string[] teens =
            {
                "десять",
                "одинадцять",
                "дванадцять",
                "тринадцять",
                "чотирнадцять",
                "п'ятнадцять",
                "шістнадцять",
                "сімнадцять",
                "вісімнадцять",
                "дев'ятнадцять"
            };

            string[] tens =
            {
                "",
                "",
                "двадцять",
                "тридцять",
                "сорок",
                "п'ятдесят",
                "шістдесят",
                "сімдесят",
                "вісімдесят",
                "дев'яносто"
            };

            string result;

            if (number < 10)
            {
                result = ones[number];
            }
            else if (number < 20)
            {
                result = teens[number - 10];
            }
            else
            {
                int ten = number / 10;
                int one = number % 10;

                result = tens[ten];

                if (one > 0)
                {
                    result += " " + ones[one];
                }
            }

            // Выбираем правильную форму "тисяча"
            int lastTwo = number % 100;
            int last = number % 10;

            string thousandWord;

            if (lastTwo >= 11 && lastTwo <= 19)
            {
                thousandWord = "тисяч";
            }
            else if (last == 1)
            {
                thousandWord = "тисяча";
            }
            else if (last >= 2 && last <= 4)
            {
                thousandWord = "тисячі";
            }
            else
            {
                thousandWord = "тисяч";
            }

            return result + " " + thousandWord;
        }

        private string ConvertBelowThousand(int number)
        {
            string[] ones =
            {
                "",
                "один",
                "два",
                "три",
                "чотири",
                "п'ять",
                "шість",
                "сім",
                "вісім",
                "дев'ять"
            };

            string[] teens =
            {
                "десять",
                "одинадцять",
                "дванадцять",
                "тринадцять",
                "чотирнадцять",
                "п'ятнадцять",
                "шістнадцять",
                "сімнадцять",
                "вісімнадцять",
                "дев'ятнадцять"
            };

            string[] tens =
            {
                "",
                "",
                "двадцять",
                "тридцять",
                "сорок",
                "п'ятдесят",
                "шістдесят",
                "сімдесят",
                "вісімдесят",
                "дев'яносто"
            };

            string[] hundreds =
            {
                "",
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

            string result = "";

            int hundred = number / 100;
            int remainder = number % 100;

            if (hundred > 0)
            {
                result += hundreds[hundred];
            }

            if (remainder == 0)
            {
                return result;
            }

            if (result != "")
            {
                result += " ";
            }

            if (remainder < 10)
            {
                result += ones[remainder];
            }
            else if (remainder < 20)
            {
                result += teens[remainder - 10];
            }
            else
            {
                int ten = remainder / 10;
                int one = remainder % 10;

                result += tens[ten];

                if (one > 0)
                {
                    result += " " + ones[one];
                }
            }

            return result;
        }
    }
}