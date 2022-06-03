using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Domain
{
    public interface IRepository
    {
        /// <summary>
        /// guarda una entidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Save<T>(T obj) where T : Entity;

        /// <summary>
        /// actualizar la entidad en db
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public void Update<T>(T obj) where T : Entity;

        /// <summary>
        /// retorna el usuario por id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T> Get<T>(Expression<Func<T, bool>> expression) where T : Entity;

        /// <summary>
        /// Retorna una lista teniendo en cuenta una condicion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<List<T>> GetAll<T>(Expression<Func<T, bool>> expression) where T : Entity;

        /// <summary>
        /// verificar si existe
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool Exists<T>(Expression<Func<T, bool>> expression) where T : Entity;

        /// <summary>
        /// Verifica si existe teniendo en cuenta 2 condiciones
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="conditionWhereOne"></param>
        /// <param name="conditionWhereSecond"></param>
        /// <returns></returns>
        public bool Exists<T>(Expression<Func<T, bool>> expression, Expression<Func<T, bool>> conditionWhereOne, Expression<Func<T, bool>> conditionWhereSecond) where T : Entity;

        /// <summary>
        /// Verifica si existe bajo una condicion 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditionWhereOne"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool Exists<T>(Expression<Func<T, bool>> conditionWhereOne, Expression<Func<T, bool>> expression) where T : Entity;

        /// <summary>
        /// Obtiene un objeto teniendo en cuenta una condicion 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditionWhereOne"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<T> Get<T>(Expression<Func<T, bool>> conditionWhereOne, Expression<Func<T, bool>> expression) where T : Entity;


        /// <summary>
        /// retornamos una lista teniendo en cuenta el filtro, pagina, numero de pagina y condicion.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="expressionNest"></param>
        /// <param name="expressionConditional"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<T>> GetAll<T>(Expression<Func<T, string>> sort, int page, int pageSize,
            Expression<Func<T, bool>> expressionConditional) where T : Entity;

        /// <summary>
        /// Retorna una lista teniendo en cuenta filtro, pagina, numero de pagina y 2 condiciones
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="expressionConditional"></param>
        /// <param name="expresionConditionalWhere"></param>
        /// <returns></returns>
        public Task<List<T>> GetAll<T>(Expression<Func<T, string>> sort, int page, int pageSize,
                   Expression<Func<T, bool>> conditionWhereOne, Expression<Func<T, bool>> conditionWhereSecond) where T : Entity;


        /// <summary>
        /// eliminar objetos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Delete<T>(T obj) where T : Entity;

        /// <summary>
        /// calcula el total de registros segun la conticion
        /// retorna un numero que representa el total de registros que tiene la busqueda segun la exprecion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expressionConditional"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<int> Count<T>(Expression<Func<T, bool>> expressionConditional) where T : Entity;


        /// <summary>
        /// obtener la entidad con el objeto anidado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="nested"></param>
        /// <returns></returns>
        public Task<T> GetNested<T>(Expression<Func<T, bool>> expression, string nested) where T : Entity;

        /// <summary>
        /// Obtiene una lista con los objetos anidados
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expressionConditional"></param>
        /// <param name="nested"></param>
        /// <returns></returns>
        public Task<List<T>> GetAllNested<T>(Expression<Func<T, bool>> expressionConditional, string nested) where T : Entity;

        /// <summary>
        /// Obtiene el ultimo objeto ordenado descendentemente por fecha con los datos anidados 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sort"></param>
        /// <param name="expressionConditional"></param>
        /// <param name="nested"></param>
        /// <returns></returns>
        public Task<T> GetLastNested<T>(Expression<Func<T, DateTime>> sort, Expression<Func<T, bool>> expressionConditional, string nested) where T : Entity;

        /// <summary>
        /// Guarda los datos en la db
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task Commit();

        /// <summary>
        /// obtener un listado respecto a dos expersiones
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expressionFirst"></param>
        /// <param name="expressionSecond"></param>
        /// <returns></returns>
        public Task<List<T>> GetAll<T>(Expression<Func<T, bool>> expressionFirst,
            Expression<Func<T, bool>> expressionSecond) where T : Entity;

        /// <summary>
        /// Obtiene un listado respecto a 1 condicion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Task<List<T>> GetAll<T>(int page, int pageSize,
            Expression<Func<T, bool>> condition) where T : Entity;

    }

}
