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
using System.Windows.Threading;

namespace MatchGameRetried1
{
    using System.Windows.Threading;
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;
        
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_tick;

            SetUpGame();
        }

        private void Timer_tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed/ 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play Again? ";
            }
        }

        private void SetUpGame()
        {
            List<string> animalemoji = new List<string>()
            {
            "🐵","🐵",
            "🐺","🐺",
            "🦁","🦁",
            "🦌","🦌",
            "🦣","🦣",
            "🐧","🐧",
            "🐍","🐍",
            "🐬","🐬",
            };

            Random random = new Random();

            foreach (TextBlock textblock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textblock.Name != "timeTextBlock")
                {
                    textblock.Visibility = Visibility.Visible;
                    int index = random.Next(animalemoji.Count);
                    string nextEmoji = animalemoji[index];
                    textblock.Text = nextEmoji;
                    animalemoji.RemoveAt(index);
                }
            }

            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;

        }

        /* the variables inside the class */

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            if (findingMatch == false)             /* if nothing clicked */
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)  /* if 2nd click & last click matched*/
            {
                matchesFound++;                         /* count the matches found by player everytime */
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else        /* if 2nd click & last click does not match, which is why visibility is visible */
            {
                textBlock.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
