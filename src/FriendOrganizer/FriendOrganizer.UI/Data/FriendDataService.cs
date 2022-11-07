using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data;

public class FriendDataService : IFriendDataService
{
    private readonly FriendOrganizerDbContext _context;

    public FriendDataService(FriendOrganizerDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Friend> GetByIdAsync(int friendId)
    {
        var friend = await _context.Friends.AsNoTracking().SingleAsync(f => f.Id == friendId);
        return friend;
    }
}
