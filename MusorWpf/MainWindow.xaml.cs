using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MusorWpf.Properties;

namespace MusorWpf
{
    internal enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_GRADIENT = 1,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_INVALID_STATE = 4
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy
    {
        public AccentState AccentState;
        public int AccentFlags;
        public int GradientColor;
        public int AnimationId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    internal enum WindowCompositionAttribute
    {
        WCA_ACCENT_POLICY = 19
    }

    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        BigInteger cols = 0;
        public MainWindow()
        {
            InitializeComponent();

        }


        SolidColorBrush colorGift = new SolidColorBrush();
        SolidColorBrush colorRefresh = new SolidColorBrush();

        List<ServiceData.User> GlobalUsers = new List<ServiceData.User>();
        List<ServiceData.User> LocallUsers = new List<ServiceData.User>();

        SolidColorBrush colorGiftborder = new SolidColorBrush();
        SolidColorBrush colorRefreshborder = new SolidColorBrush();
        SolidColorBrush colorBrush = new SolidColorBrush();
        SolidColorBrush TextBox_colorBrush = new SolidColorBrush();
        SolidColorBrush Border_TextBox_colorBrush = new SolidColorBrush();
        SolidColorBrush colorBlurButton = new SolidColorBrush();

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Close_window_Click(object sender, RoutedEventArgs e)
        {
            //if (Settings.Default.Codes.Count == 0)
            //{
            //    foreach (var i in ComboBox_Product.Items)
            //    {
            //        if (i.GetType() == typeof(string))
            //        {
            //            Settings.Default.Codes.Add(i.ToString());
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (var i in ComboBox_Product.Items)
            //    {
            //        if (i.GetType() == typeof(string))
            //        {
            //            if (!Settings.Default.Codes.Contains(i.ToString()))
            //            {
            //                Settings.Default.Codes.Add(i.ToString());
            //            }
            //        }
            //    }
            //}

            //Settings.Default.Save();
            Close();
        }

        private void Close_window_MouseEnter(object sender, MouseEventArgs e)
        {
            #region Animation
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
            #endregion

        }

        private void Close_window_button_MouseLeave(object sender, MouseEventArgs e)
        {
            #region Animation
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
            #endregion

        }
        internal void EnableBlur(AccentState accentState)
        {
            var windowHelper = new WindowInteropHelper(this);
            var accent = new AccentPolicy();
            var accentStructSize = Marshal.SizeOf(accent);
            accent.AccentState = accentState;

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        private async void GetBalance()
        {
            //var q = await Client.GetBalanceAsync(new ServiceData.logger() { logPass = new ServiceData.LogPass() { Login = Settings.Default.Login, Password = Settings.Default.Password } });
            //Balance_TextBox.Text = $"Balance: {q.Score}";


        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            var rect = new Rectangle();
            rect.Width = 20;
            rect.Height = 20;
            rect.Fill = Brushes.Red;

            sp.Children.Add(rect);
            var label=new Label();
            label.Content = "ABOBA";
            sp.Children.Add(label);
            listbox.Items.Add(sp);
            //Client.InnerChannel.OperationTimeout = new TimeSpan(0, 4, 0); // For 30 minute timeout - adjust as necessary
            //string balance = "Balance: ";

            //var ii = Client.GetHelpers(new ServiceData.logger() { logPass = new ServiceData.LogPass() { Login = Settings.Default.Login, Password = Settings.Default.Password } });

            //if (!ii.Seeker)
            //{

            //    Settingbutton.Visibility = Visibility.Collapsed;
            //    ComboBox_FindGifter.Visibility = Visibility.Collapsed;
            //    var i = await Client.GetCodesAsync(new ServiceData.LogPass() { Login = Settings.Default.Login, Password = Settings.Default.Password });
            //    if (!i.Seeker) return;
            //    GetBalance();
            //    Settings.Default.Codes.Clear();
            //    Settings.Default.Codes.AddRange(i.passes[0]._Codes);
            //    ComboBox_Product.ItemsSource = null;
            //    ComboBox_Product.Items.Clear();
            //    ComboBox_Product.ItemsSource = Settings.Default.Codes;
            //    ComboBox_Product.Items.Refresh();


            //}

            //await GetList();
            //ServiceData.SendIndfo date = new ServiceData.SendIndfo();

            //date = Client.GetDate(new ServiceData.logger() { logPass = new ServiceData.LogPass() { Login = Settings.Default.Login, Password = Settings.Default.Password } });


            //if (date.Seeker == false)
            //{
            //    AddText(date.Info, date.Seeker);
            //    return;
            //}
            //Date_TextBox.Text = $"Subscription till: { date.DateSubEnd.ToString("dd.MM.yyyy HH:mm")}";
            //if (Settings.Default.Codes == null) Settings.Default.Codes = new System.Collections.Specialized.StringCollection();

            #region Animation
            TextBox_Nickname.Background = TextBox_colorBrush;//Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Nickname.BorderBrush = Border_TextBox_colorBrush;
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

            to_r = 113;
            to_g = 113;
            to_b = 113;

            ButtonBlur.Foreground = colorBlurButton;   //Тут слой и привязывается к самому элементу(Border'у)

            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться

            to_r = 113;
            to_g = 113;
            to_b = 113;

            ColorAnimation colorAnimatiown = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorBlurButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimatiown);   //Запуск анимации


            GiftButton.Background = colorGift;//Тут слой и привязывается к самому элементу(Border'у)
            GiftBorder.BorderBrush = colorGiftborder;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            to_r = 28;
            to_g = 28;
            to_b = 28;
            to_rr = 69;
            to_gg = 69;
            to_bb = 69;


            colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };

            colorAnimation2 = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorGift.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorGiftborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации


            Close_window_button.Foreground = colorBrush;   //Тут слой и привязывается к самому элементу(Border'у)

            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться

            to_r = 113;
            to_g = 113;
            to_b = 113;



            backEase.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации


            RefreshButton.Background = colorRefresh;//Тут слой и привязывается к самому элементу(Border'у)
            RefreshBorder.BorderBrush = colorRefreshborder;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            to_r = 28;
            to_g = 28;
            to_b = 28;
            to_rr = 69;
            to_gg = 69;
            to_bb = 69;

            backEase.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorAnimation2 = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorRefresh.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorRefreshborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            SetBlur();
            #endregion
        }


