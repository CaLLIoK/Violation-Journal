using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Journal
{
    static class CheckCar
    {
        public static string CheckCarStateNumber (string str)
        {
            if (str == string.Empty)
            {
                return "Вы не ввели номер автомобиля";
            }
            else
            {
                if (str.Length == 8)
                {
                    if (!Regex.IsMatch(str, @"\d{4}(А|В|Е|І|К|М|Н|О|Р|С|Т|Х){2}\-[1-7]"))
                    {
                        return "Вы неверно указали номер. Он должен выглядеть следующим образом: 4444СТ-7";
                    }
                }
                else
                {
                    return "Допустимая длина номера - 8 символов, включая '-'.";
                }
            }
            return str;
        }

        public static string CheckCarColor(string str)
        {
            if (str == string.Empty)
            {
                return "Вы не ввели цвет автомобиля";
            }
            else
            {
                if (str.Length > 3 && str.Length <= 50)
                {
                    char[] colorArray = str.ToCharArray();
                    for (int i = 0; i < colorArray.Length; i++)
                    {
                        if (!char.IsLetter(colorArray[i]) && colorArray[i] != '-')
                        {
                            return "Вы указали в названии цвета автомобиля недопустимые символы.";
                        }
                    }
                }
                else
                {
                    return "Допустимая длина цвета автомобиля 4-50 символов.";
                }
            }
            return str;
        }

        public static string CheckCarModel (string str)
        {
            if (str == string.Empty)
            {
                return "Вы не ввели марку автомобиля";
            }
            else
            {
                if (str.Length > 1 && str.Length <= 50)
                {
                    char[] carModelArray = str.ToCharArray();
                    for (int i = 0; i < carModelArray.Length; i++)
                    {
                        if (!char.IsLetter(carModelArray[i]) && carModelArray[i] != '-')
                        {
                            return "Вы указали в названии марки автомобиля недопустимые символы.";
                        }
                    }
                }
                else
                {
                    return "Допустимая длина марки автомобиля 2-50 символов.";
                }
            }
            return str;
        }
    }
}