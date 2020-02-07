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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        TextBlock topic;
        Frame frame;
        Button button;
        public Login(TextBlock topic, Frame frame, Button button)
        {
            InitializeComponent();

            topic.Text = "Login";
            this.topic = topic;
            this.frame = frame;
            this.button = button;

            this.button.IsEnabled = false;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            ListPerson people = new ListPerson();
            if (people.Login(TextName.Text, Password.Password))
            {
                frame.Content = new Account(topic, people.FindPerson(TextName.Text, Password.Password));
                button.IsEnabled = true;
            }
        }
    }
}
