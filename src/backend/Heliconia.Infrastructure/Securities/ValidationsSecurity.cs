using Heliconia.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Infrastructure.Securities
{
    internal class ValidationsSecurity
    {
        internal static readonly Action<string>[] checksPassword =
        {
            (x) =>
            {
                var isCorrect = (x != null & x != "");
                if(!isCorrect)
                    throw new Exception("CONTRASEÑA");
            },

            (x) =>
            {
                var isCorrect = (x.Any(x => char.IsUpper(x)) &&  x.Any(x => char.IsLower(x)) &&
                                    x.Any(x => char.IsDigit(x)));

                if(!isCorrect)
                    throw new Exception("CARACTERES CONTRASEÑA");
            },

            (x) =>
            {
                var isCorrect = (x.Length >= 9);
                if(!isCorrect)
                    throw new Exception("LONGITUD CARACTERES");
            }
        };

        internal static readonly Action<string>[] checksClaims =
        {
            (x) =>
            {
                var isCorrect = x != null && x!= "";
                if(!isCorrect)
                    throw new Exception("Claims NULOS");
            },

            (x) =>
            {
                var isCorrect = (x == ISecurity.MAIL || x == ISecurity.USERNAME ||
                                 x == ISecurity.USERID || x == ISecurity.JTI || x == ISecurity.ROLE);
                if(!isCorrect)
                    throw new Exception("claims incorrectos");
            }
        };

        internal static readonly Action<string>[] checksToken =
        {
            (x) =>
            {
                var isCorrect = x != null && x!= "";
                if(!isCorrect)
                    throw new Exception("TOKEN NULO");
            },
        };

        internal static void Validator(string parameter, params Action<string>[] validations)
        {
            foreach (var validation in validations)
            {
                validation(parameter);
            }
        }
    }
}
