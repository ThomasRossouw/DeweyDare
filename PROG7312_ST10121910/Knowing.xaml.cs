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

using PROG7312_ST10121910.Models;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Threading;
using System.Threading;
using PROG7312_ST10121910.TreeData;

namespace PROG7312_ST10121910
{
    /// <summary>
    /// Interaction logic for Knowing.xaml
    /// </summary>
    public partial class Knowing : Window
    {
        

        //Declaration of tree object 
        private Tree<string> tree = new Tree<string>();


        //Declaration of global variables to temporarily store correct question child/parent index numbers for usage in the tree
        private int correctFirstLevel;
        private int correctSecondLevel;
        private int correctThirdLevel;

      
        
        private bool gameStarted = false;
    


     








        private List<string> GetDataFromFile(string id)
        {
            List<string> list = new List<string>();

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PROG7312_ST10121910.Call.txt"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (reader.Peek() >= 0)
                    {
                        string input = reader.ReadLine();
                        string result = input.Substring(0, 3);

                        if (result.Equals(id))
                        {
                            input = input.Substring(3);
                            list.Add(input);
                        }


                    }
                }
            }

            return list;
        }

        private void winFindingCallNumbers_Loaded(object sender, RoutedEventArgs e)
        {

            PopulateTreeWithTextFileData();


        }

        private void PopulateTreeWithTextFileData()
        {
            //Retrieves data from txt file and stores it in list for populating Tree Data Structure
            List<string> FileDataTopLevel = GetDataFromFile("(1)");
            List<string> FileDataSecondLevel = GetDataFromFile("(2)");
            List<string> FileDataThirdLevel = GetDataFromFile("(3)");

            //populating root with description of tree. Other nodes populated from txt file
            tree.Root = new TreeNode<string>() { Data = "Dewey Decimal Classification Tree" };
            tree.Root.Children = new List<TreeNode<string>>();

            //Adding data to Tree from list containing top level call number txt file data
            foreach (var item in FileDataTopLevel)
            {
                TreeNode<string> topLevel = new TreeNode<string> { Data = item, Parent = tree.Root };
                tree.Root.Children.Add(topLevel);
            }

            //Adding data to Tree from list containing second level call number txt file data 
            int indexEnd = 5;
            int indexStart = 0;
            int childrenFirst = 0;
            while (childrenFirst <= 9)
            {
                tree.Root.Children[childrenFirst].Children = new List<TreeNode<string>>();
                for (int x = indexStart; x < indexEnd; x++)
                {
                    TreeNode<string> dd = new TreeNode<string> { Data = FileDataSecondLevel[x], Parent = tree.Root.Children[childrenFirst] };
                    tree.Root.Children[childrenFirst].Children.Add(dd);

                }
                indexStart += 5;
                indexEnd += 5;
                childrenFirst++;
            }



            //Adding data to Tree from list containing third level call number txt file data 
            indexEnd = 2;
            indexStart = 0;
            childrenFirst = 0;
            int childrenSecond = 0;
            while (childrenFirst <= 9)
            {
                while (childrenSecond <= 4)
                {
                    tree.Root.Children[childrenFirst].Children[childrenSecond].Children = new List<TreeNode<string>>();
                    for (int x = indexStart; x < indexEnd; x++)
                    {
                        TreeNode<string> dd = new TreeNode<string> { Data = FileDataThirdLevel[x], Parent = tree.Root.Children[childrenFirst].Children[childrenSecond] };
                        tree.Root.Children[childrenFirst].Children[childrenSecond].Children.Add(dd);

                    }

                    indexStart += 2;
                    indexEnd += 2;
                    childrenSecond++;

                }
                childrenSecond = 0;
                childrenFirst++;
            }



        }

        private void DisplayQuestionRandomThirdLevelCallNumber()
        {
            Random rnd = new Random();
            correctFirstLevel = rnd.Next(0, 9);
            correctSecondLevel = rnd.Next(0, 4);
            correctThirdLevel = rnd.Next(0, 1);

            lbQuestion.Content = tree.Root.Children[correctFirstLevel].Children[correctSecondLevel].Children[correctThirdLevel].Data.Substring(4);

        }

        private void DisplayAnswerOptionsFirstLevel()
        {
            lbNumQTitle.Content = "1/2";

            Random rnd = new Random();
            int i = 0;

            List<string> listForSorting = new List<string>();

            listForSorting.Add(tree.Root.Children[correctFirstLevel].Data);

            while (i <= 2)
            {
                int rndfirstLevel = rnd.Next(0, 9);
                if (listForSorting.Contains(tree.Root.Children[rndfirstLevel].Data) || tree.Root.Children[rndfirstLevel].Data.Equals(tree.Root.Children[correctFirstLevel].Data))
                {

                }
                else
                {
                    listForSorting.Add(tree.Root.Children[rndfirstLevel].Data);
                    i++;
                }

            }

            listForSorting.Sort();

            foreach (var item in listForSorting)
            {
                ListViewQuiz.Items.Add(item);
            }


        }

        private void DisplayAnswerOptionsSecondLevel()
        {
            List<string> listForSorting = new List<string>();

            if (ListViewQuiz.Items.GetItemAt(ListViewQuiz.SelectedIndex).ToString().Equals(tree.Root.Children[correctFirstLevel].Data))
            {
                ListViewQuiz.Items.Clear();

                Random rnd = new Random();
                int i = 0;

                listForSorting.Add(tree.Root.Children[correctFirstLevel].Children[correctSecondLevel].Data);

                while (i <= 2)
                {
                    int rndSecondLevel = rnd.Next(0, 5);
                    if (listForSorting.Contains(tree.Root.Children[correctFirstLevel].Children[rndSecondLevel].Data) || tree.Root.Children[correctFirstLevel].Children[rndSecondLevel].Data.Equals(tree.Root.Children[correctFirstLevel].Children[correctSecondLevel].Data))
                    {

                    }
                    else
                    {
                        listForSorting.Add(tree.Root.Children[correctFirstLevel].Children[rndSecondLevel].Data);
                        i++;
                    }

                }

                listForSorting.Sort();

                foreach (var item in listForSorting)
                {
                    ListViewQuiz.Items.Add(item);
                }
            }


        }

        public void StartGameComponents()
        {
            btStartQuiz.Content = "NEXT QUESTION";
            ListViewQuiz.Items.Clear();
            btStartQuizDisabled.Visibility = Visibility.Visible;
            btEndQuiz.Visibility = Visibility.Visible;
            btEndQuizDisabled.Visibility = Visibility.Hidden;
            lbQuestion.Visibility = Visibility.Visible;
            lbNumQTitle.Visibility = Visibility.Visible;
            ListViewQuiz.IsEnabled = true;
           
        }

        private void btStartQuiz_Click(object sender, RoutedEventArgs e)
        {
            StartGameComponents();

            if (gameStarted == false)
            {
                gameStarted = true;

            }



            DisplayQuestionRandomThirdLevelCallNumber();

            DisplayAnswerOptionsFirstLevel();



        }



        private void btEndQuiz_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to end the current game? If you did not achieve enough points the game will be lost", "End Current Quiz?", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {

                ResetGameComponents();

            }
        }


        private void ResetGameComponents()
        {
            ListViewQuiz.Items.Clear();
            lbQuestion.Content = "";
            lbNumQTitle.Visibility = Visibility.Hidden;
            btStartQuiz.Content = "Try Again?";
            btEndQuizDisabled.Visibility = Visibility.Visible;
            btStartQuizDisabled.Visibility = Visibility.Hidden;
   
        }



        private void ButtonQuiz_Click(object sender, RoutedEventArgs e)
        {
            Knowing know = new Knowing();
            this.Visibility = Visibility.Hidden;
            know.Show(); // loads the Mix page 
        }

        // The action to change to the Sorting Game
        private void ButtonBook_Click(object sender, RoutedEventArgs e)
        {
            Sort sort = new Sort();
            this.Visibility = Visibility.Hidden;
            sort.Show();
        }

        // The action to change to the Main Window
        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Visibility = Visibility.Hidden;
            main.Show();
        }

        // The action to change to the Mix and Match Game
        private void ButtonMix_Click(object sender, RoutedEventArgs e)
        {
            Mix mix = new Mix();
            this.Visibility = Visibility.Hidden;
            mix.Show();
        }

        //pop up that thanks the user and lets them exit the game
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thank you for using The Book Game!", "Thank you"); // Ok message box --> Thank you

            this.Close(); // closes the program for the user 
        }
    }






       
    }

