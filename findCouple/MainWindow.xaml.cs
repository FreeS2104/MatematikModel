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

namespace findCouple
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
       
        public MyfindCouple findCouple;
        Rectangle[] rectangles;
        Rectangle[] pictures;

        public MainWindow()
        {
            InitializeComponent();
            preparetionArea();
            Game();
        }
        public void preparetionArea()
        {
            rectangles = new Rectangle[20];
            pictures = new Rectangle[20];
            int i = 0;
            while(i<20)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        pictures[i] = new Rectangle();
                        pictures[i].Name = "p" + i.ToString();
                        pictures[i].Fill = new SolidColorBrush(Colors.Black);
                        pictures[i].Margin = new Thickness(2);
                        Grid.SetRow(pictures[i], y);
                        Grid.SetColumn(pictures[i], x);
                        grid.Children.Add(pictures[i]);
                        rectangles[i] = new Rectangle();
                        rectangles[i].Name = "r"+i.ToString();
                        rectangles[i].Fill = new SolidColorBrush(Colors.DeepSkyBlue);
                        rectangles[i].Margin = new Thickness(2);
                        rectangles[i].MouseEnter += new MouseEventHandler(mouseEnter);
                        rectangles[i].MouseLeave += new MouseEventHandler(mouseLeave);
                        rectangles[i].MouseLeftButtonUp += new MouseButtonEventHandler(mouseLeftButtonUp);
                        Grid.SetRow(rectangles[i], y);
                        Grid.SetColumn(rectangles[i], x);
                        grid.Children.Add(rectangles[i]);
                        i++;
                    }
                }
            }
        }
        public void Game()
        {
            for (int i = 0; i <20; i++)
            {
                rectangles[i].Visibility = Visibility.Visible;
            }
            findCouple = new MyfindCouple();
            findCouple.newGame();
            findCouple.setPictures(pictures);
        }
        private void mouseEnter(object sender, MouseEventArgs e) 
         {
            Rectangle rect = (Rectangle)sender;
            SolidColorBrush blackBrush = new SolidColorBrush(Colors.DarkBlue);
            rect.StrokeThickness = 1;
            rect.Stroke = blackBrush;
        }
        private void mouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            rect.StrokeThickness = 0;
        }
        private void mouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            rect.Visibility = Visibility.Hidden;
            int n = Convert.ToInt32(rect.Name.Remove(0, 1));
            switch (findCouple.Motion(n))
            {
                case 0:
                    findCouple.motion++;
                    break;
                case 1:
                    if (findCouple.checkWithPrevious(n))
                    {
                       TTT.Text = Convert.ToString(Math.Round(((float)100 / (float)(10-findCouple.combination)),2));
                        x1.Content = TTT.Text;
                        x2.Content = TTT.Text;
                        x3.Content = TTT.Text;
                        x4.Content = TTT.Text;
                        x5.Content = TTT.Text;
                        x6.Content = TTT.Text;
                        x7.Content = TTT.Text;
                        x8.Content = TTT.Text;
                        x9.Content = TTT.Text;
                        x10.Content = TTT.Text;
                        x11.Content = TTT.Text;
                        x12.Content = TTT.Text;
                        x13.Content = TTT.Text;
                        x14.Content = TTT.Text;
                        x15.Content = TTT.Text;
                        x16.Content = TTT.Text;
                        x17.Content = TTT.Text;
                        x18.Content = TTT.Text;
                        x19.Content = TTT.Text;
                        x20.Content = TTT.Text;

                        if (findCouple.combination < 10)
                        {
                            findCouple.motion = 0;
                        }
                        else
                        {
                            congratulations.Visibility = Visibility.Visible;
                            int second, minute;
                            if (DateTime.Now.Second - findCouple.now.Second < 0)
                                second = findCouple.now.Second - DateTime.Now.Second;
                            else
                                second = DateTime.Now.Second - findCouple.now.Second;
                            if (DateTime.Now.Minute - findCouple.now.Minute < 0)
                                minute = findCouple.now.Minute - DateTime.Now.Minute;
                            else
                                minute = DateTime.Now.Minute - findCouple.now.Minute;
                            string min = minute.ToString(); string sec = second.ToString();
                            if (minute < 10)
                            {
                                min = "0" + minute;
                            }
                            if (second < 10)
                                sec = "0" + second;
                            time.Content = min + ":" + sec;
                        }
                    }
                    else
                        findCouple.motion++;
                    break;
                case 2:
                    System.Threading.Thread.Sleep(200);
                    rectangles[findCouple.n1].Visibility = Visibility.Visible;
                    rectangles[findCouple.n2].Visibility = Visibility.Visible;
                    findCouple.motion = 1;
                    findCouple.n1 = n;
                    break;
            }
        }
        private void newGame(object sender, KeyEventArgs e)
        {
            congratulations.Visibility = Visibility.Hidden;
            Game();
        }
    }
    public class MyfindCouple
    {
        public int[] locationOfImages = new int[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public int motion, n1, n2, combination;
        public DateTime now;
        public void newGame()
        {
            combination = 0;
            motion = 0;
            now = DateTime.Now;
            Random random = new Random();
            int n = 10;
            int temp = random.Next(0, 19); ;
            for (int i = 0; i < 20; i++)
                locationOfImages[i] = 0;
            while (n > 0)
            {
                while (locationOfImages[temp] != 0)
                {
                    if (temp < 19)
                        temp++;
                    else
                        temp = 0;
                }
                locationOfImages[temp] = n;
                temp = temp + random.Next(1, 20);
                do
                {
                    if (temp < 19)
                        temp++;
                    else
                        temp = temp - 19;
                } while (locationOfImages[temp] != 0);
                locationOfImages[temp] = n;
                n--;
            }
        }
        public void setPictures(Rectangle[] pictures)
        {
            ImageBrush myBrush;
            for (int i = 0; i < 20; i++)
            {
                myBrush = new ImageBrush();
                BitmapImage bi = new BitmapImage(new Uri("pack://application:,,/Icons/i" + (locationOfImages[i]-1) + ".png"));
                myBrush.ImageSource = bi;
                pictures[i].Fill = myBrush;
            }
        }
        public int Motion(int i)
        {
            switch (motion)
            {
                case 0:
                    n1 = i;
                    break;
                case 1:
                    n2 = i;
                    break;
            }
            return motion;
        }
        public bool checkWithPrevious(int n)
        {
            if (locationOfImages[n1] == locationOfImages[n])
            {
                combination++;
                
                return true;
            }
            else
                return false;
        }
        
    }
}
