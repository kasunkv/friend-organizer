using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.Views.Services;
using Microsoft.Extensions.DependencyInjection;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModels;

public class MainViewModel : ViewModelBase
{
	private readonly IEventAggregator _eventAggregator;
	private readonly IMessageDialogService _messageDialogService;
	private readonly IServiceProvider _serviceProvider;

	public INavigationViewModel NavigationViewModel { get; }

	private IFriendDetailViewModel _friendDetailViewModel;

	public IFriendDetailViewModel FriendDetailViewModel
	{
		get { return _friendDetailViewModel; }
		private set
		{
			_friendDetailViewModel = value;
			OnPropertyChanged();
		}
	}


	public MainViewModel(INavigationViewModel navigationViewModel, IEventAggregator eventAggregator, IMessageDialogService messageDialogService, IServiceProvider serviceProvider)
	{
		_eventAggregator = eventAggregator ?? throw new ArgumentException(nameof(eventAggregator));
		_messageDialogService = messageDialogService ?? throw new ArgumentException(nameof(messageDialogService));
		_serviceProvider = serviceProvider ?? throw new ArgumentException(nameof(serviceProvider));

		NavigationViewModel = navigationViewModel ?? throw new ArgumentNullException(nameof(navigationViewModel));

		_eventAggregator
				.GetEvent<OpenFriendDetailViewEvent>()
				.Subscribe(OnOpenFriendDetailView);
	}

	public async Task LoadAsync()
	{
		await NavigationViewModel.LoadAsync();
	}

	private async void OnOpenFriendDetailView(int friendId)
	{
		if (FriendDetailViewModel != null && FriendDetailViewModel.HasChanges)
		{
			var result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Questions");
			if (result == MessageDialogResult.Cancel)
			{
				return;
			}
		}

		FriendDetailViewModel = _serviceProvider.GetRequiredService<IFriendDetailViewModel>();
		await FriendDetailViewModel.LoadAsync(friendId);
	}
}
