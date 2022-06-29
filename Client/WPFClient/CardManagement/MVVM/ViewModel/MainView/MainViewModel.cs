using CardManagement.Core;

namespace CardManagement.MVVM.ViewModel.MainView
{
    public class MainViewModel : ObservableObject
    {
        public CardManagementViewModel CardManagementVM { get; set; }
        public AccountManagementViewModel AccountManagementVM { get; set; }
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            CardManagementVM = new CardManagementViewModel();
            CurrentView = CardManagementVM;
        }

        public void GoToCardManagementView()
        {
            CurrentView = CardManagementVM;
        }

        public void GoToAccountManagementView()
        {
            CurrentView = AccountManagementVM;
        }
    }
}