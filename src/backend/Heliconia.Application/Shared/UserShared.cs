using Heliconia.Domain;
using Heliconia.Domain.UsersEntities;
using System;
using System.Threading.Tasks;

namespace Heliconia.Application.Shared
{
    public class UserShared
    {
        /// <summary>
        /// Modifica y Actualiza en db los atributos de un usuario de Tipo T 
        /// </summary>
        /// <typeparam name="T">Tipo de usuario al cual se le van a modificar los atributos basicos puede ser Tipo User, es decir Heliconia, Manager o Worker</typeparam>
        /// <param name="repository"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="identificationDocument"></param>
        /// <param name="cellPhone"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        internal static async Task changeMainAtributes<T>(IRepository repository, string id, string name, string lastName, string identificationDocument, string cellPhone) where T : User
        {
            T user;

            //Verificar que el usuario a editar se encuentre registrado
            if (!repository.Exists<T>(x => x.Id.ToString() == id))
                throw new Exception("El usuario a editar no se encuentra registrado");

            //Obtener el usuario a editar
            user = await repository.Get<T>(x => x.Id.ToString() == id);

            //Cambiar los atributos del usuario
            user.ChangeMainAttributes(name, lastName, identificationDocument, cellPhone);

            //Actualizar el usuario en la db
            repository.Update(user);
            await repository.Commit();
        }

        /// <summary>
        /// Permite que un usuario de Tipo T Modifique a un usuario de Tipo U
        /// </summary>
        /// <typeparam name="T">Tipo de usuario que realiza la modificacion de atributos puede ser tipo User, es decir Heliconia, Manager</typeparam>
        /// <typeparam name="U">Tipo de usuario al cual se le realiza la modificacion de atributos puede ser de tipo User, es decir Heliconia, Manager, Worker</typeparam>
        /// <param name="request"></param>
        /// <param name="security"></param>
        /// <param name="repository"></param>
        /// <param name="utility"></param>
        /// <returns></returns>
        internal static async Task ModifyUser<T, U>(UserCommandShared request, ISecurity security, IRepository repository, IUtility utility) where T : User where U : User
        {
            if (Access.IsUserType<T>(request.claims, security))
            {
                await Access.VerifyAccess<T>(request.claims, repository, security, utility);
                await changeMainAtributes<U>(
                    repository: repository,
                    id: request.dataUser.id,
                    name: request.dataUser.Name,
                    lastName: request.dataUser.LastName,
                    identificationDocument: request.dataUser.IdentificationDocument,
                    cellPhone: request.dataUser.Phone);
            }
        }

        /// <summary>
        /// Permite la modificacion de los datos de un usuario de Tipo T 
        /// </summary>
        /// <typeparam name="T">Tipo de usuario al cual se le van a modificar los datos puede ser de tipo User es decir Heliconia, Manager o Worker</typeparam>
        /// <param name="request"></param>
        /// <param name="security"></param>
        /// <param name="repository"></param>
        /// <param name="utility"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        internal static async Task ModifyUser<T>(UserCommandShared request, ISecurity security, IRepository repository, IUtility utility) where T : User
        {
            if (Access.IsUserType<T>(request.claims, security))
            {
                string id;

                //Verificar Acceso del usuario que realiza la peticion
                await Access.VerifyAccess<T>(request.claims, repository, security, utility);

                //Obtener id del usuario que realiza la peticion y comprobar que sea igual al usuario a modificar
                id = security.GetClaim(request.claims, ISecurity.USERID);

                if (request.dataUser.id != id)
                    throw new Exception("El usuario solo se puede modificar asi mismo");

                //Modificar atributos del usuario de Tipo T
                await changeMainAtributes<T>(
                    repository: repository,
                    id: request.dataUser.id,
                    name: request.dataUser.Name,
                    lastName: request.dataUser.LastName,
                    identificationDocument: request.dataUser.IdentificationDocument,
                    cellPhone: request.dataUser.Phone);
            }
        }

    }
}
