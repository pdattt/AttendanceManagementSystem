using CardManagement.MVVM.Model;
using CardManagement.MVVM.ViewModel;
using System;
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

        public CardManagementView()
        {
            InitializeComponent();
            CardVM = new CardManagementViewModel();

            Port = new SerialPort();
            Port.BaudRate = 9600;
            Port.PortName = "COM4";
            Port.Parity = Parity.None;
            Port.DataBits = 8;
            //Port.StopBits = StopBits.None;
            Port.ReadTimeout = 500;
            Port.DataReceived += Port_DataReceived;

            //try
            //{
            //    Port.Open();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error");
            //}
        }

        private async void CardManagement_Load(object sender, RoutedEventArgs e)
        {
            attendees = await CardVM.GetAllAttendee();
            gridAttendee.ItemsSource = attendees;
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new EventHandler(displaydata_event));
        }

        private void displaydata_event(object sender, EventArgs e)
        {
            string in_data = Port.ReadLine();
        }
    }
}