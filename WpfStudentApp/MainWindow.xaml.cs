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
            studService.
            labelID = studService[listBox1.SelectedIndex].

            
        }
    }
}
