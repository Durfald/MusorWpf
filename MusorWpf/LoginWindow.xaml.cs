using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Newtonsoft.Json;
using MusorWpf.Properties;
using System.ServiceModel;
using System.Management;

namespace MusorWpf
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        ServiceData.ServiceDataClient client = new ServiceData.ServiceDataClient();
        SolidColorBrush colorBrush = new SolidColorBrush();
        SolidColorBrush TextBox_colorBrush = new SolidColorBrush();
        SolidColorBrush Border_TextBox_colorBrush = new SolidColorBrush();
        SolidColorBrush TextBox2_colorBrush = new SolidColorBrush();
        SolidColorBrush Border_TextBox2_colorBrush = new SolidColorBrush();
        SolidColorBrush ColorLoginButton = new SolidColorBrush();
        SolidColorBrush ColorBorderLoginButton = new SolidColorBrush();
        private void Close_window_button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public async Task<bool> LoginToServer()
        {
            ManagementObjectCollection mbsList = null;
            ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_processor");
            mbsList = mbs.Get();
            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorID"].ToString();
            }

            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            ManagementObjectCollection moc = mos.Get();
            string serial = "";
            foreach (ManagementObject mo in moc)
            {
                serial = (string)mo["SerialNumber"];
            }
            id = id.Substring(id.Length - 5);
            serial = serial.Substring(serial.Length - 5);
            client.InnerChannel.OperationTimeout = new TimeSpan(0, 4, 0); // For 30 minute timeout - adjust as necessary
            ServiceData.LogPass logPass = new ServiceData.LogPass();
            logPass.Login = TextBox_Login.Text;
            logPass.Password = passbox.Password;
            logPass.HWID = id + serial;

            var a = await client.LoginAsync(logPass);
            if (a.Seeker == false)
            {
                MessageBox.Show(a.Info);
                return false;
            }
            else return true;
        }

        private void Close_window_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Close_window_button.Foreground = colorBrush;   //Тут слой и привязывается к самому элементу(Border'у)

            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            byte to_r;
            byte to_g;
            byte to_b;

            to_r = 237;
            to_g = 66;
            to_b = 69;


            BackEase backEase = new BackEase()  //Формула плавности с отскоком
            {
                Amplitude = 0.3     //Амплитуда, от этого значения зависит насколько сильно будет отскок.
            };
            backEase.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            ColorAnimation colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
        }

        private void Close_window_button_MouseLeave(object sender, MouseEventArgs e)
        {
            Close_window_button.Foreground = colorBrush;   //Тут слой и привязывается к самому элементу(Border'у)

            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            byte to_r;
            byte to_g;
            byte to_b;

            to_r = 113;
            to_g = 113;
            to_b = 113;



            BackEase backEase = new BackEase()  //Формула плавности с отскоком
            {
                Amplitude = 0.01     //Амплитуда, от этого значения зависит насколько сильно будет отскок.
            };
            backEase.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            ColorAnimation colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
        }



        private void TextBox_Password_MouseLeave(object sender, MouseEventArgs e)
        {
            passbox.Background = TextBox_colorBrush;//Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Password.BorderBrush = Border_TextBox_colorBrush;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            byte to_r;
            byte to_g;
            byte to_b;
            byte to_rr;
            byte to_gg;
            byte to_bb;
            to_r = 28;
            to_g = 28;
            to_b = 28;
            to_rr = 69;
            to_gg = 69;
            to_bb = 69;

            BackEase backEase = new BackEase()  //Формула плавности с отскоком
            {
                Amplitude = 0.3     //Амплитуда, от этого значения зависит насколько сильно будет отскок.
            };
            backEase.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            ColorAnimation colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2 = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            TextBox_colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            Border_TextBox_colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
        }

        private void TextBox_Password_MouseEnter(object sender, MouseEventArgs e)
        {
            passbox.Background = TextBox_colorBrush;   //Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Password.BorderBrush = Border_TextBox_colorBrush;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            byte to_r;
            byte to_g;
            byte to_b;
            byte to_rr;
            byte to_gg;
            byte to_bb;
            to_r = 40;
            to_g = 40;
            to_b = 40;
            to_rr = 81;
            to_gg = 81;
            to_bb = 81;

            BackEase backEase = new BackEase()  //Формула плавности с отскоком
            {
                Amplitude = 0.3     //Амплитуда, от этого значения зависит насколько сильно будет отскок.
            };
            backEase.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            ColorAnimation colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2 = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            TextBox_colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            Border_TextBox_colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
        }

        private void TextBox_Login_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBox_Login.Background = TextBox2_colorBrush;   //Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Login.BorderBrush = Border_TextBox2_colorBrush;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            byte to_r;
            byte to_g;
            byte to_b;
            byte to_rr;
            byte to_gg;
            byte to_bb;
            to_r = 40;
            to_g = 40;
            to_b = 40;
            to_rr = 81;
            to_gg = 81;
            to_bb = 81;

            BackEase backEase = new BackEase()  //Формула плавности с отскоком
            {
                Amplitude = 0.3     //Амплитуда, от этого значения зависит насколько сильно будет отскок.
            };
            backEase.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            ColorAnimation colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2 = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            TextBox2_colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            Border_TextBox2_colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации

        }

        private void TextBox_Login_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBox_Login.Background = TextBox2_colorBrush;//Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Login.BorderBrush = Border_TextBox2_colorBrush;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            byte to_r;
            byte to_g;
            byte to_b;
            byte to_rr;
            byte to_gg;
            byte to_bb;
            to_r = 28;
            to_g = 28;
            to_b = 28;
            to_rr = 69;
            to_gg = 69;
            to_bb = 69;

            BackEase backEase = new BackEase()  //Формула плавности с отскоком
            {
                Amplitude = 0.3     //Амплитуда, от этого значения зависит насколько сильно будет отскок.
            };
            backEase.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            ColorAnimation colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2 = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            TextBox2_colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            Border_TextBox2_colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
        }

        private void TextBox_Login_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Login.Text == "Login") TextBox_Login.Text = null;
        }

        private void TextBox_Login_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox_Login.Text)) TextBox_Login.Text = "Login";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox_Login.Background = TextBox2_colorBrush;//Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Login.BorderBrush = Border_TextBox2_colorBrush;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            byte to_r;
            byte to_g;
            byte to_b;
            byte to_rr;
            byte to_gg;
            byte to_bb;
            to_r = 28;
            to_g = 28;
            to_b = 28;
            to_rr = 69;
            to_gg = 69;
            to_bb = 69;

            BackEase backEase = new BackEase()  //Формула плавности с отскоком
            {
                Amplitude = 0.3     //Амплитуда, от этого значения зависит насколько сильно будет отскок.
            };
            backEase.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            ColorAnimation colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2 = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            TextBox2_colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            Border_TextBox2_colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации

            passbox.Background = TextBox_colorBrush;//Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Password.BorderBrush = Border_TextBox_colorBrush;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            to_r = 28;
            to_g = 28;
            to_b = 28;
            to_rr = 69;
            to_gg = 69;
            to_bb = 69;

            ColorAnimation colorAnimation21 = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation22 = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            TextBox_colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation21);   //Запуск анимации
            Border_TextBox_colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation22);   //Запуск анимации


            LoginButton.Background = ColorLoginButton;//Тут слой и привязывается к самому элементу(Border'у)
            LoginBorder.BorderBrush = ColorBorderLoginButton;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            to_r = 28;
            to_g = 28;
            to_b = 28;
            to_rr = 69;
            to_gg = 69;
            to_bb = 69;

            ColorAnimation colorAnimationw = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2w = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorLoginButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimationw);   //Запуск анимации
            ColorBorderLoginButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2w);   //Запуск анимации

            Close_window_button.Foreground = colorBrush;   //Тут слой и привязывается к самому элементу(Border'у)

            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться

            to_r = 113;
            to_g = 113;
            to_b = 113;



            BackEase backEasea = new BackEase()  //Формула плавности с отскоком
            {
                Amplitude = 0.01     //Амплитуда, от этого значения зависит насколько сильно будет отскок.
            };
            backEasea.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            ColorAnimation colorAnimationa = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEasea   //Привязывается функция плавности к анимации.
            };
            colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimationa);   //Запуск анимации


            if (!string.IsNullOrEmpty(Settings.Default.Password)) passbox.Password = Settings.Default.Password;
            if (!string.IsNullOrEmpty(Settings.Default.Login)) TextBox_Login.Text = Settings.Default.Login;
        }

        private void LoginButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginButton.Background = ColorLoginButton;//Тут слой и привязывается к самому элементу(Border'у)
            LoginBorder.BorderBrush = ColorBorderLoginButton;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            byte to_r;
            byte to_g;
            byte to_b;
            byte to_rr;
            byte to_gg;
            byte to_bb;
            to_r = 60;
            to_g = 60;
            to_b = 60;
            to_rr = 101;
            to_gg = 101;
            to_bb = 101;

            BackEase backEase = new BackEase()  //Формула плавности с отскоком
            {
                Amplitude = 0.3     //Амплитуда, от этого значения зависит насколько сильно будет отскок.
            };
            backEase.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            ColorAnimation colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(50))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2 = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(50))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorLoginButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            ColorBorderLoginButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
        }

        private void LoginButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LoginButton.Background = ColorLoginButton;//Тут слой и привязывается к самому элементу(Border'у)
            LoginBorder.BorderBrush = ColorBorderLoginButton;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            byte to_r;
            byte to_g;
            byte to_b;
            byte to_rr;
            byte to_gg;
            byte to_bb;
            to_r = 40;
            to_g = 40;
            to_b = 40;
            to_rr = 81;
            to_gg = 81;
            to_bb = 81;

            BackEase backEase = new BackEase()  //Формула плавности с отскоком
            {
                Amplitude = 0.3     //Амплитуда, от этого значения зависит насколько сильно будет отскок.
            };
            backEase.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            ColorAnimation colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(50))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2 = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(50))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorLoginButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            ColorBorderLoginButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
        }

        private void LoginButton_MouseLeave(object sender, MouseEventArgs e)
        {
            LoginButton.Background = ColorLoginButton;//Тут слой и привязывается к самому элементу(Border'у)
            LoginBorder.BorderBrush = ColorBorderLoginButton;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            byte to_r;
            byte to_g;
            byte to_b;
            byte to_rr;
            byte to_gg;
            byte to_bb;
            to_r = 28;
            to_g = 28;
            to_b = 28;
            to_rr = 69;
            to_gg = 69;
            to_bb = 69;

            BackEase backEase = new BackEase()  //Формула плавности с отскоком
            {
                Amplitude = 0.3     //Амплитуда, от этого значения зависит насколько сильно будет отскок.
            };
            backEase.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            ColorAnimation colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2 = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorLoginButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            ColorBorderLoginButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
        }

        private void LoginButton_MouseEnter(object sender, MouseEventArgs e)
        {
            LoginButton.Background = ColorLoginButton;//Тут слой и привязывается к самому элементу(Border'у)
            LoginBorder.BorderBrush = ColorBorderLoginButton;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            byte to_r;
            byte to_g;
            byte to_b;
            byte to_rr;
            byte to_gg;
            byte to_bb;
            to_r = 40;
            to_g = 40;
            to_b = 40;
            to_rr = 81;
            to_gg = 81;
            to_bb = 81;

            BackEase backEase = new BackEase()  //Формула плавности с отскоком
            {
                Amplitude = 0.3     //Амплитуда, от этого значения зависит насколько сильно будет отскок.
            };
            backEase.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            ColorAnimation colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(50))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2 = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(50))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorLoginButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            ColorBorderLoginButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (await LoginToServer())
                {
                    Settings.Default.Login = TextBox_Login.Text;
                    Settings.Default.Password = passbox.Password;
                    Settings.Default.Save();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
