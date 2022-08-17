using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace WaterSports
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Booking> displayAppointments = null;
        BookingList appointmentList = new BookingList();

        public ObservableCollection<Booking> DisplayAppointments { get => displayAppointments; set => displayAppointments = value; }

        enum Slots
        {
            AM9h = 0,
            AM9h30m = 1,
            AM10h = 2,
            AM10h30m = 3,
            AM11h = 4,
            AM11h30m = 5,
            PM1h = 6,
            PM1h30m = 7,
            PM2h = 8,
            PM2h30m = 9,
        }

        public MainWindow()
        {
            InitializeComponent();
            displayAppointments = new ObservableCollection<Booking>();
            DataContext = this;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool result = true;
            if ((textAge.Text == string.Empty) || (textName.Text == string.Empty) || (textPhonenumber.Text == string.Empty) || (textCreditcard.Text == string.Empty) || (Gender.SelectedIndex < 0) || (Services.SelectedIndex < 0) || Sports.SelectedIndex < 0)
            {
                MessageBox.Show("Please fill all the fields");
                result = false;
            }

            int age = 0;
            if (textAge.Text != string.Empty && !int.TryParse(textAge.Text, out age))
            {
                textAge.Foreground = Brushes.Red;
                result = false;
            }
            string creditcardRead = textCreditcard.Text;
            string creditcardTrim = creditcardRead.Trim();
            string creditcardReplace = creditcardTrim.Replace(" ", string.Empty);
            if (result != false && creditcardReplace.Length != 16)
            {
                textCreditcard.Foreground = Brushes.Red;
                if (result != false)
                {
                    if (MessageBox.Show("Please enter 16 digit credit card number",
                    "Confirmation", MessageBoxButton.OK) == MessageBoxResult.OK)
                    {
                        textCreditcard.Text = string.Empty;
                        textCreditcard.Foreground = Brushes.Black;

                    }
                    result = false;
                }
            }

            if (result)
            {
                string encrypt = "XXXX XXXX ";
                // hidden the middle eight digits of credit card
                string creditcard = creditcardReplace.Substring(0, 4) + " " + encrypt + creditcardReplace.Substring(12, 4);
                var time_item = (ComboBoxItem)available_Slots.SelectedValue;
                var time_content = (string)time_item.Content;
                var Gender_item = (ComboBoxItem)Gender.SelectedValue;
                var Gender_content = (string)Gender_item.Content;
                Client customer = null;
                Booking booking = new Booking();
                booking.AppointmentTime = time_content;
                customer = new Client(textName.Text, age, Gender_content, textPhonenumber.Text, creditcard);

                switch (Sports.SelectedItem.ToString())
                {
                    case "Scuba Diving":
                        customer.Sport = new Scuba(Services.SelectedItem.ToString(), Sports.SelectedItem.ToString());
                        break;
                    case "Snorkeling":
                        customer.Sport = new Snorkeling(Services.SelectedItem.ToString(), Sports.SelectedItem.ToString());
                        break;
                    case "Inflatable water park":
                        customer.Sport = new InflatableWaterpark(Services.SelectedItem.ToString(), Sports.SelectedItem.ToString());
                        break;

                    default:
                        MessageBox.Show("Program Error");
                        return;
                }
                booking.Client = customer;
                DisplayAppointments.Add(booking);

                if (MyGrid.ItemsSource != DisplayAppointments)
                {
                    MyGrid.ItemsSource = DisplayAppointments;
                }
                // reset fields
                Sports.SelectedIndex = -1;
                textAge.Text = string.Empty;
                textName.Text = string.Empty;
                textCreditcard.Text = string.Empty;
                textPhonenumber.Text = string.Empty;
                Gender.SelectedIndex = -1;
                available_Slots.Items.RemoveAt(available_Slots.Items.IndexOf(available_Slots.SelectedItem));
                available_Slots.SelectedIndex = -1;
                Services.SelectedIndex = -1;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Booking item = MyGrid.SelectedItem as Booking;
            DisplayAppointments.Remove(item);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = txtSearch.Text;
            if (search.Length > 0)
            {
                var query = from person in displayAppointments
                            where person.Client.Sport.Sport_type.ToString().ToLower() == search.ToLower()
                            select person;

                MyGrid.ItemsSource = query;
            }
        }

        private void WriteToFile()
        {
            TextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BookingList));
                tw = new StreamWriter("appointments.xml");
                serializer.Serialize(tw, appointmentList);
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                tw.Close();
            }
        }

        private void ReadFromFile()
        {
            TextReader tr = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BookingList));
                tr = new StreamReader("appointments.xml");
                appointmentList = (BookingList)serializer.Deserialize(tr);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                tr.Close();
            }
        }

        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            appointmentList.Appointments.Clear();
            ReadFromFile();
            displayAppointments.Clear();
            for (int i = 0; i < appointmentList.Count; i++)
            {
                Booking booking = new Booking();
                booking.AppointmentTime = appointmentList[i].AppointmentTime;
                Client customer = appointmentList[i].Client;
                Client newcustomer = new Client();
                if (customer != null)
                {
                    newcustomer.Age = customer.Age;
                    newcustomer.Name = customer.Name;
                    newcustomer.PhoneNumber = customer.PhoneNumber;
                    newcustomer.CreditCardNumber = customer.CreditCardNumber;
                    booking.AppointmentTime = booking.AppointmentTime;
                    newcustomer.Gender = customer.Gender;
                    newcustomer.Sport = customer.Sport;
                    booking.Client = newcustomer;
                    DisplayAppointments.Add(booking);
                }
                if (MyGrid.ItemsSource != DisplayAppointments)
                {
                    MyGrid.ItemsSource = DisplayAppointments;
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            appointmentList.Appointments.Clear();
            foreach (Booking booking in displayAppointments)
            {
                appointmentList.Add(booking);
            }
            WriteToFile();
        }

        private void textAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sports.Items.Clear();
            int age = 0;
            int.TryParse(textAge.Text, out age);
            if (age >= 15)
            {
                Sports.Items.Add("Scuba Diving");
            }
            if (age >= 10)
            {
                Sports.Items.Add("Snorkeling");
            }
            if (age <= 18)
            {
                Sports.Items.Add("Inflatable water park");
            }
        }

        private void Sports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Services.Items.Clear();
            if (Sports.SelectedIndex >= 0)
            {
                if (Sports.SelectedItem.ToString() == "Scuba Diving")
                {
                    Services.Items.Add("Video Clip");
                    Services.Items.Add("Photos(maximum 5)");
                    Services.Items.Add("Pickup & Drop off");
                    Services.Items.Add("Training Session");
                    Services.Items.Add("Scuba Tank");
                }
                else if (Sports.SelectedItem.ToString() == "Snorkeling")
                {
                    Services.Items.Add("Video Clip");
                    Services.Items.Add("Photos(maximum 5)");
                    Services.Items.Add("Pickup & Drop off");
                    Services.Items.Add("Snorkel");
                }
                else if (Sports.SelectedItem.ToString() == "Inflatable water park")
                {
                    Services.Items.Add("Video Clip");
                    Services.Items.Add("Photos(maximum 5)");
                    Services.Items.Add("Pickup & Drop off");
                    Services.Items.Add("Life Jackets");
                    Services.Items.Add("Accompanied by patron");
                }
            }
        }
    }
}