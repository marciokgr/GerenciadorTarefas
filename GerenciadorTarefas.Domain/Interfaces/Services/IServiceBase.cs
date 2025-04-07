using System.Linq.Expressions;

namespace GerenciadorTarefas.Domain.Interfaces.Services;

public interface IServiceBase<T> where T : class
{
    T Add(T entity);
    Task<IEnumerable<T>> GetListByCondition(Expression<Func<T, bool>> expression);
    Task<T> GetByCondition(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> GetAll();
    void Update(T entity);
    void Remove(T entity);
    Task Commit();

}
