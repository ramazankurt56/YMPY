using Microsoft.EntityFrameworkCore;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entities.Models;
using System.Linq.Expressions;

namespace NTierArchitecture.DataAccess.Repositories;

public sealed class ClassRoomRepository
    (ApplicationDbContext context) : IClassRoomRepository
{
    public bool Any(Expression<Func<ClassRoom, bool>> predicate)
    {
        return context.ClassRooms.AsNoTracking().Any(predicate);
    }

    public void Create(ClassRoom classRoom)
    {
        context.Add(classRoom);
        context.SaveChanges();
    }

    public void DeleteById(Guid Id)
    {
        ClassRoom? classRoom = GetClassRoomById(Id);
        if (classRoom is not null)
        {
            classRoom.IsDeleted = true;
            context.SaveChanges();
        }
    }

    public IQueryable<ClassRoom> GetAll()
    {
        return context.ClassRooms.AsNoTracking().AsQueryable();
    }

    public ClassRoom? GetClassRoomById(Guid classRoomId)
    {
        return context.ClassRooms.Where(p => p.Id == classRoomId).FirstOrDefault();
    }

    public void Update(ClassRoom classRoom)
    {
        context.Update(classRoom);
        context.SaveChanges();
    }
}
