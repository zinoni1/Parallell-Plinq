using System;
using System.Net;

namespace Parallel_Plinq
{
    public class Usuari
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }

        public Usuari(string name, string dni, string mail)
        {
            Name = name;
            Dni = dni;
            Email = mail;

           
        }
        public bool ComprovaDNI()
        {
            if (string.IsNullOrEmpty(Dni) || Dni.Length != 9)
                return false;

            string digits = Dni.Substring(0, 8);
            string lletra = Dni.Substring(8, 1);

            int n;
            if (!int.TryParse(digits, out n))
                return false;

            string lletres = "TRWAGMYFPDXBNJZSQVHLCKE";
            int index = n % 23;

            return lletra == lletres.Substring(index, 1);
        }

        public bool ComprovaNom()
        {
            return !string.IsNullOrEmpty(Name);
        }

        public bool ComprovaMail()
        {
            if (string.IsNullOrEmpty(Email) || !Email.Contains("@"))
                return false;

            if (Email.IndexOf('@') != Email.LastIndexOf('@') || Email.IndexOf('@') == 0 || Email.IndexOf('@') == Email.Length - 1)
                return false;

            return true;
        }
    }
}
