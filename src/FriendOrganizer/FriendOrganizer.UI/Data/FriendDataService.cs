using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data;

public class FriendDataService : IFriendDataService
{
    private readonly FriendOrganizerDbContext _context;

    public FriendDataService(FriendOrganizerDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<Friend>> GetAllAsync()
    {
        var friends = await _context.Friends.AsNoTracking().ToListAsync();
        return friends;
    }
}
