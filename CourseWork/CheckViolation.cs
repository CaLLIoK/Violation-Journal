using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal
{
    static class CheckViolation
    {
        public static string CheckViolationName (string str)
        {
            if (str == string.Empty)
            {
                return "Вы не ввели название автонарушения.";
            }
            else
            {
                if (str.Length > 5 && str.Length < 81)
                {
                    char[] violationName = str.ToCharArray();
                    for (int i = 0; i < violationName.Length; i++)
                    {
                        if (!char.IsLetter(violationName[i]) && !char.IsDigit(violationName[i]) && violationName[i] != '/' && violationName[i] != ' ' && violationName[i] != '(' && violationName[i] != ')' && violationName[i] != '.')
                        {
                            return "Вы указали в названии автонарушения недопустимые символы.";
                        }
                    }
                }
                else
                {
                    return "Допустимая длина названия штрафа 6-80 символов.";
                }
            }
            return str;
        }

        public static string CheckViolationCost(string str)
        {
            if (str == string.Empty)
            {
                return "Вы не ввели сумму штрафа.";
            }
            else
            {
                char[] costArray = str.ToCharArray();
                for (int i = 0; i < costArray.Length; i++)
                {
                    if ((!char.IsDigit(costArray[i]) && costArray[i] != ','))
                    {
                        return "Вы указали в сумме штрафа недопустимые символы.";
                    }
                }
                if (Convert.ToDouble(str) < 0 || Convert.ToDouble(str) > 5000)
                {
                    return "Введена неверная сумма штрафа. Нужно ввести от 1 до 5000.";
                }
            }
            return str;
        }
    }
}