using CardManagement.Core;

namespace CardManagement.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public CardManagementViewModel CardManagementVM { get; set; }
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
    }
}