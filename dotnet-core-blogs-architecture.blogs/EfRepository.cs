using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using dotnet_core_blogs_architecture.Data.Data;
using dotnet_core_blogs_architecture.infrastructure;
using dotnet_core_blogs_architecture.infrastructure.Data;
using dotnet_core_blogs_architecture.infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_core_blogs_architecture.blogs;

// inherit from Ardalis.Specification type
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
{
    private readonly DbSet<T> _entity;
    public EfRepository(BlogDbContext dbContext) : base(dbContext)
    {
        _entity = dbContext.Set<T>();
    }

    public override async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        return (await _entity.AddAsync(entity, cancellationToken)).Entity;
    }

    public override async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _entity.AddRangeAsync(entities, cancellationToken);
        return entities;
    }

    public override Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _entity.Update(entity);
        return Task.CompletedTask;
    }

    public override Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _entity.UpdateRange(entities);
        return Task.CompletedTask;
    }

    public override async Task DeleteAsync(T entities, CancellationToken cancellationToken = default)
    {
        _entity.Remove(entities);
        await Task.CompletedTask;
    }

    public override async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _entity.RemoveRange(entities);
        await Task.CompletedTask;
    }

    public async Task DeleteRangeAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        _entity.RemoveRange((T)specification);
        await Task.CompletedTask;
    }

    public bool Any(ISpecification<T> specification)
    {
        return ApplySpecification(specification, true).Any();
    }

    public List<T> List(ISpecification<T> specification)
    {
        var queryResult = ApplySpecification(specification).ToList();

        return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
    }

    public async Task<PaginatedList<T>> GetPagninated(ISpecification<T> specification, PageQueryModel pageQueryModel)
    {
        var queryResult = ApplySpecification(specification);

        if (pageQueryModel.All.HasValue && pageQueryModel.All.Value)
        {
            var items = await queryResult.ToListAsync();
            return new PaginatedList<T>(items, items.Count, 1, items.Count);
        }
        else
        {
            return await this.GetPahinatedList(queryResult, pageQueryModel.Page.Value, pageQueryModel.PageSize.Value);
        }
    }

    private async Task<PaginatedList<T>> GetPahinatedList(IQueryable<T> source, int page, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, page, pageSize);
    }
}