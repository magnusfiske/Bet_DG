using Bet.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bet.Data.Contexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace Bet.Data.Services;

public class DbService : IDbService
{
    private readonly BetContext _db;
    private readonly IMapper _mapper;

    public DbService(BetContext context, IMapper mapper)
	{
        _db = context;
        _mapper = mapper;
    }

    async Task<bool> IDbService.AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
    {
        return await _db.Set<TEntity>().AnyAsync(expression);
    }

    async Task<List<TDto>> IDbService.GetAsync<TEntity, TDto>()
    {
        var entities = await _db.Set<TEntity>().ToListAsync();
        return _mapper.Map<List<TDto>>(entities);
    }

    private async Task<TEntity?> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression) 
        where TEntity : class, IEntity
        => await _db.Set<TEntity>().SingleOrDefaultAsync(expression);

    async Task<TDto> IDbService.SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await SingleAsync<TEntity>(expression);
        return _mapper.Map<TDto>(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync() >= 0;
    }

    public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
        where TEntity : class
        where TDto : class
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _db.AddAsync(entity);
        return entity;
    }

    public void Include<TEntity>() where TEntity : class, IEntity
    {
        var propertyNames =
            _db.Model.FindEntityType(typeof(TEntity))?.GetNavigations().Select(e => e.Name);

        if (propertyNames is null)
            return;

        foreach (var name in propertyNames)
            _db.Set<TEntity>().Include(name).Load();
    }

    void IDbService.Update<TEntity, TDto>(int id, TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        entity.Id = id;
        _db.Set<TEntity>().Update(entity);
    }

    async Task<bool> IDbService.DeleteAsync<TEntity>(int id)
    {
        try
        {
            var entity = await SingleAsync<TEntity>(e => e.Id == id);
            if(entity == null) 
                return false;

            _db.Remove(entity);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
