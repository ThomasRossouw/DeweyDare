using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PROG7312_ST10121910
{
    /// <summary>
    /// Interaction logic for Sort.xaml
    /// </summary>
    public partial class Sort : Window
    {
        public Sort()
        {
            // Random gerneration is intiated on page load
            InitializeComponent();

            string Result1 = RandomString();
            string Result2 = RandomString();
            string Result3 = RandomString();
            string Result4 = RandomString();
            string Result5 = RandomString();
            string Result6 = RandomString();
            string Result7 = RandomString();
            string Result8 = RandomString();
            string Result9 = RandomString();
            string Result10 = RandomString();

            double Number1 = GetRandomNumber(0, 1000);
            Number1 = Math.Round(Number1, 2);
            double Number2 = GetRandomNumber(0, 1000);
            Number2 = Math.Round(Number2, 2);
            double Number3 = GetRandomNumber(0, 1000);
            Number3 = Math.Round(Number3, 2);
            double Number4 = GetRandomNumber(0, 1000);
            Number4 = Math.Round(Number4, 2);
            double Number5 = GetRandomNumber(0, 1000);
            Number5 = Math.Round(Number5, 2);
            double Number6 = GetRandomNumber(0, 1000);
            Number6 = Math.Round(Number6, 2);
            double Number7 = GetRandomNumber(0, 1000);
            Number7 = Math.Round(Number7, 2);
            double Number8 = GetRandomNumber(0, 1000);
            Number8 = Math.Round(Number8, 2);
            double Number9 = GetRandomNumber(0, 1000);
            Number9 = Math.Round(Number9, 2);
            double Number10 = GetRandomNumber(0, 1000);
            Number10 = Math.Round(Number10, 2);

            cd.Add(Number1, Result1);
            cd.Add(Number2, Result2);
            cd.Add(Number3, Result3);
            cd.Add(Number4, Result4);
            cd.Add(Number5, Result5);
            cd.Add(Number6, Result6);
            cd.Add(Number7, Result7);
            cd.Add(Number8, Result8);
            cd.Add(Number9, Result9);
            cd.Add(Number10, Result10);

            var booklist = cd.ToList<KeyValuePair<double, string>>();

            lstBox1.Items.Add(booklist[0]);
            lstBox1.Items.Add(booklist[1]);
            lstBox1.Items.Add(booklist[2]);
            lstBox1.Items.Add(booklist[3]);
            lstBox1.Items.Add(booklist[4]);
            lstBox1.Items.Add(booklist[5]);
            lstBox1.Items.Add(booklist[6]);
            lstBox1.Items.Add(booklist[7]);
            lstBox1.Items.Add(booklist[8]);
            lstBox1.Items.Add(booklist[9]);



            lstBox1.SelectedIndex = 0;

        }

        // dictionaries 
        IDictionary<double, string> cd = new Dictionary<double, string>();
        IDictionary<double, string> cd1 = new Dictionary<double, string>();




        private void lstBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        // calling for the random numbers

        private static Random random = new Random();

        public static string RandomString(int length = 3)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }




        public double GetRandomNumber(double minimum, double maximum)
        {

            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        //Up button - to make the values go up
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = lstBox1.SelectedIndex;
            var listBoxItemText = lstBox1.SelectedItem;
            if (index > 0)
            {
                lstBox1.Items.RemoveAt(index);
                lstBox1.Items.Insert(index - 1, listBoxItemText);
                lstBox1.SelectedIndex = index - 1;

            }
        }

        //Down button - to make the values go down
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int index = lstBox1.SelectedIndex;
            var listBoxItemText = lstBox1.SelectedItem;
            if (index < 9)
            {
                lstBox1.Items.RemoveAt(index);
                lstBox1.Items.Insert(index + 1, listBoxItemText);
                lstBox1.SelectedIndex = index + 1;

            }
        }

        //Submit Button - allows the User to submit their work
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var sortedDict = from entry in cd orderby entry.Key ascending select entry;

            var list = sortedDict.ToList<KeyValuePair<double, string>>();

            var list2 = cd1.ToList<KeyValuePair<double, string>>();

            var selectedProperty1 = (KeyValuePair<double, string>)lstBox1.Items[0];
            var selectedProperty2 = (KeyValuePair<double, string>)lstBox1.Items[1];
            var selectedProperty3 = (KeyValuePair<double, string>)lstBox1.Items[2];
            var selectedProperty4 = (KeyValuePair<double, string>)lstBox1.Items[3];
            var selectedProperty5 = (KeyValuePair<double, string>)lstBox1.Items[4];
            var selectedProperty6 = (KeyValuePair<double, string>)lstBox1.Items[5];
            var selectedProperty7 = (KeyValuePair<double, string>)lstBox1.Items[6];
            var selectedProperty8 = (KeyValuePair<double, string>)lstBox1.Items[7];
            var selectedProperty9 = (KeyValuePair<double, string>)lstBox1.Items[8];
            var selectedProperty10 = (KeyValuePair<double, string>)lstBox1.Items[9];



            list2.Add((KeyValuePair<double, string>)lstBox1.Items[0]);
            list2.Add((KeyValuePair<double, string>)lstBox1.Items[1]);
            list2.Add((KeyValuePair<double, string>)lstBox1.Items[2]);
            list2.Add((KeyValuePair<double, string>)lstBox1.Items[3]);
            list2.Add((KeyValuePair<double, string>)lstBox1.Items[4]);
            list2.Add((KeyValuePair<double, string>)lstBox1.Items[5]);
            list2.Add((KeyValuePair<double, string>)lstBox1.Items[6]);
            list2.Add((KeyValuePair<double, string>)lstBox1.Items[7]);
            list2.Add((KeyValuePair<double, string>)lstBox1.Items[8]);
            list2.Add((KeyValuePair<double, string>)lstBox1.Items[9]);

            //pop up to notify the User that they are correct

            bool isEqual = Enumerable.SequenceEqual(list, list2);
            if (isEqual)
            {
                MessageBox.Show("You got it!", "Congratulations you are correct!");
                BarSort.Value = 100;
                OMG.Source = new BitmapImage(new Uri("/1000.png", UriKind.Relative));
               

            }
            //pop up to notify the User that they are wrong
            else
            {
                MessageBox.Show("These aren't up to the Dewey standards", "Unforuntely!");
                var test = list.All(list2.Contains) && list.Count == list.Count;
                int check = test ? 1 : 0;
                BarSort.Value = 5;
                BarSort.Value = 4;
                BarSort.Value = 3;
                BarSort.Value = 2;
                BarSort.Value = 1;
               
                OMG.Source = new BitmapImage(new Uri("/wrong.png", UriKind.Relative));

            }
        }

        // The action to change to the Main Window
        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Visibility = Visibility.Hidden;
            main.Show();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thank you for using The Book Game!", "Thank you"); // Ok message box --> Thank you

            this.Close(); // closes the program for the user 
        }

        //Reloads the page to reset the game for the User
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Sort sort = new Sort();
            this.Visibility = Visibility.Hidden;
            sort.Show();
        }

        // The action to change to the Sorting Game
        private void ButtonBook_Click(object sender, RoutedEventArgs e)
        {
            Sort sort = new Sort();
            this.Visibility = Visibility.Hidden;
            sort.Show();
        }

        // The action to change to the Mix and Match Game
        private void ButtonMix_Click(object sender, RoutedEventArgs e)
        {
            Mix mix = new Mix();
            this.Visibility = Visibility.Hidden;
            mix.Show();
        }
    }
}