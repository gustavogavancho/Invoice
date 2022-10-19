using Invoice.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Invoice.Repository.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected InvoiceContext InvoiceContext;
    protected RepositoryBase(InvoiceContext invoiceContext) => InvoiceContext = invoiceContext;

    public IQueryable<T> FindAll(bool trackChanges) =>
        !trackChanges ?
        InvoiceContext.Set<T>()
        .AsNoTracking() :
        InvoiceContext.Set<T>();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        !trackChanges ?
        InvoiceContext.Set<T>()
        .Where(expression)
        .AsNoTracking() :
        InvoiceContext.Set<T>()
        .Where(expression);

    public void Create(T entity) => InvoiceContext.Set<T>().Add(entity);

    public void Update(T entity) => InvoiceContext.Set<T>().Update(entity);

    public void Delete(T entity) => InvoiceContext.Set<T>().Remove(entity);
}