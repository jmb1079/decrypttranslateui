using Microsoft.AspNetCore.Components;

namespace DecryptTranslateUi.Services
{
    public class NavigationHelper
    {
        private NavigationManager _navigationManager;

        public NavigationHelper(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void NavigateTo(string page)
        {
            _navigationManager.NavigateTo(page);
        }
    }
}