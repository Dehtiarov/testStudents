using Microsoft.Win32;
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
using System.Windows.Shapes;
using DAL;

namespace WpfStudentApp
{
    /// <summary>
    /// Логика взаимодействия для WindowAddStudent.xaml
    /// </summary>
    public partial class WindowAddStudent : Window
    {
        Image imageSmall;
        Image imageBig;
        public WindowAddStudent()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           OpenFileDialog openDlg = new OpenFileDialog();
            if (openDlg.ShowDialog() == true)
            {
                imageBox.Source = new BitmapImage(new Uri(openDlg.FileName));
                imageBig = imageBox;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(textBox1.Text.Length ==0 && imageBox.Source == null)
            {
                MessageBox.Show("Не заповнено поле Ім’я або не додане фото", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Student newStud = new Student
            {
                Name = textBox1.Text,
                Image = Guid.NewGuid().ToString()
            };
            Image.Deco
            MainWindow.studService.Add(newStud);
            MainWindow.studService.Save();
        }
    }
}
