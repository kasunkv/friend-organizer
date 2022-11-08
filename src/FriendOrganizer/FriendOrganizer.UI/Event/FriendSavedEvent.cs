using Prism.Events;

namespace FriendOrganizer.UI.Event;

public class FriendSavedEvent : PubSubEvent<FriendSavedEventArgs>
{

}

public class FriendSavedEventArgs
{
    public int Id { get; set; }
    public string DisplayMember { get; set; } = default!;
}

