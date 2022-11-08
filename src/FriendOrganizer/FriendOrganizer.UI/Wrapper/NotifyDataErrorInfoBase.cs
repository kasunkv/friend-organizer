using FriendOrganizer.UI.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FriendOrganizer.UI.Wrapper;

public class NotifyDataErrorInfoBase : ViewModelBase, INotifyDataErrorInfo
{
	private Dictionary<string, List<string>> _errorByPropertyName = new Dictionary<string, List<string>>();

	public bool HasErrors => _errorByPropertyName.Any();

	public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

	public IEnumerable? GetErrors(string? propertyName)
	{
		return _errorByPropertyName.ContainsKey(propertyName) ? _errorByPropertyName[propertyName] : null;
	}

    protected virtual void OnErrorChanged(string propertyName)
	{
		ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
		base.OnPropertyChanged(nameof(HasErrors));
	}

	protected void AddError(string propertyName, string error)
	{
		if (!_errorByPropertyName.ContainsKey(propertyName))
		{
			_errorByPropertyName.Add(propertyName, new List<string>());
		}

		if (!_errorByPropertyName[propertyName].Contains(error))
		{
			_errorByPropertyName[propertyName].Add(error);
			OnErrorChanged(propertyName);
		}
	}

	protected void ClearErrors(string propertyName)
	{
		if (_errorByPropertyName.ContainsKey(propertyName))
		{
			_errorByPropertyName.Remove(propertyName);
			OnErrorChanged(propertyName);
		}
	}
}