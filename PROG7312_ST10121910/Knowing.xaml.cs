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
     

        //Declaration of tree  
        private Tree<string> tree = new Tree<string>();
      

        //Declaration of variables to temporarily store for usage in the tree
        private int correctFirstLevel;
        private int correctSecondLevel;
        private int correctThirdLevel;

        // Varaibles for the game score
        private int currentGameScore=0;
        private int currentLevelsCorrect = 0;
        private bool gameStarted = false;
    
        //Varaibles for the Timer
        private DispatcherTimer _timer;
        private TimeSpan _time;
        private int HIERATCHY_LEVEL_ONE_POINTS = 5;
        private int HIERATCHY_LEVEL_TWO_POINTS = 10;

        //Initializing the component 
        public Knowing()
        {
            InitializeComponent();
        }


        //Getting Data from the text file
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

        // Creating an instance to populate the 
        private void winFindingCallNumbers_Loaded(object sender, RoutedEventArgs e)
        {

            PopulateTreeWithTextFileData();
           




        }


        //Populating the tree  with the text file content
        private void PopulateTreeWithTextFileData()
        {
            //Gets the data from text file and populates it in the tree 
            List<string> FileDataTopLevel = GetDataFromFile("(1)");
            List<string> FileDataSecondLevel = GetDataFromFile("(2)");
            List<string> FileDataThirdLevel = GetDataFromFile("(3)");

            //populating root with description of tree
            tree.Root = new TreeNode<string>() { Data = "Dewey Decimal Classification Tree" };
            tree.Root.Children = new List<TreeNode<string>>();

            //top level call popualted from text file
            foreach (var item in FileDataTopLevel)
            {
                TreeNode<string> topLevel = new TreeNode<string> { Data = item, Parent = tree.Root };
                tree.Root.Children.Add(topLevel);
            }

            //mid level call populated from text file
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

          

            //third level call populated from the text file
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


        //displays the thrid level call randomly
        private void DisplayQuestionRandomThirdLevelCallNumber()
        {
            Random rond = new Random();
            correctFirstLevel = rond.Next(0, 9); 
            correctSecondLevel = rond.Next(0, 4);   
            correctThirdLevel = rond.Next(0,1);     

            lbQuestion.Content = tree.Root.Children[correctFirstLevel].Children[correctSecondLevel].Children[correctThirdLevel].Data.Substring(4);

        }

        //displays the first set of possible answers for the user
        private void DisplayAnswerOptionsFirstLevel()
        {
            lbNumQTitle.Content = "1/2";

            Random rond = new Random();
            int i = 0;

            List<string> listForSorting = new List<string>();

            listForSorting.Add(tree.Root.Children[correctFirstLevel].Data);

            while(i<=2)
            {
                int rndfirstLevel = rond.Next(0, 9);
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
        
        // displays the second set of possible answers for the user
        private void DisplayAnswerOptionsSecondLevel()
        {
            List<string> SortingList = new List<string>();

            if (ListViewQuiz.Items.GetItemAt(ListViewQuiz.SelectedIndex).ToString().Equals(tree.Root.Children[correctFirstLevel].Data))
            {
                ListViewQuiz.Items.Clear();

                Random rnd = new Random();
                int i = 0;

                SortingList.Add(tree.Root.Children[correctFirstLevel].Children[correctSecondLevel].Data);

                while (i <= 2)
                {
                    int rndSecondLevel = rnd.Next(0, 5);
                    if (SortingList.Contains(tree.Root.Children[correctFirstLevel].Children[rndSecondLevel].Data) || tree.Root.Children[correctFirstLevel].Children[rndSecondLevel].Data.Equals(tree.Root.Children[correctFirstLevel].Children[correctSecondLevel].Data))
                    {

                    }
                    else
                    {
                        SortingList.Add(tree.Root.Children[correctFirstLevel].Children[rndSecondLevel].Data);
                        i++;
                    }

                }

                SortingList.Sort();

                foreach (var item in SortingList)
                {
                    ListViewQuiz.Items.Add(item);
                }
            }
        
           
        }
        
        //begins to work as the window loads -- loads the buttons and hides buttons 
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
            currentLevelsCorrect = 0;
        }

        //the start quiz button componants 
        private void btStartQuiz_Click(object sender, RoutedEventArgs e)
        {
            StartGameComponents();

            if (gameStarted == false)
            {
                gameStarted = true;
                StartCountDown();
            }
         

            
                DisplayQuestionRandomThirdLevelCallNumber();

                DisplayAnswerOptionsFirstLevel();
           

          
        }

        //the second set of possible answers
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                if (ListViewQuiz.Items.GetItemAt(ListViewQuiz.SelectedIndex).ToString().Equals(tree.Root.Children[correctFirstLevel].Data))
                {
                    DisplayAnswerOptionsSecondLevel();
                    currentGameScore += HIERATCHY_LEVEL_ONE_POINTS;
                    currentLevelsCorrect++;
                    lbNumQTitle.Content = "2/2";
                 
                  
                }
                else if (ListViewQuiz.Items.GetItemAt(ListViewQuiz.SelectedIndex).ToString().Equals(tree.Root.Children[correctFirstLevel].Children[correctSecondLevel].Data))
                {
                    MessageBox.Show("You got the correct matching set. Click NEXT QUESTION to start the next question or END QUIZ to end the current game. ", "Congratulations! :D");
                    ListViewQuiz.Items.Clear();
                    lbQuestion.Content = "";
                    currentGameScore += HIERATCHY_LEVEL_TWO_POINTS;
                    currentLevelsCorrect++;


                    if (tbTimeDisplay.Text.Equals("00:00:00"))
                    {
                        DetermineGameResult();
                        ResetGameComponents();
                    }
                    else
                    {
                        btStartQuizDisabled.Visibility = Visibility.Hidden;
                        lbNumQTitle.Visibility = Visibility.Hidden;
                        ListViewQuiz.IsEnabled = false;
                    }
                   
                    
                }
                else
                {
                    MessageBox.Show("Unfortunately, your selected set is incorrect."
                        +"Click NEXT QUESTION to start the next question or END QUIZ to end the current game.","Game Over :(");
                    currentLevelsCorrect = 0;
                    if (tbTimeDisplay.Text.Equals("00:00:00"))
                    {
                        DetermineGameResult();
                        ResetGameComponents();
                    }
                    else
                    {
                        btStartQuizDisabled.Visibility = Visibility.Hidden;
                        lbNumQTitle.Visibility = Visibility.Hidden;
                        ListViewQuiz.IsEnabled = false;
                    }
                }

                if (currentGameScore >= 100)
                {
                    DetermineGameResult();
                    ResetGameComponents();
                }
                tbCurrentScore.Text = currentGameScore.ToString();
             

            }
        }

        // the end quiz button with popups and end of game
        private void btEndQuiz_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you certain you are wanting to end your current game? All your points will be lost.", "End Current Quiz?", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {

                DetermineGameResult();
                ResetGameComponents();

            }
        }

        //Game result from points and time limits of completion
        private void DetermineGameResult()
        {
            _timer.Stop();
            tbTimeDisplay.Text = "00:00:00";
            tbCurrentScore.Text = currentGameScore.ToString();

            if (currentGameScore < 100)
            {
                MessageBox.Show("Unfortunately the points weren't enough to win. You scored " + currentGameScore + " points that round.", "Better Luck next time :(");


            }
            else
            {
                MessageBox.Show("Congratulations! You scored " + currentGameScore + " points that round. ", "You Win! :D");


            }
            gameStarted = false;

       
        }

        //Reset the game when Try again is pressed by the User
        private void ResetGameComponents()
        {
            ListViewQuiz.Items.Clear();
            lbQuestion.Content = "";
            lbNumQTitle.Visibility = Visibility.Hidden;
            btStartQuiz.Content = "Try Again?";
            btEndQuizDisabled.Visibility = Visibility.Visible;
            btStartQuizDisabled.Visibility = Visibility.Hidden;
            currentGameScore = 0;
            tbCurrentScore.Text = currentGameScore.ToString();
        }

        //The countdown begins with 3 minutes to complete and get to 100 points
        private void StartCountDown()
        {
            
            _time = TimeSpan.FromMinutes(3);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTimeDisplay.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
            

           
        }

        //The action to reload the Quiz Game
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

