using System.Linq.Expressions;

namespace APICatalogo.Repositories;

/// <summary>
/// Interface genérica
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T>? GetAsync(Expression<Func<T, bool>> predicate);

    T Create(T entity);

    T Update(T entity);

    T Delete(T entity);
}
