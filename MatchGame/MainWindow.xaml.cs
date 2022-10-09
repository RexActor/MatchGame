using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace MatchGame
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	/// 
	
	public partial class MainWindow : Window
	{
		TextBlock lastTextBlockClicked;
		bool findMatching = false;
		DispatcherTimer timer = new DispatcherTimer();
		int tenthofSecondsElapsed;
		int matchesFound;
		public MainWindow()
		{
			
			InitializeComponent();
			SetupGame();
			timer.Interval = TimeSpan.FromSeconds(.1);
			timer.Tick += Timer_Tick;
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			tenthofSecondsElapsed++;
			timeTextBlock.Text = (tenthofSecondsElapsed / 10F).ToString("0.0s");
			if (matchesFound == 8)
			{
				timer.Stop();
				timeTextBlock.Text = timeTextBlock.Text + " - Play Again?";
			}
		}

		private void SetupGame()
		{
			List<string> animalEmoji = new List<string>()
			{
				"🙈","🙈",
				"🐕","🐕",
				"🦍","🦍",
				"🦊","🦊",
				"🦝","🦝",
				"🐴","🐴",
				"🐪","🐪",
				"🦔","🦔",
			};

			Random random = new Random();

			foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) 
			{
				if (textBlock.Name != "timeTextBlock")
				{
					int index = random.Next(animalEmoji.Count);
					string nextEmoji = animalEmoji[index];
					textBlock.Text = nextEmoji;
					animalEmoji.RemoveAt(index);
				}
			}
			timer.Start();
			tenthofSecondsElapsed = 0;
			matchesFound = 0;

		}

		private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
		{
			TextBlock textBlock= sender as TextBlock;
			if(findMatching == false)
			{
				textBlock.Visibility = Visibility.Hidden;
				lastTextBlockClicked = textBlock;
				findMatching = true;
			}
			else if (textBlock.Text == lastTextBlockClicked.Text)
			{
				matchesFound++;
				textBlock.Visibility = Visibility.Hidden;
				findMatching = false;
			}
			else
			{
				lastTextBlockClicked.Visibility = Visibility.Visible;
				findMatching = false;
			}

		}
	}
}