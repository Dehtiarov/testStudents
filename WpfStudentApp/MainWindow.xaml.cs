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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DAL;

namespace WpfStudentApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<string> predmets;
        public static StudentService studService = new StudentService();
        public MainWindow()
        {
            InitializeComponent();
            foreach (var item in studService.GetAllStudents)
            {
                listBox1.Items.Add(item.Name);
            }
            predmets = new List<string>();
            predmets.Add("Програмування");
            predmets.Add("Фізика");
            predmets.Add("Хімія");
            predmets.Add("Математика");
            foreach (var item in predmets)
            {
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedIndex = 0;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowAddStudent formAdd = new WindowAddStudent(-1);
          
            if (formAdd.ShowDialog() == true)
            {
                listBox1.Items.Clear();
                foreach (var item in studService.GetAllStudents)
                {
                    listBox1.Items.Add(item.Name);
                }
            }
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                // labelName.Content = studService[listBox1.SelectedIndex].Name;
                labelID.Content = studService.GetAllStudents[listBox1.SelectedIndex].Id;
                labelName.Content = studService.GetAllStudents[listBox1.SelectedIndex].Name;
                if (!String.IsNullOrEmpty(studService.GetAllStudents[listBox1.SelectedIndex].ImageSmall))
                {
                    //if(studService.GetAllStudents[listBox1.SelectedIndex].ImageSmall.Length !=0 )
                    try
                    {
                        pictureBox1.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + studService.GetAllStudents[listBox1.SelectedIndex].ImageSmall));
                    }
                    catch { }
                }
                    dataGrid1.ItemsSource = null;
                //foreach (var item in studService.GetAllStudents[listBox1.SelectedIndex].Marks)
                //{
                //    dataGrid1.Items.Add(item);
                //}
                dataGrid1.ItemsSource = studService.GetAllStudents[listBox1.SelectedIndex].Marks;
                
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // Видалити студента
        {
            if (listBox1.SelectedIndex != -1)
            {
                studService.Delete(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
           // studService.GetAllStudents[listBox1.SelectedIndex]
        }

        private void button4_Click(object sender, RoutedEventArgs e) // Додати оцінку для студента
        {
            if (listBox1.SelectedIndex != -1)
            {
                if (comboBox1.SelectedIndex == -1 || textBoxMark.Text.Length == 0)
                {
                    MessageBox.Show("Не вибрано предмет або Оцінка", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                Mark tmpMark = new Mark
                {
                    Discipline = comboBox1.Text,
                    Ozinka = short.Parse(textBoxMark.Text)
                };
                studService.GetAllStudents[listBox1.SelectedIndex].AddMark(tmpMark);
                studService.Save();
                dataGrid1.ItemsSource = null;
                dataGrid1.ItemsSource = studService.GetAllStudents[listBox1.SelectedIndex].Marks;
            }
        }

              private void pictureBox1_MouseEnter(object sender, MouseEventArgs e)
        {
            //if (!String.IsNullOrEmpty(studService.GetAllStudents[listBox1.SelectedIndex].Image))
            //{
            //    try
            //    {
            //        pictureBox2.Height = 640;
            //        pictureBox2.Width = 480;
            //        pictureBox2.Visibility = Visibility.Visible;
            //        pictureBox2.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + studService.GetAllStudents[listBox1.SelectedIndex].Image));
            //    }
            //    catch { }
            //}
        }

            private void pictureBox1_MouseLeave(object sender, MouseEventArgs e)
        {
           // pictureBox2.Visibility = Visibility.Hidden;
        }

        private void pictureBox1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!String.IsNullOrEmpty(studService.GetAllStudents[listBox1.SelectedIndex].Image))
            {
                try
                {
                    Window1 wnd = new Window1();
                    wnd.imageBox1.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + studService.GetAllStudents[listBox1.SelectedIndex].Image));
                    //wnd.Height = wnd.imageBox1.Height;
                    //wnd.Width = wnd.imageBox1.Width;
                    wnd.ShowDialog();
                }
                catch { }
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e) // Редагування
        {
            if (listBox1.SelectedIndex != -1)
            {
                WindowAddStudent formEdit = new WindowAddStudent(listBox1.SelectedIndex);
                formEdit.Title = "Редагування студента";
                if (formEdit.ShowDialog() == true)
                {
                    listBox1.Items.Clear();
                    foreach (var item in studService.GetAllStudents)
                    {
                        listBox1.Items.Add(item.Name);
                    }
                }
            }
        }
    }
}
