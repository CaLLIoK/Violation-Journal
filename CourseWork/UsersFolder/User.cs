using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserCode { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserPatronymic { get; set; }
        public string UserPhoneNumber { get; set; }

        public User() { }

        public User(string userLogin, string userPassword, string userName, string userSurname, string userPatronymic, string userPhoneNumber)
        {
            UserLogin = userLogin;
            UserPassword = userPassword;
            UserName = userName;
            UserSurname = userSurname;
            UserPatronymic = userPatronymic;
            UserPhoneNumber = userPhoneNumber;
        }
    }
}
