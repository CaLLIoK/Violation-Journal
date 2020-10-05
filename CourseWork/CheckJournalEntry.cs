using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Journal
{
    static class CheckJournalEntry
    {
        public static string CheckEnrtyDate (string str)
        {
            if (str == string.Empty)
            {
                return "Дата не указана.";
            }
            else
            {
                DateTime fromDateValue;
                var formats = new[] { "dd-MM-yyyy" };
                if (!DateTime.TryParseExact(str, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue))
                {
                    return "Вы ввели неверную дату. Дата имеет следующий формат: 'дд-мм-гггг'";
                }
            }
            return str;
        }

        public static string CheckEntryTime (string str)
        {
            if (str == string.Empty)
            {
                return "Время не указано.";
            }
            else
            {
                if (!Regex.IsMatch(str, @"((0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9])$"))
                {
                    return "Введено неверное время.";
                }
            }
            return str;
        }

        public static string CheckPaymentStatus (string str)
        {
            if (str == string.Empty)
            {
                return "Статус оплаты не указан.";
            }
            else
            {
                if (str != "Оплачено" && str != "Не оплачено")
                {
                    return "Вы указали неверный статус. Он может быть 'Оплачено' или 'Не оплачено'.";
                }
            }
            return str;
        }
    }
}