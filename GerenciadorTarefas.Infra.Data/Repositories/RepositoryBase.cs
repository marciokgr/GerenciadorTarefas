using Microsoft.EntityFrameworkCore;
using GerenciadorTarefas.Domain.Interfaces.Repositories;
using GerenciadorTarefas.Infra.Data.Contextos;
using System.Linq.Expressions;

namespace GerenciadorTarefas.Infra.Data.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryContext context;

    public RepositoryBase(RepositoryContext context)
    {
        this.context = context;
    }

    public void Create(T entity)
    {
        this.context.Set<T>().Add(entity);
    }

    public async Task<IEnumerable<T>> GetListByCondition(Expression<Func<T, bool>> expression)
    {
        return await this.context.Set<T>().Where(expression).AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false)
    {
        return trackChanges ? await this.context.Set<T>().Where(expression).FirstOrDefaultAsync() 
                            : await this.context.Set<T>().Where(expression).AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await this.context.Set<T>().AsNoTracking().ToListAsync();
    }

    public void Update(T entity)
    {
        this.context.Set<T>().Update(entity);
    }

    public void Remove(T entity)
    {
        this.context.Set<T>().Remove(entity);
    }    
    
    public async Task Commit()
    {
        await this.context.SaveChangesAsync();
    }
}
