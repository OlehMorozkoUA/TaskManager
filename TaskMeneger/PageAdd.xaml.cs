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
    /// Interaction logic for PageAdd.xaml
    /// </summary>
    public partial class PageAdd : Page
    {
        Tasks Tasks { get; set; } = new Tasks();
        public PageAdd(Person person)
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string richText = new TextRange(docBox.Document.ContentStart, docBox.Document.ContentEnd).Text;
            Tasks.AddTask(new TaskItem { DateTime = DateTime.Now, Title = TextTitle.Text, Task = richText, PersonId = per });
        }
    }
}
