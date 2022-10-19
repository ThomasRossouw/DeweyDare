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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROG7312_ST10121910
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();       
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to play the game of Sorting?",
            "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Sort sort = new Sort();
                this.Visibility = Visibility.Hidden;
                sort.Show(); // loads the sorting page 
            }
            else
            {
                MainWindow main = new MainWindow();
                this.Visibility = Visibility.Hidden;
                main.Show(); // Reloads the main page 
            }


            //Main.Content = new Sorting();
            //Sort sort = new Sort();
            //this.Visibility = Visibility.Hidden;
            //sort.Show();

        }
      
        // The action to change to the Main Window
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Visibility = Visibility.Hidden;
            main.Show();
        }

        //pop up to notify the User that the game is coming soon
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to play the game of Mix and Match?",
          "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Mix mix = new Mix();
                this.Visibility = Visibility.Hidden;
                mix.Show(); // loads the Mix page 
            }
            else
            {
                MainWindow main = new MainWindow();
                this.Visibility = Visibility.Hidden;
                main.Show(); // Reloads the main page 
            }
        }

        //pop up to notify the User that the game is coming soon
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Coming Soon", "Exciting");
        }

        //pop up that thanks the user and lets them exit the game
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thank you for using The Book Game!", "Thank you"); // Ok message box --> Thank you

            this.Close(); // closes the program for the user 
        }
    }
}
