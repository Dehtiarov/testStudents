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
        int IndexEdit;
        bool changedFoto = false;
        public WindowAddStudent(int index)
        {
            IndexEdit = index;
            InitializeComponent();
            if (index != -1)
            {
                textBox1.Text = MainWindow.studService.GetAllStudents[index].Name;
                imageBox.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + MainWindow.studService.GetAllStudents[index].ImageSmall));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "Image Files(*.BMP; *.JPG; *.GIF)| *.BMP; *.JPG; *.GIF";
            if (openDlg.ShowDialog() == true)
            {
                patch = openDlg.FileName;
                imageBox.Source = new BitmapImage(new Uri(patch));
                changedFoto = true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text.Length == 0 || imageBox.Source == null)
            {
                MessageBox.Show("Не заповнено поле Ім’я або не додано фото", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string newNameBig = /* @"\Images\"*/ ConfigurationManager.AppSettings["ImagePath"].ToString() + Guid.NewGuid().ToString() + ".jpg";
            string newNameSmall = /* @"\Images\"*/ ConfigurationManager.AppSettings["ImagePath"].ToString() + Guid.NewGuid().ToString() + ".jpg";
            //File.Copy(patch, newName);

            var image = System.Drawing.Image.FromFile(patch);
            var newImageBig = ImageConverter.ImageWorker.ConverImageToBitmap(image, 480, 640);
            if (newImageBig != null)
            {
                newImageBig.Save(Environment.CurrentDirectory + newNameBig, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            var newImageSmall = ImageConverter.ImageWorker.ConverImageToBitmap(image, 134, 156);
            if (newImageSmall != null)
            {
                newImageSmall.Save(Environment.CurrentDirectory + newNameSmall, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            Student newStud = new Student
            {
                Name = textBox1.Text,
                Image = newNameBig,
                ImageSmall = newNameSmall
            };
            if (IndexEdit == -1)
            {
                MainWindow.studService.Add(newStud);
                MainWindow.studService.Save();
                patch = null;
            }
            else
            {
                MainWindow.studService.Edit(newStud, IndexEdit);
            }
        
        DialogResult = true;
        }
}
}
