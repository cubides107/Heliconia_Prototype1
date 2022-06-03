using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Domain.UsersEntities
{
    public abstract class User : Entity
    {
        public string Name { get; private set; }

        public string Lasname { get; private set; }

        public string IdentificationDocument { get; private set; }

        public string Mail { get; private set; }

        public string EncryptedPassword { get; private set; }

        public string Token { get; private set; }

        public string CellPhoneNumber { get; private set; }

        /// <summary>
        /// for ef
        /// </summary>
        protected User(): base()
        {

        }

        protected User(string name, string lasname, string identificationDocument, string mail, 
            string encryptedPassword, string cellPhoneNumber):base()
        {
            Name = Guard.Against.NullOrEmpty(name, 
                nameof(name), "Nombre es obligatorio");

            Lasname = Guard.Against.NullOrEmpty(lasname, 
                nameof(lasname), "Apellido es obligatorio");

            IdentificationDocument = Guard.Against.NullOrEmpty(identificationDocument, 
                nameof(identificationDocument), "Identidad es obligatorio");

            Mail = Guard.Against.NullOrEmpty(mail, 
                nameof(mail), "correo obligatorio");

            EncryptedPassword = Guard.Against.NullOrEmpty(encryptedPassword, 
                nameof(encryptedPassword), "contraseña no encriptada");

            CellPhoneNumber = Guard.Against.NullOrEmpty(cellPhoneNumber, 
                nameof(cellPhoneNumber), "Telefono obligatorio");

            Token = string.Empty;
        }


        public void ChangeMainAttributes(string name, string lasname, string identificationDocument, string cellPhoneNumber)
        {
            Name = Guard.Against.NullOrEmpty(name,
                nameof(name), "Nombre es obligatorio");

            Lasname = Guard.Against.NullOrEmpty(lasname,
                nameof(lasname), "Apellido es obligatorio");

            IdentificationDocument = Guard.Against.NullOrEmpty(identificationDocument,
                nameof(identificationDocument), "Identidad es obligatorio");

            CellPhoneNumber = Guard.Against.NullOrEmpty(cellPhoneNumber,
                nameof(cellPhoneNumber), "Telefono obligatorio");
        }

        public void Login(string token)
        {
            Token = Guard.Against.NullOrEmpty(token, nameof(token),
                "No hay session");
        }

        public void Logout()
        {
            Token = string.Empty;
        }
    }
}
