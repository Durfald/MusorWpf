using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using MusorWpf.Properties;
namespace MusorWpf
{
    /// <summary>
    /// Логика взаимодействия для AddNewCodeWindow.xaml
    /// </summary>
    public partial class AddNewCodeWindow : Window
    {
        public AddNewCodeWindow()
        {
            InitializeComponent();
        }


        SolidColorBrush colorCodeBorder = new SolidColorBrush();
        SolidColorBrush colorCode = new SolidColorBrush();
        SolidColorBrush colorBrush = new SolidColorBrush();
        SolidColorBrush colorAdd = new SolidColorBrush();
        SolidColorBrush colorAddborder = new SolidColorBrush();
        

        private void Close_window_button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Close_window_button_MouseEnter(object sender, MouseEventArgs e)
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

        private void Add_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            #region Animation

            Add.Background = colorAdd;//Тут слой и привязывается к самому элементу(Border'у)
            AddBorder.BorderBrush = colorAddborder;
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
            colorAdd.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorAddborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion


        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            bool added = false;
            MainWindow frmFirst = this.Owner as MainWindow;
            if (frmFirst != null)
            {
                foreach(var i in frmFirst.ComboBox_Product.Items)
                {
                    if(i.GetType()==typeof(string))
                    {
                        if ((string)i == TextBox_Code.Text)
                        {
                            MessageBox.Show("This code already added");
                            added = true;
                            break;
                        }
                    }
                   
                }
                if (!added)
                {
                    frmFirst.ComboBox_Product.Items.Add(TextBox_Code.Text);
                    Settings.Default.Codes.Add(TextBox_Code.Text);
                }
            }
            Close();
        }

        private void Add_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            #region Animation

            Add.Background = colorAdd;//Тут слой и привязывается к самому элементу(Border'у)
            AddBorder.BorderBrush = colorAddborder;
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
            colorAdd.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorAddborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void Add_MouseLeave(object sender, MouseEventArgs e)
        {
            #region Animation

            Add.Background = colorAdd;//Тут слой и привязывается к самому элементу(Border'у)
            AddBorder.BorderBrush = colorAddborder;
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

            ColorAnimation colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(50))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2 = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(50))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorAdd.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorAddborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void Add_MouseEnter(object sender, MouseEventArgs e)
        {
            #region Animation

            Add.Background = colorAdd;//Тут слой и привязывается к самому элементу(Border'у)
            AddBorder.BorderBrush = colorAddborder;
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
            colorAdd.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorAddborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion


        }

        private void TextBox_Code_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox_Code.Text)) TextBox_Code.Text = "Code";
        }

        private void TextBox_Code_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Code.Text == "Code") TextBox_Code.Text = null;
        }

        private void TextBox_Code_MouseLeave(object sender, MouseEventArgs e)
        {
            #region Animation

            TextBox_Code.Background = colorCode;//Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Code.BorderBrush = colorCodeBorder;
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
            colorCode.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorCodeBorder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void TextBox_Code_MouseEnter(object sender, MouseEventArgs e)
        {
            #region Animation

            TextBox_Code.Background = colorCode;//Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Code.BorderBrush = colorCodeBorder;
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
            colorCode.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
            colorCodeBorder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации
            #endregion
        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region Animation
            Close_window_button.Foreground = colorBrush;   //Тут слой и привязывается к самому элементу(Border'у)

            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            byte to_r;
            byte to_g;
            byte to_b;
            byte to_rr;
            byte to_gg;
            byte to_bb;
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


            TextBox_Code.Background = colorCode;//Тут слой и привязывается к самому элементу(Border'у)
            Border_TextBox_Code.BorderBrush = colorCodeBorder;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            to_r = 28;
            to_g = 28;
            to_b = 28;
            to_rr = 69;
            to_gg = 69;
            to_bb = 69;

            BackEase backEasee = new BackEase()  //Формула плавности с отскоком
            {
                Amplitude = 0.3     //Амплитуда, от этого значения зависит насколько сильно будет отскок.
            };
            backEasee.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            ColorAnimation colorAnimationw = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEasee   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2 = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(300))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorCode.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimationw);   //Запуск анимации
            colorCodeBorder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);   //Запуск анимации

            Add.Background = colorAdd;//Тут слой и привязывается к самому элементу(Border'у)
            AddBorder.BorderBrush = colorAddborder;
            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            to_r = 28;
            to_g = 28;
            to_b = 28;
            to_rr = 69;
            to_gg = 69;
            to_bb = 69;



            ColorAnimation colorAnimationa = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromMilliseconds(50))    // Собственно сама анимация
            {
                EasingFunction = backEasee   //Привязывается функция плавности к анимации.
            };
            ColorAnimation colorAnimation2a = new ColorAnimation(Color.FromRgb(to_rr, to_gg, to_bb), TimeSpan.FromMilliseconds(50))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorAdd.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimationa);   //Запуск анимации
            colorAddborder.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2a);   //Запуск анимации
            #endregion

        }
    }
}
