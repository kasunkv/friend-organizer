using FriendOrganizer.UI.Event;
using Prism.Commands;
using Prism.Events;
using System;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModels;

public class NavigationItemViewModel : ViewModelBase
{

	private readonly IEventAggregator _eventAggregator;

	private string _displayMember = default!;

	public ICommand OpenFriendDetailViewCommand { get; }


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

	public NavigationItemViewModel(int id, string displayMember, IEventAggregator eventAggregator)
	{
		Id = id;
		DisplayMember = displayMember;
		_eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

		OpenFriendDetailViewCommand = new DelegateCommand(OnOpenFriendDetailView);
	}

	private void OnOpenFriendDetailView()
	{
		_eventAggregator
			.GetEvent<OpenFriendDetailViewEvent>()
			.Publish(Id);
	}
}
