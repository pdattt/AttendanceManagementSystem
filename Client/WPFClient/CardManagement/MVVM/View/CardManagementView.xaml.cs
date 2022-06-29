using CardManagement.MVVM.Model;
using CardManagement.MVVM.ViewModel.MainView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
        private bool isPortOpen = false;
        private ArrayList messages;
        private int nRow;

        public CardManagementView()
        {
            InitializeComponent();
            CardVM = new CardManagementViewModel();
            messages = new ArrayList();
            nRow = gridAttendee.SelectedIndex;
        }

        private async void CardManagement_Load(object sender, RoutedEventArgs e)
        {
            attendees = await CardVM.GetAllAttendee();

            if (attendees == null)
            {
                btn_StartRead.IsEnabled = false;
                btn_RemoveCard.IsEnabled = false;
                return;
            }

            gridAttendee.ItemsSource = attendees;
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting().Replace("\r", "").Replace("\n", "").Trim();
            string message = "";
            this.Dispatcher.Invoke(() =>
            {
                if (gridAttendee.SelectedIndex < 0)
                    return;

                Attendee attendee = (Attendee)gridAttendee.Items[gridAttendee.SelectedIndex];

                var respone = CardVM.UpdateAttendeeCardId(attendee.ID, indata);

                if (!respone.Contains("Update attendee card successful!"))
                {
                    message = "[" + DateTime.Now.ToString("G") + "]: " + "Attendee ID:" + attendee.ID.ToString() + "\n" + respone;
                    txt_MessageBox.Items.Add(message);
                    txt_MessageBox.ScrollIntoView(message);
                    return;
                }

                int index = gridAttendee.Items.IndexOf(gridAttendee.SelectedItem);
                ++index;

                Console.WriteLine(indata);

                gridAttendee.SelectedItem = gridAttendee.Items[index];
                message = "[" + DateTime.Now.ToString("G") + "]: " + "Attendee ID:" + attendee.ID.ToString() + "\n" + respone;
                txt_MessageBox.Items.Add(message);
                txt_MessageBox.ScrollIntoView(message);
            });
        }

        private void btn_StartRead_Click(object sender, RoutedEventArgs e)
        {
            if (isPortOpen)
                return;

            if (gridAttendee.SelectedItem == null)
            {
                gridAttendee.Focus();
                gridAttendee.SelectedItem = gridAttendee.Items[0];
            }
            nRow = gridAttendee.SelectedIndex;
            Port = new SerialPort();
            Port.BaudRate = 9600;
            Port.PortName = "COM4";
            Port.Parity = Parity.None;
            Port.DataBits = 8;
            Port.ReadTimeout = 500;
            Port.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);

            try
            {
                Port.Open();
                isPortOpen = true;
                btn_StopRead.IsEnabled = true;
                btn_StartRead.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btn_StopRead_Click(object sender, RoutedEventArgs e)
        {
            if (!isPortOpen)
                return;

            Port.Close();
            isPortOpen = false;
            btn_StopRead.IsEnabled = false;
            btn_StartRead.IsEnabled = true;
        }

        private void Sync_Click(object sender, RoutedEventArgs e)
        {
            CardManagement_Load(null, null);
        }

        private void btn_RemoveCard_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}