﻿using Microsoft.EntityFrameworkCore;
using N66.LibraryManagement.Application.Services;
using N66.LibraryManagement.Domin.Entities.Models;
using N66.LibraryManagement.Persistance.DataContext;
using System.Linq.Expressions;

namespace N66.LibraryManagement.Infrastucture.Services;

public class AutherService : IEntityBaseService<Author>
{
    private readonly AppDBContext _appDBContext;

    public AutherService(AppDBContext appDBContext)
    {
        _appDBContext = appDBContext;
    }

    public ValueTask<ICollection<Author>> GetAsync(IEnumerable<Guid> Ids, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Author> Get(Expression<Func<Author, bool>>? pridicate = null)
     => pridicate != null ? _appDBContext.Authors.Where(pridicate) : _appDBContext.Authors;

    public  ValueTask<Author> GetByIdAsync(Guid autherId, CancellationToken cancellationToken = default)
    {
        var auther =  _appDBContext.Authors.FirstOrDefault(auther => auther.Id == autherId);

        if (auther is null) throw new InvalidOperationException("Auther not found");

        return new ValueTask<Author>(auther);
    }

    public async ValueTask<Author> CreateAsync(Author auther, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
       await _appDBContext.Authors.AddAsync(auther, cancellationToken);
       
       if(saveChanges) await _appDBContext.SaveChangesAsync(cancellationToken);
        
       return auther;
    }

    public async ValueTask<Author> UpdateAsync(Author auther, bool saveChance = true,CancellationToken cancellationToken = default)
    {
        var foundAuther = await GetByIdAsync(auther.Id);

        foundAuther.FirstName = auther.FirstName;
        foundAuther.LastName = auther.LastName;

        _appDBContext.Authors.Update(foundAuther);  
        
       if(saveChance) await _appDBContext.SaveChangesAsync(cancellationToken);
        
        return foundAuther;
    }

    public async ValueTask<Author> DeleteAsync(Guid autherId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundAuther  = await GetByIdAsync(autherId);

        _appDBContext.Authors.Remove(foundAuther);
        
       if(saveChanges) await _appDBContext.SaveChangesAsync(cancellationToken);
       
        return foundAuther;
    }

}