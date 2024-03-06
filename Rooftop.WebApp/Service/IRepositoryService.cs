using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;

namespace Rooftop.WebApp.Service;

public interface IRepositoryService<TEntity, IModel>
    where TEntity : class,
    new() where IModel : class
{
    Task<IEnumerable<IModel>> GetAllAsync(CancellationToken cancellation);
    Task<IModel> InsertAsync(IModel model, CancellationToken cancellation);
    Task<IModel> UpdateAsync(int id, IModel model, CancellationToken cancellation);
    Task<IModel> DeleteAsync(int id, CancellationToken cancellation);
    Task<IModel> GetByIdAsync(int id, CancellationToken cancellation);
    Task<List<IModel>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
}
