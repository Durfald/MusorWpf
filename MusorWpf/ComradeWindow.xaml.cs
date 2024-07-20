using MusorWpf.Properties;
using System;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MusorWpf
{



    public partial class ComradeWindow : Window
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        BigInteger cols = 0;
        public ComradeWindow()
        {
            InitializeComponent();
        }
        SolidColorBrush TextBox_colorBrush = new SolidColorBrush();
        SolidColorBrush Border_TextBox_colorBrush = new SolidColorBrush();
        SolidColorBrush TextBox_NewAcc = new SolidColorBrush();
        SolidColorBrush Border_NewAccborder = new SolidColorBrush();
        SolidColorBrush colorBlurButton = new SolidColorBrush();
        SolidColorBrush colorBrush = new SolidColorBrush();
        SolidColorBrush colorGift = new SolidColorBrush();
        SolidColorBrush colorGiftborder = new SolidColorBrush();
        SolidColorBrush colortextboxcode = new SolidColorBrush();
        SolidColorBrush colorbordercode = new SolidColorBrush();
        SolidColorBrush colorDelete = new SolidColorBrush();
        SolidColorBrush colorDeleteBorder = new SolidColorBrush();
        SolidColorBrush colorbalance = new SolidColorBrush();
        SolidColorBrush colorbalanceButtonBorder = new SolidColorBrush();
        SolidColorBrush colorDeleteButton = new SolidColorBrush();
        SolidColorBrush colorDeleteButtonBorder = new SolidColorBrush();
        ServiceData.ServiceDataClient client = new ServiceData.ServiceDataClient();
        public void AddText(string texts, bool green)
        {
            Ellipse el = new Ellipse();
            el.Width = 10;
            el.Height = 10;
            StackPanel stackPanel2 = new StackPanel() { Orientation = Orientation.Horizontal };
            //if (texts == "PRZ")
            //{
            //    MessageBox.Show("Server send error");
            //    return;
            //}
            //if (texts == " Unknown user") el.Fill = Brushes.Red;
            //if (texts == " No subscribtion") el.Fill = Brushes.Red;
            //if (texts == " Access denied") el.Fill = Brushes.Red;
            //if (texts == " This code is already added") el.Fill = Brushes.Red;
            //if (texts == " Not found code") el.Fill = Brushes.Red;
            //if (texts == " Not found user") el.Fill = Brushes.Red;
            //if (texts == " Successfully deleted account") el.Fill = Brushes.Green;
            //if (texts == " Successfully removed code") el.Fill = Brushes.Green;
            //if (texts == " Successfully added code") el.Fill = Brushes.Green;
            //if (texts == " Refreshed") el.Fill = Brushes.Green;
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

        private void Close_window_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.Save();
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
        private void TextBox_Code_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Code.Text == "Code") TextBox_Code.Text = null;
        }

        private void TextBox_Code_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox_Code.Text))
            {
                TextBox_Code.Text = "Code";
            }
        }

        private void TextBox_Code_MouseLeave(object sender, MouseEventArgs e)
        {
            #region Animation
            TextBox_Code.Background = colortextboxcode;   //Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Code.BorderBrush = colorbordercode;
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
            colortextboxcode.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorbordercode.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void TextBox_Code_MouseEnter(object sender, MouseEventArgs e)
        {
            #region Animation
            TextBox_Code.Background = colortextboxcode;   //Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Code.BorderBrush = colorbordercode;
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
            colortextboxcode.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorbordercode.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void GiveButton_MouseLeave(object sender, MouseEventArgs e)
        {
            #region Animation
            GiveButton.Background = colorGift;//Тут слой и привязывается к самому элементу(Border'у)
            GiveBorder.BorderBrush = colorGiftborder;
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
        public async void RefreshHelpers()
        {

            client.InnerChannel.OperationTimeout = new TimeSpan(0, 4, 0); // For 30 minute timeout - adjust as necessary
            var a = await client.GetHelpersAsync(new ServiceData.logger() { logPass = new ServiceData.LogPass() { Login = Settings.Default.Login, Password = Settings.Default.Password } });
            if (a.Seeker == false)
            {
                AddText(a.Info, a.Seeker);
                return;
            }
            foreach (var i in a.passes)
            {
                foreach (var ii in i._Codes)
                {
                    if (i._Codes.First() == ii) i.Codes += ii;
                    else i.Codes += $", {ii}";
                }
            }
            DataGrid.ItemsSource = a.passes;


        }
        private void GiveButton_MouseEnter(object sender, MouseEventArgs e)
        {
            #region Animation
            GiveButton.Background = colorGift;//Тут слой и привязывается к самому элементу(Border'у)
            GiveBorder.BorderBrush = colorGiftborder;
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
        private async void GiveButton_Click(object sender, RoutedEventArgs e)
        {
            GiveButton.IsEnabled = false;

            AddWaitText();
            bool q = false;
            if (TextBox_Code.Text != "Code")
            {
                ServiceData.Helper helper = new ServiceData.Helper() { Login = TextBox_Nickname.Text, Code = TextBox_Code.Text };
                client.InnerChannel.OperationTimeout = new TimeSpan(0, 4, 0); // For 30 minute timeout - adjust as necessary
                var a = await client.AddCodetoHelperAsync(new ServiceData.logger() { Helper = helper, logPass = new ServiceData.LogPass() { Login = Settings.Default.Login, Password = Settings.Default.Password } });
                AddText(a.Info, a.Seeker);
                q = a.Seeker;
            }
            if (int.TryParse(TextBox_Balance.Text, out int num) && !string.IsNullOrEmpty(TextBox_Balance.Text))
            {
                ServiceData.Helper helper = new ServiceData.Helper() { Login = TextBox_Nickname.Text, Code = TextBox_Code.Text, Score = num };
                var o = await client.ChangeBalanceAsync(new ServiceData.logger() { logPass = new ServiceData.LogPass() { Login = Settings.Default.Login, Password = Settings.Default.Password }, Helper = helper });
                AddText(o.Info, o.Seeker);
                q = o.Seeker;
            }
            if (q) RefreshHelpers();
            GiveButton.IsEnabled = true;
        }

        private void GiveButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            #region Animation
            GiveButton.Background = colorGift;//Тут слой и привязывается к самому элементу(Border'у)
            GiveBorder.BorderBrush = colorGiftborder;
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

        private void GiveButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            #region Animation
            GiveButton.Background = colorGift;//Тут слой и привязывается к самому элементу(Border'у)
            GiveBorder.BorderBrush = colorGiftborder;
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

        private void TextBox_Nickname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox_Nickname.Text))
            {
                TextBox_Nickname.Text = "Nickname";
            }
        }

        private void NewAccButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            #region Animation
            NewAccButton.Background = TextBox_NewAcc;//Тут слой и привязывается к самому элементу(Border'у)
            NewAccBorder.BorderBrush = Border_NewAccborder;
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
            TextBox_NewAcc.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            Border_NewAccborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void NewAccButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            #region Animation
            NewAccButton.Background = TextBox_NewAcc;//Тут слой и привязывается к самому элементу(Border'у)
            NewAccBorder.BorderBrush = Border_NewAccborder;
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
            TextBox_NewAcc.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            Border_NewAccborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
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

        private void ButtonBlur_MouseEnter(object sender, MouseEventArgs e)
        {
            #region Animation
            ButtonBlur.Foreground = colorBlurButton;   //Тут слой и привязывается к самому элементу(Border'у)

            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            byte to_r;
            byte to_g;
            byte to_b;
            //113
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

        private void NewAccButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewComradeWindow addNewComrade = new AddNewComradeWindow(this);
            addNewComrade.ShowDialog();
        }

        private void NewAccButton_MouseEnter(object sender, MouseEventArgs e)
        {
            #region Animation
            NewAccButton.Background = TextBox_NewAcc;//Тут слой и привязывается к самому элементу(Border'у)
            NewAccBorder.BorderBrush = Border_NewAccborder;
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
            TextBox_NewAcc.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            Border_NewAccborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void NewAccButton_MouseLeave(object sender, MouseEventArgs e)
        {
            #region Animation
            NewAccButton.Background = TextBox_NewAcc;//Тут слой и привязывается к самому элементу(Border'у)
            NewAccBorder.BorderBrush = Border_NewAccborder;
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
            TextBox_NewAcc.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            Border_NewAccborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void DeleteButton_MouseLeave(object sender, MouseEventArgs e)
        {
            #region Animation
            DeleteButton.Background = colorDelete;//Тут слой и привязывается к самому элементу(Border'у)
            DeleteBorder.BorderBrush = colorDeleteBorder;
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
            colorDelete.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorDeleteBorder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void DeleteButton_MouseEnter(object sender, MouseEventArgs e)
        {
            #region Animation
            DeleteButton.Background = colorDelete;//Тут слой и привязывается к самому элементу(Border'у)
            DeleteBorder.BorderBrush = colorDeleteBorder;
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
            colorDelete.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorDeleteBorder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void DeleteButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            #region Animation
            DeleteButton.Background = colorDelete;//Тут слой и привязывается к самому элементу(Border'у)
            DeleteBorder.BorderBrush = colorDeleteBorder;
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
            colorDelete.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorDeleteBorder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void DeleteButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            #region Animation
            DeleteButton.Background = colorDelete;//Тут слой и привязывается к самому элементу(Border'у)
            DeleteBorder.BorderBrush = colorDeleteBorder;
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
            colorDelete.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorDeleteBorder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            AddWaitText();
            ServiceData.Helper helper = new ServiceData.Helper() { Login = TextBox_Nickname.Text, Code = TextBox_Code.Text };
            client.InnerChannel.OperationTimeout = new TimeSpan(0, 4, 0); // For 30 minute timeout - adjust as necessary
            var a = await client.RemoveCodeHelperAsync(new ServiceData.logger() { Helper = helper, logPass = new ServiceData.LogPass() { Login = Settings.Default.Login, Password = Settings.Default.Password } });
            AddText(a.Info, a.Seeker);
            if (a.Seeker == false) return;
            RefreshHelpers();

        }

        private void DeleteAccButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            #region Animation
            DeleteAccButton.Background = colorDeleteButton;//Тут слой и привязывается к самому элементу(Border'у)
            DeleteAccBorder.BorderBrush = colorDeleteButtonBorder;
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
            colorDeleteButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorDeleteButtonBorder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void DeleteAccButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            #region Animation
            DeleteAccButton.Background = colorDeleteButton;//Тут слой и привязывается к самому элементу(Border'у)
            DeleteAccBorder.BorderBrush = colorDeleteButtonBorder;
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
            colorDeleteButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorDeleteButtonBorder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private async void DeleteAccButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (TextBox_Nickname.Text == "Nickname" || string.IsNullOrEmpty(TextBox_Nickname.Text))
            {
                MessageBox.Show("Please fill field");
                return;
            }
            DeleteAccButton.IsEnabled = false;

            AddWaitText();
            ServiceData.Helper helper = new ServiceData.Helper() { Login = TextBox_Nickname.Text };
            client.InnerChannel.OperationTimeout = new TimeSpan(0, 4, 0); // For 30 minute timeout - adjust as necessary
            var a = await client.DeleteHelperAccAsync(new ServiceData.logger() { Helper = helper, logPass = new ServiceData.LogPass() { Login = Settings.Default.Login, Password = Settings.Default.Password } });
            AddText(a.Info, a.Seeker);
            if (a.Seeker == false)
            {
                return;
            }
            RefreshHelpers();
            DeleteAccButton.IsEnabled = true;
        }

        private void DeleteAccButton_MouseEnter(object sender, MouseEventArgs e)
        {
            #region Animation
            DeleteAccButton.Background = colorDeleteButton;//Тут слой и привязывается к самому элементу(Border'у)
            DeleteAccBorder.BorderBrush = colorDeleteButtonBorder;
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
            colorDeleteButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorDeleteButtonBorder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void DeleteAccButton_MouseLeave(object sender, MouseEventArgs e)
        {
            #region Animation
            DeleteAccButton.Background = colorDeleteButton;//Тут слой и привязывается к самому элементу(Border'у)
            DeleteAccBorder.BorderBrush = colorDeleteButtonBorder;
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
            colorDeleteButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorDeleteButtonBorder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshHelpers();
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

            TextBox_Code.Background = colortextboxcode;   //Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Code.BorderBrush = colorbordercode;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            byte to_rr;
            byte to_gg;
            byte to_bb;
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
            colortextboxcode.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimationw);   //Запуск анимации
            colorbordercode.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2w);   //Запуск анимации

            GiveButton.Background = colorGift;//Тут слой и привязывается к самому элементу(Border'у)
            GiveBorder.BorderBrush = colorGiftborder;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            to_r = 28;
            to_g = 28;
            to_b = 28;
            to_rr = 69;
            to_gg = 69;
            to_bb = 69;

            ColorAnimation colorAnimationq = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2q = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorGift.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimationq);   //Запуск анимации
            colorGiftborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2q);   //Запуск анимации

            TextBox_Nickname.Background = TextBox_colorBrush;//Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Nickname.BorderBrush = Border_TextBox_colorBrush;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            to_r = 28;
            to_g = 28;
            to_b = 28;
            to_rr = 69;
            to_gg = 69;
            to_bb = 69;


            ColorAnimation colorAnimatione = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2e = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            TextBox_colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimatione);   //Запуск анимации
            Border_TextBox_colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2e);   //Запуск анимации

            ButtonBlur.Foreground = colorBlurButton;   //Тут слой и привязывается к самому элементу(Border'у)


            to_r = 113;
            to_g = 113;
            to_b = 113;


            ColorAnimation colorAnimationa = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorBlurButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimationa);   //Запуск анимации

            NewAccButton.Background = TextBox_NewAcc;//Тут слой и привязывается к самому элементу(Border'у)
            NewAccBorder.BorderBrush = Border_NewAccborder;

            to_r = 28;
            to_g = 28;
            to_b = 28;
            to_rr = 69;
            to_gg = 69;
            to_bb = 69;


            ColorAnimation colorAnimations = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2s = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            TextBox_NewAcc.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimations);   //Запуск анимации
            Border_NewAccborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2s);   //Запуск анимации

            DeleteButton.Background = colorDelete;//Тут слой и привязывается к самому элементу(Border'у)
            DeleteBorder.BorderBrush = colorDeleteBorder;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            to_r = 28;
            to_g = 28;
            to_b = 28;
            to_rr = 69;
            to_gg = 69;
            to_bb = 69;


            ColorAnimation colorAnimationz = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2z = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorDelete.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimationz);   //Запуск анимации
            colorDeleteBorder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2z);   //Запуск анимации

            DeleteAccButton.Background = colorDeleteButton;//Тут слой и привязывается к самому элементу(Border'у)
            DeleteAccBorder.BorderBrush = colorDeleteButtonBorder;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            to_r = 28;
            to_g = 28;
            to_b = 28;
            to_rr = 69;
            to_gg = 69;
            to_bb = 69;


            ColorAnimation colorAnimationx = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2x = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorDeleteButton.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimationx);   //Запуск анимации
            colorDeleteButtonBorder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2x);   //Запуск анимации

            TextBox_Balance.Background = colorbalance;//Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Balance.BorderBrush = colorbalanceButtonBorder;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            to_r = 28;
            to_g = 28;
            to_b = 28;
            to_rr = 69;
            to_gg = 69;
            to_bb = 69;



            ColorAnimation colorAnimationba = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2ba = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorbalance.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimationba);   //Запуск анимации
            colorbalanceButtonBorder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2ba);   //Запуск анимации
            #endregion
        }

        private void TextBox_Balance_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Balance.Text == "Balance") TextBox_Balance.Text = "";
        }

        private void TextBox_Balance_MouseLeave(object sender, MouseEventArgs e)
        {
            #region Animation
            TextBox_Balance.Background = colorbalance;//Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Balance.BorderBrush = colorbalanceButtonBorder;
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
            colorbalance.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorbalanceButtonBorder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void TextBox_Balance_MouseEnter(object sender, MouseEventArgs e)
        {
            #region Animation
            TextBox_Balance.Background = colorbalance;//Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Balance.BorderBrush = colorbalanceButtonBorder;
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
            colorbalance.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorbalanceButtonBorder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void TextBox_Balance_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox_Balance.Text))
            {
                TextBox_Balance.Text = "Balance";
            }
        }
    }
}
