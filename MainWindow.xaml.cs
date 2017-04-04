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
using OiX;

namespace KIK
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool shape = false;
        private int x, y, z, t;
        private int tmp = 6;
        private string tmp2 = "";
        private static int size = 6;
        private bool? chck = null;
        private oix game;
        //MediaElement xSound = new MediaElement();
        //MediaElement oSound = new MediaElement();

        public MainWindow()
        {
            InitializeComponent();
            gameLoad();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D3 || e.Key == Key.NumPad3)
            {
                size = 3;
                gameRestart(); 
            }
            else if (e.Key == Key.D4 || e.Key == Key.NumPad4)
            {
                size = 4;
                gameRestart();
                
            }
            else if (e.Key == Key.D5 || e.Key == Key.NumPad5)
            {
                size = 5;
                gameRestart();
            }
            else if (e.Key == Key.D6 || e.Key == Key.NumPad6)
            {
                size = 6;
                gameRestart();
            }
            else if (e.Key == Key.D7 || e.Key == Key.NumPad7)
            {
                size = 7;
                gameRestart();
            }
            else if (e.Key == Key.D8 || e.Key == Key.NumPad8)
            {
                size = 8;
                gameRestart();
            }
            else if (e.Key == Key.D9 || e.Key == Key.NumPad9)
            {
                size = 9;
                gameRestart();               
            }
          
        }

        private void btClick(object sender, RoutedEventArgs e)
        {
            
            Button bt = (Button)sender;
            if (bt.Content.ToString() == "")
            {
                shape = !shape;

                while (bt.Name[tmp] != '_')
                {
                    tmp2 += bt.Name[tmp];
                    tmp++;
                }
                x = int.Parse(tmp2);
                tmp++;
                tmp2 = "";

                while (bt.Name[tmp] != '_')
                {
                    tmp2 += bt.Name[tmp];
                    tmp++;
                }
                y = int.Parse(tmp2);
                tmp++;
                tmp2 = "";

                while (bt.Name[tmp] != '_')
                {
                    tmp2 += bt.Name[tmp];
                    tmp++;
                }
                z = int.Parse(tmp2);
                tmp++;
                tmp2 = "";

                while (tmp < bt.Name.Length)
                {
                    tmp2 += bt.Name[tmp];
                    tmp++;
                }
                t = int.Parse(tmp2);
                tmp++;
                tmp2 = "";
                tmp = 6;
                if (game.insert(x, y, z, t, shape))
                {
                    if (shape == true)
                    {
                        bt.Content = "X";
                    }
                    else
                    {
                        bt.Content = "O";

                    }
                }
                chck = game.check();
                if (chck == true)
                {
                    MessageBox.Show("Wygrał X");
                    
                    gameRestart();
                }
                else if (chck == false)
                {
                    MessageBox.Show("Wygrał O");
                    gameRestart();
                }
            }
            
        }
        public void gameLoad()
        {
            createGrid();
            game = new oix(size);
            string name;
            int left;
            int top;

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    for (int k = 0; k < size; k++)
                        for (int o = 0; o < size; o++)
                        {

                            name = "button" + i + "_" + j + "_" + k + "_" + o;
                            Button bt = new Button();

                            bt.Name = name;
                            bt.Width = 20;
                            bt.Height = 20;
                            left = 1 + k + (size + 1) * i;
                            top = 1 + o + (size + 1) * j;
                            gameBoard.Children.Add(bt);
                            Grid.SetRow(bt, top);
                            Grid.SetColumn(bt, left);
                            bt.Click += btClick;
                            bt.Content = "";
                        }
        
        }

        public void createGrid()
        {
            for (int i = 0; i < size * (size + 1); i++)
            {
                RowDefinition rowdef = new RowDefinition();
                rowdef.Height = new GridLength(20);
                gameBoard.RowDefinitions.Add(rowdef);

                ColumnDefinition coldef = new ColumnDefinition();
                coldef.Width = new GridLength(20);
                gameBoard.ColumnDefinitions.Add(coldef);
            }
        }

        public void gameRestart()
        {
            shape = false;
            chck = null;
            gameBoard.Children.Clear();
            gameLoad();
            
        }

        
        
        
        
    }
}
