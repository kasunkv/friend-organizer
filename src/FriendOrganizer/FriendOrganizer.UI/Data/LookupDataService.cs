using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data;

public class LookupDataService : IFriendLookupDataService
{
    private readonly FriendOrganizerDbContext _context;

    public LookupDataService(FriendOrganizerDbContext friendOrganizerDbContext)
    {
        _context = friendOrganizerDbContext ?? throw new ArgumentNullException(nameof(friendOrganizerDbContext));
    }

    public async Task<IEnumerable<LookupItem>> GetFriendLookupAsync()
    {
        var lookupItems = await _context.Friends.AsNoTracking()
            .Select(f => new LookupItem
            {
                Id = f.Id,
                DisplayMember = $"{f.FirstName} {f.LastName}"
            })
            .ToListAsync();

        return lookupItems;
    }
}
