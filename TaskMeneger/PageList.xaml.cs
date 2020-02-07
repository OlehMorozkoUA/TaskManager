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

namespace TaskMeneger
{
    /// <summary>
    /// Interaction logic for PageList.xaml
    /// </summary>
    public partial class PageList : Page
    {
        Tasks Tasks { get; set; } = new Tasks();
        public PageList(Person person)
        {
            InitializeComponent();
            MessageBox.Show(Tasks.tasks.Count.ToString());
            List<ListItemTask> tasks = new List<ListItemTask>();
            foreach(var task in Tasks.GetTasksById(person.Id))
            {
                tasks.Add(new ListItemTask { Id = task.Id, Time = task.DateTime.ToString("hh:mm:ss"), Title = task.Title });
            }
            listView.ItemsSource = tasks;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class ListItemTask
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string Title { get; set; }
    }
}
