using System.Linq.Expressions;

namespace GerenciadorTarefas.Domain.Interfaces.Repositories;

public interface IRepositoryBase<T> where T : class
{
    void Create(T entity);
    Task<IEnumerable<T>> GetListByCondition(Expression<Func<T, bool>> expression);
    Task<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
    Task<IEnumerable<T>> GetAll();
    void Update(T entity);
    void Remove(T entity);
    Task Commit();
}
