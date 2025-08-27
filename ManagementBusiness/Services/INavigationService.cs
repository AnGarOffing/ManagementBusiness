namespace ManagementBusiness.Services
{
    public interface INavigationService
    {
        void NavigateTo<T>() where T : class;
        void NavigateBack();
        void ShowDialog<T>() where T : class;
        void CloseDialog();
    }
}
