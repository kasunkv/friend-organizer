namespace FriendOrganizer.UI.ViewModels;

public class NavigationItemViewModel : ViewModelBase
{
	private string _displayMember = default!;

	public string DisplayMember
	{
		get { return _displayMember; }
		set
		{
			_displayMember = value;
			OnPropertyChanged();
		}
	}

	public int Id { get; }


	public NavigationItemViewModel(int id, string displayMember)
	{
		Id = id;
		DisplayMember = displayMember;
	}

}
