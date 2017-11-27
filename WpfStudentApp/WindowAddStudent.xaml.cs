using Microsoft.Win32;
using System;
using System.Configuration;
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
using System.IO;


namespace WpfStudentApp
{
    /// <summary>
    /// Логика взаимодействия для WindowAddStudent.xaml
    /// </summary>
    public partial class WindowAddStudent : Window
    {
        //BitmapImage imageSmall;
        //BitmapImage imageBig;
        string patch = null;
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
                patch = openDlg.FileName;
                //imageBig = new BitmapImage(new Uri(openDlg.FileName));
                //var resized = new TransformedBitmap(imageBig, new ScaleTransform(15 / imageBig.PixelWidth, 15 / imageBig.PixelHeight));

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(textBox1.Text.Length ==0 && imageBox.Source == null)
            {
                MessageBox.Show("Не заповнено поле Ім’я або не додане фото", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string newName = /*ConfigurationManager.AppSettings["ImagePath"].ToString() +*/ Environment.CurrentDirectory + @"\Images\"  + Guid.NewGuid().ToString() + ".jpg";
            File.Copy(patch, newName);
            Student newStud = new Student
            {
                Name = textBox1.Text,
                Image = newName//Guid.NewGuid().ToString() ///////////////////////////////////
            };
            MainWindow.studService.Add(newStud);
            MainWindow.studService.Save();
            DialogResult = true;
        }
    }
}
