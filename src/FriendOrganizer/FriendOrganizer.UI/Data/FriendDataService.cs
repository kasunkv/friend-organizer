using FriendOrganizer.Model;
using System.Collections.Generic;

namespace FriendOrganizer.UI.Data;

public class FriendDataService : IFriendDataService
{
    public IEnumerable<Friend> GetAll()
    {
        yield return new Friend { FirstName = "Kasun", LastName = "Kodagoda" };
        yield return new Friend { FirstName = "Oreliya", LastName = "Fernando" };
        yield return new Friend { FirstName = "John", LastName = "Doe" };
        yield return new Friend { FirstName = "Jane", LastName = "Smith" };
    }
}
