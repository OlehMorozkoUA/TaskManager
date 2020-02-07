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
    /// Interaction logic for Registrate.xaml
    /// </summary>
    public partial class Registrate : Page
    {
        private ListPerson people = new ListPerson();
        private Person person;
        public Registrate(TextBlock topic)
        {
            InitializeComponent();
            topic.Text = "Registrate";
        }

        private void BtnRegistrate_Click(object sender, RoutedEventArgs e)
        {
            if (people.IsExist(TextName.Text))
            {
                TextWarningName.Visibility = Visibility;
                BorderText.Background = Brushes.LightGray;
                return;
            }
            if (Password.Password == PasswordConfim.Password)
            {
                BorderText.Background = (Brush)ColorConverter.ConvertFromString("#FFF5F4F4");
                BorderPasswordConfim.Background = (Brush)ColorConverter.ConvertFromString("#FFF5F4F4");
                person = new Person()
                {
                    Name = TextName.Text,
                    Password = Password.Password
                };

                people.AddPerson(person);
            }
            else
            {
                TextWarning.Visibility = Visibility;
                BorderPasswordConfim.Background = Brushes.LightGray;
            }
        }
    }
}
