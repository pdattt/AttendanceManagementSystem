using CardManagement.MVVM.Model;
using CardManagement.MVVM.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
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
            gridAttendee.ItemsSource = attendees;
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            this.Dispatcher.Invoke(() =>
            {
                string indata = sp.ReadExisting();
                //messages.Add(indata);

                //var cardID = gridAttendee.Columns[4] as DataGridTextColumn;
                //cardID.Binding = new Binding(indata);

                int index = gridAttendee.Items.IndexOf(gridAttendee.SelectedItem);
                ++index;

                gridAttendee.Focus();
                gridAttendee.SelectedItem = gridAttendee.Items[index];

                //txt_MessageBox.Items.Add(indata.Replace('\r', ' ').Replace('\n', ' ').Trim());
                //txt_Search.Text = indata;
            });
        }

        private void displaydata_event(object sender, EventArgs e)
        {
            //string in_data = Port.ReadLine();
            //txt_MessageBox.Items.Add(in_data + "\n");
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
            //Port.StopBits = StopBits.None;
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
    }
}