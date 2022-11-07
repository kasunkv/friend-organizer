﻿using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using System;
using System.Collections.ObjectModel;

namespace FriendOrganizer.UI.ViewModels;

public class MainViewModel : ViewModelBase
{
	private readonly IFriendDataService _friendDataService;
    private Friend _selectedFriend;

	public ObservableCollection<Friend> Friends { get; set; }

	public Friend SelectedFriend
	{
		get { return _selectedFriend; }
		set
		{
			_selectedFriend = value;

			// OnPropertyChanged(nameof(SelectedFriend));
			OnPropertyChanged();
		}
	}


	public MainViewModel(IFriendDataService friendDataService)
	{
		Friends = new ObservableCollection<Friend>();
		_friendDataService = friendDataService ?? throw new ArgumentNullException(nameof(friendDataService));
	}

	public void Load()
	{
		var friends = _friendDataService.GetAll();

		Friends.Clear();
		foreach (var friend in friends)
		{
			Friends.Add(friend);
        }
	}
}