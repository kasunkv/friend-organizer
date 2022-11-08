using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data.Repositories;

public class FriendRepository : IFriendRepository
{
    private readonly FriendOrganizerDbContext _context;

    public FriendRepository(FriendOrganizerDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Friend> GetByIdAsync(int friendId)
    {
        var friend = await _context.Friends.SingleAsync(f => f.Id == friendId);
        return friend;
    }

    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
        //_context.ChangeTracker.Clear();
    }
}
