using Heliconia.Domain;
using Heliconia.Domain.UsersEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Application.Shared
{
    public class Access
    {
        internal static async Task VerifyAccess<T>(List<Claim> claims, IRepository repository,
           ISecurity security, IUtility utility) where T : User
        {
            //obtenemos claims
            var id = security.GetClaim(claims, ISecurity.USERID);
            var mail = security.GetClaim(claims, ISecurity.MAIL);
            var jti = security.GetClaim(claims, ISecurity.JTI);
            var rol = security.GetClaim(claims, ISecurity.ROLE);

            T user;

            //verificamos si el usuario existe en el web config en el caso de que sea un heliconia
            //verificamos que exista en la db y que sea del rol heliconia
            if (rol == "HeliconiaUser" && utility.CheckValueInlistJson("heliconiasMailsJson", mail) == false)
                throw new Exception("Usuario no tiene permisos administrativos");

            else if (repository.Exists<T>(x => x.Id.ToString() == id) == false)
                throw new Exception("Usuario aun no se ha creado");

            else if (rol != typeof(T).Name)
                throw new Exception("El rol no coincide con el token");

            //verificar si el token corresponde
            user = await repository.Get<T>(x => x.Id.ToString() == id);

            if (security.GetClaim(user.Token, "jti") != jti)
                throw new Exception("El token de acceso no es correcto");

        }

        internal static bool IsUserType<T>(List<Claim> claims, ISecurity security) where T : User
        {
            var isHeliconiaUser = false;
            var rol = security.GetClaim(claims, ISecurity.ROLE);

            if (rol == typeof(T).Name)
                isHeliconiaUser = true;

            return isHeliconiaUser;
        }

        /// <summary>
        /// verificamos el acceso de todos los usuarios, uno por uno
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="repository"></param>
        /// <param name="security"></param>
        /// <param name="utility"></param>
        /// <returns></returns>
        internal static async Task CheckAccessToAll(List<Claim> claims, IRepository repository, 
            ISecurity security, IUtility utility)
        {
            //verificar usuario
            if (Access.IsUserType<HeliconiaUser>(claims, security) is true)
                await Access.VerifyAccess<HeliconiaUser>(claims, repository, security, utility);
            else if (Access.IsUserType<Manager>(claims, security) is true)
                await Access.VerifyAccess<Manager>(claims, repository, security, utility);
            else if (Access.IsUserType<Worker>(claims, security) is true)
                await Access.VerifyAccess<Worker>(claims, repository, security, utility);
        }
    }
}
