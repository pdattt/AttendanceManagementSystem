using CardManagement.MVVM.Model;
using CardManagement.MVVM.ViewModel;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;

namespace CardManagement.MVVM.View
{
    /// <summary>
    /// Interaction logic for CardManagementView.xaml
    /// </summary>
    public partial class CardManagementView : UserControl
    {
        private SerialPort Port;
        private CardManagementViewModel CardVM;

        private List<Attendee> attendees;
        //private readonly AttendeeBUS _bus;

        public CardManagementView()
        {
            InitializeComponent();
            CardVM = new CardManagementViewModel();
        }

        private async void CardManagement_Load(object sender, RoutedEventArgs e)
        {
            attendees = await CardVM.GetAllAttendee();
            gridAttendee.ItemsSource = attendees;
        }
    }
}