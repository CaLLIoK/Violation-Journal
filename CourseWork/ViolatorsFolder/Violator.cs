using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Journal
{
    [Table("Violator")]
    public class Violator
    {
        private string violatorName;
        private string violatorSurname;
        private string violatorPatronymic;
        private string violatorPasportNumber;
        private string violatorPhoneNumber;
        private int violatorHouseNumber;
        private int violatorApartmentNumber;

        public Violator()  { }

        public Violator(string violatorName, string violatorSurname, string violatorPatronymic, string violatorPasportNumber, string violatorPhoneNumber, int violatorHouseNumber, int violatorApartmentNumber, int violatorTownCode, int violatorStreetCode)
        {
            ViolatorName = violatorName;
            ViolatorSurname = violatorSurname;
            ViolatorPatronymic = violatorPatronymic;
            ViolatorPasportNumber = violatorPasportNumber;
            ViolatorPhoneNumber = violatorPhoneNumber;
            ViolatorHouseNumber = violatorHouseNumber;
            ViolatorApartmentNumber = violatorApartmentNumber;
            ViolatorTownCode = violatorTownCode;
            ViolatorStreetCode = violatorStreetCode;
        }

        [Key]
        public int ViolatorCode { get; set; }
        public string ViolatorName
        {
            get => violatorName;
            set
            {
                try
                {
                    if (value == string.Empty)
                    {
                        throw new Exception("Вы не ввели имя");
                    }
                    else
                    {
                        if (value.Length > 2 && value.Length <= 30)
                        {
                            char[] nameArray = value.ToCharArray();
                            for (int i = 0; i < nameArray.Length; i++)
                            {
                                if (!char.IsLetter(nameArray[i]))
                                {
                                    throw new Exception("Вы указали в имени недопустимые символы.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Допустимая длина имени 3-30 символов.");
                        }
                    }
                    violatorName = value;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
            }
        }
        public string ViolatorSurname
        {
            get => violatorSurname;
            set
            {
                try
                {
                    if (value == string.Empty)
                    {
                        throw new Exception("Вы не ввели фамилию");
                    }
                    else
                    {
                        if (value.Length > 2 && value.Length <= 30)
                        {
                            char[] surnnameArray = value.ToCharArray();
                            for (int i = 0; i < surnnameArray.Length; i++)
                            {
                                if (!char.IsLetter(surnnameArray[i]))
                                {
                                    throw new Exception("Вы указали в фамилию недопустимые символы.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Допустимая длина фамилии 3-30 символов.");
                        }
                    }
                    violatorSurname = value;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
            }
        }
        public string ViolatorPatronymic
        {
            get => violatorPatronymic;
            set
            {
                try
                {
                    if (value == string.Empty)
                    {
                        throw new Exception("Вы не ввели отчество");
                    }
                    else
                    {
                        if (value.Length > 2 && value.Length <= 30)
                        {
                            char[] surnnameArray = value.ToCharArray();
                            for (int i = 0; i < surnnameArray.Length; i++)
                            {
                                if (!char.IsLetter(surnnameArray[i]))
                                {
                                    throw new Exception("Вы указали в отчестве недопустимые символы.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Допустимая длина отчества 3-30 символов.");
                        }
                    }
                    violatorPatronymic = value;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
            }
        }

        public string ViolatorPasportNumber
        {
            get => violatorPasportNumber;
            set
            {
                try
                {
                    if (value == string.Empty)
                    {
                        throw new Exception("Вы не ввели номер паспорта");
                    }
                    else
                    {
                        if (value.Length == 9)
                        {
                            if (!Regex.IsMatch(value.ToString(), @"([AB|BM|HB|KH|MP|MC|KB]\d{7})"))
                            {
                                throw new Exception("Вы указали в номере паспорта недопустимые символы.");
                            }
                        }
                        else
                        {
                            throw new Exception("Номер паспорта должен состоять из двух букв и 7-ми цифр.");
                        }
                    }
                    violatorPasportNumber = value;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
            }
        }

        public string ViolatorPhoneNumber
        {
            get => violatorPhoneNumber;
            set
            {
                try
                {
                    if (value == string.Empty)
                    {
                        throw new Exception("Вы не ввели номер телефона");
                    }
                    else
                    {
                        if (value.Length == 17)
                        {
                            if (!Regex.IsMatch(value.ToString(), @"(\+|)(375|)(\ |)(\(|)[29|25|33|44]\)\d{3}\-\d{2}\-\d{2}"))
                            {
                                throw new Exception("Вы указали в номере телефона недопустимые символы.");
                            }
                        }
                        else
                        {
                            throw new Exception("Номер телефона должен выглядеть следующим образом: '+375(25/29/33/44)***-**-**'.");
                        }
                    }
                    violatorPhoneNumber = value;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
            }
        }

        public int ViolatorHouseNumber
        {
            get => violatorHouseNumber;
            set
            {
                try
                {
                    if (value.ToString() == string.Empty)
                    {
                        throw new Exception("Вы не ввели номер дома");
                    }
                    else
                    {
                        if (value > 0 && value < 200)
                        {
                            char[] phoneArray = value.ToString().ToCharArray();
                            for (int i = 0; i < phoneArray.Length; i++)
                            {
                                if (!char.IsDigit(phoneArray[i]))
                                {
                                    throw new Exception("Вы указали в номере дома недопустимые символы.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Дома с таким номером не существует.");
                        }
                    }
                    violatorHouseNumber = value;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
            }
        }

        public int ViolatorApartmentNumber
        {
            get => violatorApartmentNumber;
            set
            {
                try
                {
                    if (value.ToString() == string.Empty)
                    {
                        throw new Exception("Вы не ввели номер квартиры");
                    }
                    else
                    {
                        if (value > 0 && value < 500)
                        {
                            char[] apartmentArray = value.ToString().ToCharArray();
                            for (int i = 0; i < apartmentArray.Length; i++)
                            {
                                if (!char.IsDigit(apartmentArray[i]))
                                {
                                    throw new Exception("Вы указали в номере квартиры недопустимые символы.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Квартиры с таким номером не существует.");
                        }
                    }
                    violatorApartmentNumber = value;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
            }
        }

        public int ViolatorTownCode { get; set; }
        public int ViolatorStreetCode { get; set; }
    }
}