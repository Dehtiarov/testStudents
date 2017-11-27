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
        public static StudentService studService = new StudentService();
        public MainWindow()
        {
            InitializeComponent();
            foreach (var item in studService.GetAllStudents)
            {
                listBox1.Items.Add(item.Name);
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowAddStudent formAdd = new WindowAddStudent();
          
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
                if(studService.GetAllStudents[listBox1.SelectedIndex].Image.Length !=0 )
                    pictureBox1.Source = new BitmapImage(new Uri(studService.GetAllStudents[listBox1.SelectedIndex].Image));
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
                if (textBoxNameMark.Text.Length == 0 && textBoxMark.Text.Length == 0)
                {
                    MessageBox.Show("Не заповнено поле  Предмет або Оцінка", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                Mark tmpMark = new Mark
                {
                    Discipline = textBoxNameMark.Text,
                    Ozinka = short.Parse(textBoxMark.Text)
                };
                //Mark tmpMark = new Mark
                //{
                //    Discipline = "ttt",
                //    Ozinka = 5
                //};
                studService.GetAllStudents[listBox1.SelectedIndex].AddMark(tmpMark);
                studService.Save();
                dataGrid1.ItemsSource = null;
                dataGrid1.ItemsSource = studService.GetAllStudents[listBox1.SelectedIndex].Marks;
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e) // Видалити оцінку для студента
        {

        }
    }
}
