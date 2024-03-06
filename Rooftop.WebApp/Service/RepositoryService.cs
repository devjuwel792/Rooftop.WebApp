using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rooftop.WebApp.DatabaseContext;
using System.Linq.Expressions;

namespace Rooftop.WebApp.Service;

public class RepositoryService<TEntity, IModel> : IRepositoryService<TEntity, IModel>
    where TEntity : class,
    new() where IModel : class
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _dbContext;

    public DbSet<TEntity> DbSet { get; private set; }

    public RepositoryService(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        DbSet = _dbContext.Set<TEntity>();
    }

    public async Task<IEnumerable<IModel>> GetAllAsync(CancellationToken cancellation)
    {
        var entity = await DbSet.AsNoTracking().ToListAsync(cancellation);
        if (entity == null) return null;
        var data = _mapper.Map<IEnumerable<IModel>>(entity);
        return data;
    }


    public async Task<IModel> InsertAsync(IModel model, CancellationToken cancellation)
    {
        var entity = _mapper.Map<IModel, TEntity>(model);

        DbSet.Add(entity);
        await _dbContext.SaveChangesAsync(cancellation);

        //var insertedModel = _mapper.Map<IModel>(entity);

        return model;
    }


    public async Task<IModel> UpdateAsync(int id, IModel model, CancellationToken cancellation)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity == null) return null;
        //DbSet.Update(entity);
        _mapper.Map(model, entity);
        await _dbContext.SaveChangesAsync(cancellation);

        //var updateModel = _mapper.Map<TEntity,IModel>(entity);
        return model;
    }

    public async Task<IModel> DeleteAsync(int id, CancellationToken cancellation)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity == null) return null;
        DbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();

        var deletModel = _mapper.Map<TEntity, IModel>(entity);

        return deletModel;

    }
    public async Task<IModel> GetByIdAsync(int id, CancellationToken cancellation)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity == null) return null;
        var model = _mapper.Map<TEntity, IModel>(entity);
        return model;
    }


    public async Task<List<IModel>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        var entities = await includes.Aggregate(DbSet.AsQueryable(), (current, include) => current.Include(include)).ToListAsync().ConfigureAwait(true);


        return _mapper.Map<List<IModel>>(entities);
    }
}
