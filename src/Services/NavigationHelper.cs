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
            this.NavigateTo(page, false);
        }

        public void NavigateTo(string page, bool reload)
        {
            _navigationManager.NavigateTo(page, reload);
        }
    }
}