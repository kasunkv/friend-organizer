using System;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModels;

public class MainViewModel : ViewModelBase
{
	public INavigationViewModel NavigationViewModel { get; }

	public IFriendDetailViewModel FriendDetailViewModel { get; }

	public MainViewModel(INavigationViewModel navigationViewModel, IFriendDetailViewModel friendDetailViewModel)
	{
		NavigationViewModel = navigationViewModel ?? throw new ArgumentNullException(nameof(navigationViewModel));
		FriendDetailViewModel = friendDetailViewModel ?? throw new ArgumentException(nameof(friendDetailViewModel));
	}

	public async Task LoadAsync()
	{
		await NavigationViewModel.LoadAsync();
	}
}
