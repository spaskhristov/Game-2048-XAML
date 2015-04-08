using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Game2048XAML.GameLogic;

namespace Game2048XAML
{
    public sealed partial class MainPage : Page
    {
        private const int Rows = 4;
        private const int Cols = 4;
        private const int Tile = 8;
        private static readonly List<TextBox> listTextBoxs = new List<TextBox>();
        private static Status status = Status.PlayGame;

        public MainPage()
        {
            this.InitializeComponent();
            LoadGame();
        }

        private void LoadGame()
        {
            ResetVisibility();
            ResetGame();
            Draw();
        }

        private void ResetVisibility()
        {
            this.endGame.Visibility = Visibility.Collapsed;
            this.gameGrid.Opacity = 1;
            foreach (var childCtrl in gameGrid.Children)
            {
                if (childCtrl is Button)
                {
                    Button btn = (Button)childCtrl;
                    if (btn.Name != "btnPlay" && btn.Name != "btnExit")
                    {
                        btn.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void ResetGame()
        {
            status = Status.PlayGame;
            Game2048.Score = 0;
            Game2048.Matrix = new int[Rows, Cols];
            Game2048.CreateRandomNumber();
            Game2048.CreateRandomNumber();
            Draw();
        }

        private void Draw()
        {
            this.textBlockScore.Text = "Score: " + Game2048.Score.ToString();
            foreach (var childCtrl in gameGrid.Children)
            {
                if (childCtrl is TextBox)
                {
                    TextBox txtbox = (TextBox)childCtrl;
                    listTextBoxs.Add(txtbox);
                }
            }
            int index = 0;
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    if (Game2048.Matrix[row, col] == 0)
                    {
                        listTextBoxs[index].Text = "";
                        listTextBoxs[index].Background = new SolidColorBrush(Colors.Beige);
                    }
                    else if (Game2048.Matrix[row, col] != 0)
                    {
                        listTextBoxs[index].Text = Game2048.Matrix[row, col].ToString();
                        listTextBoxs[index].Background = new SolidColorBrush(Color.FromArgb(120, 255, (byte)Game2048.Matrix[row, col], (byte)(Game2048.Matrix[row, col] * 10)));
                    }
                    index++;
                }
            }
            WhenEndGame();
        }

        private void WhenEndGame()
        {
            int count = 0;
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    if (Game2048.Matrix[row, col] != 0)
                    {
                        count++;
                    }
                    if (Tile == Game2048.Matrix[row, col])
                    {
                        status = Status.YouWin;
                    }
                }
            }
            if (count == Rows * Cols)
            {
                status = Status.GameOver;
            }
            if (status == Status.GameOver || status == Status.YouWin)
            {
                this.endGame.Visibility = Visibility.Visible;
                this.gameGrid.Opacity = 0.8;
                foreach (var childCtrl in gameGrid.Children)
                {
                    if (childCtrl is Button)
                    {
                        Button btn = (Button)childCtrl;
                        if (btn.Name != "btnPlay" && btn.Name != "btnExit")
                        {
                            btn.Visibility = Visibility.Collapsed;
                        }
                    }
                }
                if (status == Status.YouWin)
                {
                    this.endGame.Text = "YOU WIN!!!";
                }
                else if (status == Status.GameOver)
                {
                    this.endGame.Text = "GAME OVER!!!";
                }
            }
        }

        private void Btn_Up_Click(object sender, RoutedEventArgs e)
        {
            Game2048.Move(Direction.Up);
            Draw();
        }

        private void Btn_Down_Click(object sender, RoutedEventArgs e)
        {
            Game2048.Move(Direction.Down);
            Draw();
        }

        private void Btn_Left_Click(object sender, RoutedEventArgs e)
        {
            Game2048.Move(Direction.Left);
            Draw();
        }

        private void Btn_Right_Click(object sender, RoutedEventArgs e)
        {
            Game2048.Move(Direction.Right);
            Draw();
        }

        private void Btn_Play(object sender, RoutedEventArgs e)
        {
            LoadGame();
        }

        private void Btn_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}