        private void TextBox_Nickname_MouseEnter(object sender, MouseEventArgs e)
        {
            #region Animation
            TextBox_Nickname.Background = TextBox_colorBrush;   //Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Nickname.BorderBrush = Border_TextBox_colorBrush;
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
            #endregion
        }

        private void TextBox_Nickname_MouseLeave(object sender, MouseEventArgs e)
        {
            #region Animation
            TextBox_Nickname.Background = TextBox_colorBrush;//Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Nickname.BorderBrush = Border_TextBox_colorBrush;
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
            #endregion
        }

        private void TextBox_Nickname_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Nickname.Text == "Nickname") TextBox_Nickname.Text = null;
        }


        private void GiftButton_MouseLeave(object sender, MouseEventArgs e)
        {
            #region Animation
            GiftButton.Background = colorGift;//Тут слой и привязывается к самому элементу(Border'у)
            GiftBorder.BorderBrush = colorGiftborder;
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
            colorGift.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorGiftborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void GiftButton_MouseEnter(object sender, MouseEventArgs e)
        {
            #region Animation
            GiftButton.Background = colorGift;//Тут слой и привязывается к самому элементу(Border'у)
            GiftBorder.BorderBrush = colorGiftborder;
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
            colorGift.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorGiftborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void RefreshButton_MouseLeave(object sender, MouseEventArgs e)
        {
            #region Animation
            RefreshButton.Background = colorRefresh;//Тут слой и привязывается к самому элементу(Border'у)
            RefreshBorder.BorderBrush = colorRefreshborder;
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
            colorRefresh.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorRefreshborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void RefreshButton_MouseEnter(object sender, MouseEventArgs e)
        {
            #region Animation
            RefreshButton.Background = colorRefresh;//Тут слой и привязывается к самому элементу(Border'у)
            RefreshBorder.BorderBrush = colorRefreshborder;
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
            colorRefresh.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorRefreshborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void GiftButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            #region Animation
            GiftButton.Background = colorGift;//Тут слой и привязывается к самому элементу(Border'у)
            GiftBorder.BorderBrush = colorGiftborder;
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
            colorGift.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorGiftborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void GiftButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            #region Animation
            GiftButton.Background = colorGift;//Тут слой и привязывается к самому элементу(Border'у)
            GiftBorder.BorderBrush = colorGiftborder;
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
            colorGift.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorGiftborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void RefreshButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            #region Animation
            RefreshButton.Background = colorRefresh;//Тут слой и привязывается к самому элементу(Border'у)
            RefreshBorder.BorderBrush = colorRefreshborder;
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
            colorRefresh.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorRefreshborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void RefreshButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            #region Animation
            RefreshButton.Background = colorRefresh;//Тут слой и привязывается к самому элементу(Border'у)
            RefreshBorder.BorderBrush = colorRefreshborder;
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
            colorRefresh.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorRefreshborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void ButtonBlur_Click(object sender, RoutedEventArgs e)
        {
            SetBlur();
        }

        public void SetBlur()
        {
            if (Settings.Default.BlurONOFF == "ON" || string.IsNullOrEmpty(Settings.Default.BlurONOFF))
            {
                ButtonBlur.Content = "ON";
                EnableBlur(AccentState.ACCENT_ENABLE_BLURBEHIND);
                Grid_Left_SColorBrush.Opacity = 0.75;
                Settings.Default.BlurONOFF = "OFF";
            }
            else
            {
                ButtonBlur.Content = "OFF";
                Grid_Left_SColorBrush.Opacity = 1;
                EnableBlur(AccentState.ACCENT_DISABLED);

                Settings.Default.BlurONOFF = "ON";
            }
            Settings.Default.Save();
        }



        private void ButtonBlur_MouseLeave(object sender, MouseEventArgs e)
        {
            #region Animation
            ButtonBlur.Foreground = colorBlurButton;   //Тут слой и привязывается к самому элементу(Border'у)

            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            byte to_r;
            byte to_g;
            byte to_b;

            to_r = 113;
            to_g = 113;
            to_b = 113;


            BackEase backEase = new BackEase()  //Формула плавности с отскоком
            {
                Amplitude = 0.3     //Амплитуда, от этого значения зависит насколько сильно будет отскок.
            };
            backEase.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            ColorAnimation colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorBlurButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            #endregion
        }



        private void TextBox_Nickname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox_Nickname.Text))
            {
                TextBox_Nickname.Text = "Nickname";
            }
        }
        //ServiceData.ServiceDataClient Client = new ServiceData.ServiceDataClient();

        private async void GiftButton_Click(object sender, RoutedEventArgs e)
        {
            //GiftButton.IsEnabled = false;
            //if (!string.IsNullOrEmpty(TextBox_Nickname.Text) && ComboBox_Product.SelectedItem != null)
            //{
            //    AddWaitText();
            //    try
            //    {
            //        var user = new ServiceData.User() { code = ComboBox_Product.SelectedItem.ToString(), Date = DateTime.UtcNow, Nickname = TextBox_Nickname.Text };
            //        Client.InnerChannel.OperationTimeout = new TimeSpan(0, 4, 0); // For 30 minute timeout - adjust as necessary
            //        var a = await Client.SendNicknameAsync(new ServiceData.logger() { logPass = new ServiceData.LogPass() { Login = Settings.Default.Login, Password = Settings.Default.Password }, User = user });
            //        AddText(a.Info, a.Seeker);
            //    }
            //    catch (Exception ee)
            //    {
            //        MessageBox.Show(ee.Message);
            //    }
            //    GetBalance();
            //    await GetList();
            //    DataGrid.Items.Refresh();
            //}
            //else
            //{
            //    MessageBox.Show(" Please fill field");
            //}
            //GiftButton.IsEnabled = true;
        }

        public void AddText(string texts, bool green)
        {
            Ellipse el = new Ellipse();
            el.Width = 10;
            el.Height = 10;
            StackPanel stackPanel2 = new StackPanel() { Orientation = Orientation.Horizontal };
            if (green) el.Fill = Brushes.Green;
            else el.Fill = Brushes.Red;
            stackPanel2.Children.Add(el);
            TextBlock text = new TextBlock() { Text = $" [{++cols}]{texts}", VerticalAlignment = VerticalAlignment.Top };
            stackPanel2.Children.Add(text);
            listbox.Items.Add(stackPanel2);
            listbox.SelectedIndex = listbox.Items.Count - 1;
            listbox.ScrollIntoView(listbox.SelectedItem);

        }
        public void AddWaitText()
        {
            Ellipse el = new Ellipse();
            el.Width = 10;
            el.Height = 10;
            el.Fill = Brushes.Orange;
            StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            stackPanel.Children.Add(el);
            TextBlock text = new TextBlock() { Text = " Waiting server...", VerticalAlignment = VerticalAlignment.Top };
            stackPanel.Children.Add(text);
            listbox.Items.Add(stackPanel);
        }
        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            //RefreshButton.IsEnabled = false;
            //AddWaitText();
            //await GetList();

            //var i = await Client.GetCodesAsync(new ServiceData.LogPass() { Login = Settings.Default.Login, Password = Settings.Default.Password });
            //if (i.Seeker)
            //{
            //    Settings.Default.Codes.Clear();
            //    Settings.Default.Codes.AddRange(i.passes[0]._Codes);
            //    ComboBox_Product.ItemsSource = null;
            //    ComboBox_Product.Items.Clear();
            //    ComboBox_Product.ItemsSource = Settings.Default.Codes;
            //    ComboBox_Product.Items.Refresh();
            //}

            //AddText(" Refreshed", true);
            //RefreshButton.IsEnabled = true;
        }
        static List<string> Gifters = new List<string>();
        public async Task GetList()
        {

            //Gifters.Clear();
            //Gifters.Add("All");
            //Client.InnerChannel.OperationTimeout = new TimeSpan(0, 4, 0); // For 30 minute timeout - adjust as necessary
            //var a = await Client.GetListNicknameAsync(new ServiceData.logger() { logPass = new ServiceData.LogPass() { Login = Settings.Default.Login, Password = Settings.Default.Password } });
            //if (a.Seeker == false)
            //{
            //    AddText(a.Info, a.Seeker);
            //    return;
            //}
            //foreach (var i in a.Users)
            //{
            //    i.Date = TimeZoneInfo.ConvertTimeFromUtc(i.Date, TimeZoneInfo.Local);
            //    if (!Gifters.Contains(i.NicknameSended)) Gifters.Add(i.NicknameSended);
            //}
            //ComboBox_FindGifter.ItemsSource = Gifters;
            //GlobalUsers = a.Users.ToList();
            //DataGrid.ItemsSource = a.Users;
            //ComboBox_FindGifter.SelectedItem = "All";
            //DataGrid.Items.Refresh();


        }

        private void AddNewCode_Click(object sender, RoutedEventArgs e)
        {
            AddNewCodeWindow addNewCodeWindow = new AddNewCodeWindow();
            addNewCodeWindow.Owner = this;
            addNewCodeWindow.ShowDialog();
        }

        private void Settingbutton_Click(object sender, RoutedEventArgs e)
        {
            ComradeWindow comrade = new ComradeWindow();
            comrade.Show();
        }

        private void DeleteCode_Click(object sender, RoutedEventArgs e)
        {
            DeleteCodeWindow deleteCode = new DeleteCodeWindow();
            deleteCode.Owner = this;
            deleteCode.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {


            //Client.InnerChannel.OperationTimeout = new TimeSpan(0, 4, 0); // For 30 minute timeout - adjust as necessary
            //Client.DeleteUser(new ServiceData.LogPass() { Login = Settings.Default.Login, Password = Settings.Default.Password });

        }

        private void TextBox_FindNickname_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_FindNickname_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_FindNickname_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void TextBox_FindNickname_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        static List<ServiceData.User> users = new List<ServiceData.User>();
        private void TextBox_FindNickname_TextChanged(object sender, TextChangedEventArgs e)
        {
            users.Clear();
            if (ComboBox_FindGifter.SelectedItem.ToString() == "All")
            {
                foreach (var i in GlobalUsers)
                {
                    if (i.Nickname.Contains(TextBox_FindNickname.Text)) users.Add(i);
                }
            }
            else
            {
                foreach (var i in GlobalUsers)
                {
                    if (i.Nickname.Contains(TextBox_FindNickname.Text) && i.NicknameSended == ComboBox_FindGifter.SelectedItem.ToString()) users.Add(i);
                }
            }
            DataGrid.ItemsSource = users;
            DataGrid.Items.Refresh();
        }

        private void ComboBox_FindGifter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(ComboBox_FindGifter.SelectedItem.ToString())) return;
            users.Clear();
            if (ComboBox_FindGifter.SelectedItem.ToString() == "All")
            {
                if (!string.IsNullOrEmpty(TextBox_FindNickname.Text))
                {
                    foreach (var i in GlobalUsers)
                    {
                        if (i.Nickname.Contains(TextBox_FindNickname.Text)) users.Add(i);
                    }
                    DataGrid.ItemsSource = users;
                    DataGrid.Items.Refresh();
                    return;
                }
                DataGrid.ItemsSource = GlobalUsers;
                DataGrid.Items.Refresh();
                return;
            }
            if (string.IsNullOrEmpty(TextBox_FindNickname.Text))
            {
                foreach (var i in GlobalUsers)
                {
                    if (i.NicknameSended == ComboBox_FindGifter.SelectedItem.ToString()) users.Add(i);
                }
                DataGrid.ItemsSource = users;
                DataGrid.Items.Refresh();
                return;
            }
            foreach (var i in GlobalUsers)
            {
                if (i.Nickname.Contains(TextBox_FindNickname.Text) && i.NicknameSended == ComboBox_FindGifter.SelectedItem.ToString()) users.Add(i);
            }
            DataGrid.ItemsSource = users;
            DataGrid.Items.Refresh();
        }

        private void ButtonBlur_MouseEnter(object sender, MouseEventArgs e)
        {

            #region Animation
            ButtonBlur.Foreground = colorBlurButton;   //Тут слой и привязывается к самому элементу(Border'у)

            //Переменные для хранения цветов отдельных каналов(красный, зеленый, синий) к которым анимация будет стремиться
            byte to_r;
            byte to_g;
            byte to_b;
            to_r = 163;
            to_g = 163;
            to_b = 163;


            BackEase backEase = new BackEase()  //Формула плавности с отскоком
            {
                Amplitude = 0.3     //Амплитуда, от этого значения зависит насколько сильно будет отскок.
            };
            backEase.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            ColorAnimation colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorBlurButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            #endregion
        }

        private void DataGrid_SelectionChanged()
        {

        }
    }
}