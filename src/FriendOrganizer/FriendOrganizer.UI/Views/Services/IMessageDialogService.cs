namespace FriendOrganizer.UI.Views.Services
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowOkCancelDialog(string message, string title);
    }
}