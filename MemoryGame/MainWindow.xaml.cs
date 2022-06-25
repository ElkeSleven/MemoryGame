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
using System.Windows.Threading;  // DispatcherTimer

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        Random r = new Random();
        List<char> eazyMode;
        Label clickFir;
        Label clickSec;
        private int increment;
        private DispatcherTimer timer;



        public MainWindow()
        {
            InitializeComponent();
            DefaultValues();
            SetImages(eazyMode);
        }

        private void DefaultValues()
        {
            eazyMode = new List<char> { '¨', 'à', 'l', 'n', 'w', '+', 'd', '²' };   // Webdings 'h'
            increment = 0;
            Label clickFir = null;
            Label clickSec = null;
        }
        private void SetImages(List<char> list)
        {
            Label lbl;
            int randomIndex;
            List<char> clonedList = new List<char>(list); // clone the list 
            list.AddRange(clonedList);            // Use .AddRange to append any Enumrable collection to the list.
           
            
            for (int i = 0; i < grind.Children.Count; i++)
            {
                 if(grind.Children[i] is Label)   // check if selected child is a label  (expexted : TRUE)
                 {
                    lbl = (Label)grind.Children[i];     
                    randomIndex = r.Next(0, list.Count);
                    lbl.Content = list[randomIndex];
                    list.RemoveAt(randomIndex);
                    lbl.Foreground = Brushes.DeepSkyBlue;

                 }
                  

            }  


        }

        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0,0,0,1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            increment++;
            if(increment == 1)
            {  
                timer.Stop();
                increment = 0;
                clickFir.Foreground = Brushes.DeepSkyBlue;
                clickSec.Foreground = Brushes.DeepSkyBlue;
                clickFir = null;
                clickSec = null;
            }
        }

        private void ClickImage(object sender, MouseButtonEventArgs e)
        {
            if(clickFir != null && clickSec != null)
            {
                return;
            }
            else if (sender is Label)
            {
                Label clicked = sender as Label;
                if (clicked.Foreground == Brushes.DeepSkyBlue && clickFir == null)
                {
                    clickFir = clicked;
                    clicked.Foreground = Brushes.Black;
                }
                else if (clicked.Foreground == Brushes.DeepSkyBlue && clickSec == null && clickFir != null)
                {
                    clickSec = clicked;
                    clicked.Foreground = Brushes.Black;
                }
                else if (clicked.Foreground != Brushes.DeepSkyBlue)
                {
                    return;
                }

                if (clickFir != null && clickSec != null)
                {
                    CheckPair(clickFir, clickSec);
                }
                return;
            }
            


        }
        private void CheckPair(Label a , Label b)
        {
            if(a.Content.ToString() == b.Content.ToString())
            {
                clickFir.Foreground = Brushes.Black;
                clickSec.Foreground = Brushes.Black;
                clickFir = null;
                clickSec = null;
                CheckEndGame();
                return;
            }
            else if (a.Content != b.Content)
            {
                StartTimer();
                return;
            }

        }

        private void CheckEndGame()
        {
            Label lbl;
            for (int i = 0; i < grind.Children.Count; i++)
            {
                lbl = grind.Children[i] as Label; 
                if(lbl.Foreground == Brushes.DeepSkyBlue) { return; }
            }

            MessageBox.Show("you wonn XD ");
            Close();
        }
    }
}
