using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Journal
{
    static class CheckViolator
    {
        public static string CheckViolatorName (string str)
        {
            if (str == string.Empty)
            {
                return "Вы не ввели имя";
            }
            else
            {
                if (str.Length > 1 && str.Length <= 30)
                {
                    char[] nameArray = str.ToCharArray();
                    for (int i = 0; i < nameArray.Length; i++)
                    {
                        if (!char.IsLetter(nameArray[i]))
                        {
                            return "Вы указали в имени недопустимые символы.";
                        }
                    }
                }
                else
                {
                    return "Допустимая длина имени 2-30 символов.";
                }
            }
            return str;
        }

        public static string CheckViolatorSurname (string str)
        {
            if (str == string.Empty)
            {
                return "Вы не ввели фамилию";
            }
            else
            {
                if (str.Length > 1 && str.Length <= 30)
                {
                    char[] surnnameArray = str.ToCharArray();
                    for (int i = 0; i < surnnameArray.Length; i++)
                    {
                        if (!char.IsLetter(surnnameArray[i]))
                        {
                            return "Вы указали в фамилии недопустимые символы.";
                        }
                    }
                }
                else
                {
                    return "Допустимая длина фамилии 2-30 символов.";
                }
            }
            return str;
        }

        public static string CheckViolatorPatronymic(string str)
        {
            if (str == string.Empty)
            {
                return "Вы не ввели отчество";
            }
            else
            {
                if (str.Length > 1 && str.Length <= 30)
                {
                    char[] patronymicArray = str.ToCharArray();
                    for (int i = 0; i < patronymicArray.Length; i++)
                    {
                        if (!char.IsLetter(patronymicArray[i]))
                        {
                            return "Вы указали в отчестве недопустимые символы.";
                        }
                    }
                }
                else
                {
                    return "Допустимая длина отчества 2-30 символов.";
                }
            }
            return str;
        }

        public static string CheckViolatorPasportNumber (string str)
        {
            if (str == string.Empty)
            {
                return "Вы не ввели номер паспорта";
            }
            else
            {
                if (str.Length == 9)
                {
                    if (!Regex.IsMatch(str, @"([AB|BM|HB|KH|MP|MC|KB]\d{7})"))
                    {
                        return "Вы указали в номере паспорта недопустимые символы.";
                    }
                }
                else
                {
                    return "Номер паспорта должен состоять из двух букв и 7-ми цифр.";
                }
            }
            return str;
        }

        public static string CheckViolatorPhoneNumber (string str)
        {
            if (str == string.Empty)
            {
                return "Вы не ввели номер телефона";
            }
            else
            {
                if (str.Length == 17)
                {
                    if (!Regex.IsMatch(str.ToString(), @"(\+|)(375|)(\ |)(\(|)(29|25|33|44)\)\d{3}\-\d{2}\-\d{2}"))
                    {
                        return "Вы указали в номере телефона недопустимые символы.";
                    }
                }
                else
                {
                    return "Номер телефона должен выглядеть следующим образом: '+375(25/29/33/44)***-**-**'.";
                }
            }
            return str;
        }

        public static string CheckViolatorHouseNumber (string str)
        {
            if (str == string.Empty)
            {
                return "Вы не ввели номер дома";
            }
            else
            {
                char[] phoneArray = str.ToCharArray();
                for (int i = 0; i < phoneArray.Length; i++)
                {
                    if (!char.IsDigit(phoneArray[i]))
                    {
                        return "Вы указали в номере дома недопустимые символы.";
                    }
                }
                if (Convert.ToInt32(str) < 1 || Convert.ToInt32(str) > 200)
                {
                    return "Дома с таким номером не существует.";
                }
            }
            return str;
        }

        public static string CheckViolatorApartment (string str)
        {
            if (str == string.Empty)
            {
                return "Вы не ввели номер квартиры";
            }
            else
            {
                char[] apartmentArray = str.ToCharArray();
                for (int i = 0; i < apartmentArray.Length; i++)
                {
                    if (!char.IsDigit(apartmentArray[i]))
                    {
                        return "Вы указали в номере квартиры недопустимые символы.";
                    }
                }
                if (Convert.ToInt32(str) < 0 || Convert.ToInt32(str) > 500)
                {
                    return "Квартиры с таким номером не существует.";
                }
            }
            return str;
        }

        public static string CheckViolatorTown(string str)
        {
            if (str == string.Empty)
            {
                return "Вы не ввели название города";
            }
            else
            {
                if (str.Length > 1 && str.Length <= 30)
                {
                    char[] nameArray = str.ToCharArray();
                    for (int i = 0; i < nameArray.Length; i++)
                    {
                        if (!char.IsLetter(nameArray[i]))
                        {
                            return "Вы указали в названии города недопустимые символы.";
                        }
                    }
                }
                else
                {
                    return "Допустимая длина названия города 2-30 символов.";
                }
            }
            return str;
        }

        public static string CheckViolatorStreet(string str)
        {
            if (str == string.Empty)
            {
                return "Вы не ввели название улицы";
            }
            else
            {
                if (str.Length > 1 && str.Length <= 30)
                {
                    char[] nameArray = str.ToCharArray();
                    for (int i = 0; i < nameArray.Length; i++)
                    {
                        if (!char.IsLetter(nameArray[i]))
                        {
                            return "Вы указали в названии улицы недопустимые символы.";
                        }
                    }
                }
                else
                {
                    return "Допустимая длина названия улицы 2-30 символов.";
                }
            }
            return str;
        }
    }
}