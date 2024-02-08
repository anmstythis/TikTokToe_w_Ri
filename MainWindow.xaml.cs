using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace крестики_нолики
{
    public partial class MainWindow : Window
    {
        Button[] buttons;
        Random rnd = new Random();
        List<string> words;

        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[9] { _1_button, _2_button, _3_button, _4_button, _5_button, _6_button, _7_button, _8_button, _9_button };
            words = new List<string>() { "Я люблю бананы! А Вы?", "Мне очень нравится играть с Вами :)", "Круто играть за нолики!",
                "Эх, почему в играх не бывает 2-х победителей :(", "Вот бы Вас познакомить с моим братом...", "Спатеньки охота...zzz..." }; //для пасхалки

            for (int i = 0; i < 9; i++)
            {
                buttons[i].IsEnabled = false;
            }

        }

        private bool YouWin()
        {
            if ((buttons[0].Content == buttons[1].Content && buttons[1].Content == buttons[2].Content && buttons[1].Content == "x") || //по горизонтали
                    (buttons[3].Content == buttons[4].Content && buttons[4].Content == buttons[5].Content && buttons[4].Content == "x") ||
                    (buttons[6].Content == buttons[7].Content && buttons[7].Content == buttons[8].Content && buttons[7].Content == "x"))
            {
                return true;
            }

            else if ((buttons[0].Content == buttons[3].Content && buttons[3].Content == buttons[6].Content && buttons[3].Content == "x") || //по вертикали
                    (buttons[1].Content == buttons[4].Content && buttons[4].Content == buttons[7].Content && buttons[4].Content == "x") ||
                    (buttons[2].Content == buttons[5].Content && buttons[5].Content == buttons[8].Content && buttons[5].Content == "x"))
            {
                return true;
            }

            else if ((buttons[0].Content == buttons[4].Content && buttons[4].Content == buttons[8].Content && buttons[4].Content == "x") || //по диагонали
                    (buttons[2].Content == buttons[4].Content && buttons[4].Content == buttons[6].Content && buttons[4].Content == "x"))
            {
                return true;
            }

            return false;
        }

        private bool YouLose()
        {
            if ((buttons[0].Content == buttons[1].Content && buttons[1].Content == buttons[2].Content && buttons[1].Content.ToString() == "o") || //по горизонтали
                    (buttons[3].Content == buttons[4].Content && buttons[4].Content == buttons[5].Content && buttons[4].Content.ToString() == "o") ||
                    (buttons[6].Content == buttons[7].Content && buttons[7].Content == buttons[8].Content && buttons[7].Content.ToString() == "o"))
            {
                return true;
            }
            else if ((buttons[0].Content == buttons[3].Content && buttons[3].Content == buttons[6].Content && buttons[3].Content.ToString() == "o") || //по вертикали
                    (buttons[1].Content == buttons[4].Content && buttons[4].Content == buttons[7].Content && buttons[4].Content.ToString() == "o") ||
                    (buttons[2].Content == buttons[5].Content && buttons[5].Content == buttons[8].Content && buttons[5].Content.ToString() == "o"))
            {
                return true;
            }
            else if ((buttons[0].Content == buttons[4].Content && buttons[4].Content == buttons[8].Content && buttons[4].Content.ToString() == "o") || //по диагонали
                    (buttons[2].Content == buttons[4].Content && buttons[4].Content == buttons[6].Content && buttons[4].Content.ToString() == "o"))
            {
                return true;
            }
            return false;
        }
        private bool Draw()
        {
            if (buttons[0].IsEnabled == false && buttons[1].IsEnabled == false && buttons[2].IsEnabled == false &&
                buttons[3].IsEnabled == false && buttons[4].IsEnabled == false && buttons[5].IsEnabled == false &&
                buttons[6].IsEnabled == false && buttons[7].IsEnabled == false && buttons[8].IsEnabled == false)
            {
                return true;
            }
            return false;
        }

        private void WindowText(string text)
        {
            var result = MessageBox.Show("Продолжить?", text, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                for (int i = 0; i < 9; i++)
                {
                    buttons[i].Content = string.Empty;
                    buttons[i].IsEnabled = true;
                }
                ristart.Visibility = Visibility.Visible;
                riconf.Visibility = Visibility.Hidden;
                rineutral.Visibility = Visibility.Hidden;
                rilost.Visibility = Visibility.Hidden;
                riwon.Visibility = Visibility.Hidden;
                diawind.Text = string.Empty;
            }
            else
            {
                Close();
            }
        }
        private void _1_button_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).Content = "x";
            (sender as Button).IsEnabled = false;

            if (buttons.Any(button => button.Content == string.Empty))
            {
                int act;
                do
                {
                    act = rnd.Next(0, 9);
                }
                while (buttons[act].IsEnabled == false);

                buttons[act].Content = "o";
                buttons[act].IsEnabled = false;
            }
            if (YouWin())
            {
                diawind.Text = "Эх, я проиграла...";
                riconf.Visibility = Visibility.Hidden;
                ristart.Visibility = Visibility.Hidden;
                rilost.Visibility = Visibility.Visible;

                WindowText("ВЫ ВЫИГРАЛИ!");
            }
            else if (YouLose())
            {
                diawind.Text = "Йее, неужели я победила?!";
                riconf.Visibility = Visibility.Hidden;
                ristart.Visibility = Visibility.Hidden;
                riwon.Visibility = Visibility.Visible;

                WindowText("ВЫ ПРОИГРАЛИ!");
            }
            else if (Draw())
            {
                diawind.Text = "О, вышла ничья.";
                riconf.Visibility = Visibility.Hidden;
                ristart.Visibility = Visibility.Hidden;
                rineutral.Visibility = Visibility.Visible;

                WindowText("НИЧЬЯ!");
            }


        }

        private void startgame_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                buttons[i].IsEnabled = true;
            }
            startgame.IsEnabled = false;
            startgame.Visibility = Visibility.Hidden;
            resumegame.IsEnabled = true;
            resumegame.Visibility = Visibility.Visible;
            diawind.Text = "Удачи Вам!";
        }

        private void resumegame_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                buttons[i].IsEnabled = true;
                buttons[i].Content = string.Empty;
            }
            ristart.Visibility = Visibility.Hidden;
            riconf.Visibility = Visibility.Visible;
            diawind.Text = "Ого, Вы сбросили предыдущую попытку...";
        }

        private void ristart_MouseWheel(object sender, MouseWheelEventArgs e) //пасхалка
        {
            if (startgame.IsEnabled == false)
            {
                int say = rnd.Next(0, 6);
                diawind.Text = words[say]; //текст будет меняться при вращении колеса мышки, если курсор наведен на изображение
            }
        }
    }
}