using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for Task.xaml
    /// </summary>
    public partial class Task : Page
    {
        Person person;
        Button[] buttons;
        Brush brush;
        public Task(TextBlock topic, Person person)
        {
            InitializeComponent();
            topic.Text = "Tasks";
            this.person = person;
            buttons = new Button[3]
            {
                BtnList,
                BtnAdd,
                BtnReset
            };
            brush = BtnAdd.Background;
            buttons[1].Background = Brushes.LightGray;
            LRA.Content = new PageAdd(person);
            MessageBox.Show(person.Name);
        }

        public Calendar GetCalendar() => calendar;

        private void BtnList_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e);
            LRA.Content = new PageList(person);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e);
            LRA.Content = new PageAdd(person);
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e);
            LRA.Content = new PageList(person);
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (Button button in buttons)
            {
                button.Background = brush;
            }
            (sender as Button).Background = Brushes.LightGray;
        }
    }
}
