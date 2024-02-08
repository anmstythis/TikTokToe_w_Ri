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
using static System.Net.Mime.MediaTypeNames;

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
            words = new List<string>() { "Я люблю бананы! А Вы?", "Мне очень нравится играть с Вами :)", "Вот бы ещё во что-нибудь поиграть с Вами...",
                "Эх, почему в играх не бывает 2-х победителей :(", "Вот бы Вас познакомить с моим братом...", "Спатеньки охота...zzz..." }; //для пасхалки

            for (int i = 0; i < 9; i++)
            {
                buttons[i].IsEnabled = false;
            }

        }
        private bool YouWin(string move) //для победы
        {
            if ((buttons[0].Content == buttons[1].Content && buttons[1].Content == buttons[2].Content && buttons[1].Content.ToString() == move) || //по горизонтали
                    (buttons[3].Content == buttons[4].Content && buttons[4].Content == buttons[5].Content && buttons[4].Content.ToString() == move) ||
                    (buttons[6].Content == buttons[7].Content && buttons[7].Content == buttons[8].Content && buttons[7].Content.ToString() == move))
            {
                return true;
            }

            else if ((buttons[0].Content == buttons[3].Content && buttons[3].Content == buttons[6].Content && buttons[3].Content.ToString() == move) || //по вертикали
                    (buttons[1].Content == buttons[4].Content && buttons[4].Content == buttons[7].Content && buttons[4].Content.ToString() == move) ||
                    (buttons[2].Content == buttons[5].Content && buttons[5].Content == buttons[8].Content && buttons[5].Content.ToString() == move))
            {
                return true;
            }

            else if ((buttons[0].Content == buttons[4].Content && buttons[4].Content == buttons[8].Content && buttons[4].Content.ToString() == move) || //по диагонали
                    (buttons[2].Content == buttons[4].Content && buttons[4].Content == buttons[6].Content && buttons[4].Content.ToString() == move))
            {
                return true;
            }
            return false;
        }

        private bool YouLose(string move) //для проигрыша
        {
            if ((buttons[0].Content == buttons[1].Content && buttons[1].Content == buttons[2].Content && buttons[1].Content.ToString() == move) || //по горизонтали
                    (buttons[3].Content == buttons[4].Content && buttons[4].Content == buttons[5].Content && buttons[4].Content.ToString() == move) ||
                    (buttons[6].Content == buttons[7].Content && buttons[7].Content == buttons[8].Content && buttons[7].Content.ToString() == move))
            {
                return true;
            }
            else if ((buttons[0].Content == buttons[3].Content && buttons[3].Content == buttons[6].Content && buttons[3].Content.ToString() == move) || //по вертикали
                    (buttons[1].Content == buttons[4].Content && buttons[4].Content == buttons[7].Content && buttons[4].Content.ToString() == move) ||
                    (buttons[2].Content == buttons[5].Content && buttons[5].Content == buttons[8].Content && buttons[5].Content.ToString() == move))
            {
                return true;
            }
            else if ((buttons[0].Content == buttons[4].Content && buttons[4].Content == buttons[8].Content && buttons[4].Content.ToString() == move) || //по диагонали
                    (buttons[2].Content == buttons[4].Content && buttons[4].Content == buttons[6].Content && buttons[4].Content.ToString() == move))
            {
                return true;
            }
            return false;
        }
        private bool Draw() //ничья
        {
            if (buttons[0].IsEnabled == false && buttons[1].IsEnabled == false && buttons[2].IsEnabled == false &&
                buttons[3].IsEnabled == false && buttons[4].IsEnabled == false && buttons[5].IsEnabled == false &&
                buttons[6].IsEnabled == false && buttons[7].IsEnabled == false && buttons[8].IsEnabled == false)
            {
                return true;
            }
            return false;
        }

        private void WindowText(string text) //диалоговое окно
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

        private void RiPlays(string move, string my) //ход робота (Рицу)
        {
            if (buttons.Any(button => button.Content == string.Empty))
            {
                int act;
                do
                {
                    act = rnd.Next(0, 9);
                }
                while (buttons[act].IsEnabled == false);

                buttons[act].Content = move;
                buttons[act].IsEnabled = false;
            }
            if (YouWin(my))
            {
                diawind.Text = "Эх, я проиграла...";
                riconf.Visibility = Visibility.Hidden;
                ristart.Visibility = Visibility.Hidden;
                rilost.Visibility = Visibility.Visible;

                WindowText("ВЫ ВЫИГРАЛИ!");
            }
            else if (YouLose(move))
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
        private void _1_button_Click(object sender, RoutedEventArgs e)
        {
            if (swtcho.IsChecked == true)
            {
                (sender as Button).Content = "o";
                (sender as Button).IsEnabled = false;

                RiPlays("x", "o");
            }
            else if (swtchx.IsChecked == true)
            {
                (sender as Button).Content = "x";
                (sender as Button).IsEnabled = false;

                RiPlays("o", "x");
            }
        }

        private void startgame_Click(object sender, RoutedEventArgs e) //начало игры
        {
            for (int i = 0; i < 9; i++)
            {
                buttons[i].IsEnabled = true;
            }
            startgame.IsEnabled = false;
            startgame.Visibility = Visibility.Hidden;
            resumegame.IsEnabled = true;
            resumegame.Visibility = Visibility.Visible;
            swtchx.Visibility = Visibility.Visible;
            swtcho.Visibility = Visibility.Visible;
            diawind.Text = "Удачи Вам!";
        }

        private void resumegame_Click(object sender, RoutedEventArgs e) //заново
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

        private void swtch_Checked(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены? Если вы нажмете ДА, то текущая игра сотрется.", "Смена игрока", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                for (int i = 0; i < 9; i++)
                {
                    buttons[i].IsEnabled = true;
                    buttons[i].Content = string.Empty;
                }
                diawind.Text = "О, вы решили поменяться со мной местами?...";

                if ((sender as CheckBox) == swtchx) //смена о на х
                {
                    swtcho.IsChecked = false;
                }
                if ((sender as CheckBox) == swtcho) //смена х на о
                {
                    swtchx.IsChecked = false;
                }
            }
            else
            {
                (sender as CheckBox).IsChecked = false;
            }
        }
    }
}