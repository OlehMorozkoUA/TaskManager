using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : Page
    {
        private bool isEdit = true;
        private string FilePath { get; set; }
        private string FileName { get; set; }
        private string CopyTo { get; set; }
        private int Id { get; set; }
        string[] extensions =
                    {
                        "jpg",
                        "jpeg",
                        "png",
                        "ico"
                    };
        ListPerson people = new ListPerson();
        Person person;
        public Account(TextBlock topic, Person person)
        {
            InitializeComponent();
            topic.Text = "Account";
            this.person = person;

            Id = person.Id;
            TextName.Text = person.Name;
            TextEmail.Text = person.Email;
            TextPhone.Text = person.Phone;
            TextStatus.Text = person.Status;
            if (person.Image != "")
            {
                Photo.Source = new BitmapImage(new Uri(person.Image));
            }
        }

        public Person GetPerson()
        {
            return person;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                TextName.IsEnabled = true;
                TextEmail.IsEnabled = true;
                TextPhone.IsEnabled = true;
            }
            else
            {
                TextName.IsEnabled = false;
                TextEmail.IsEnabled = false;
                TextPhone.IsEnabled = false;


                people.UpdateById(person.Id, new Person(person.Id, TextName.Text, TextEmail.Text, TextPhone.Text, "User", person.Image, person.Password));
            }

            isEdit = !isEdit;
        }

        private void BtnBrowser_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                FileName = System.IO.Path.GetFileName(FilePath);
            }

            if (FileName != null)
            {
                bool isGoodExtension = false;
                for (int i = 0; i < extensions.Length; i++)
                {
                    if (extensions[i] == Extension(FileName)) isGoodExtension = true;
                }
                if (isGoodExtension)
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Photos");
                    CopyTo = $@"{Directory.GetCurrentDirectory()}\Photos\{Id}.{Extension(FileName)}";
                    try
                    {
                        bool isEmpty = true;
                        foreach (string extension in extensions)
                        {
                            if (File.Exists($@"{Directory.GetCurrentDirectory()}\Photos\{Id}.{extension}"))
                            {
                                File.Delete($@"{Directory.GetCurrentDirectory()}\Photos\{Id}.{extension}");
                                File.Copy(FilePath, $@"{Directory.GetCurrentDirectory()}\Photos\{Id}.{extension}");
                                Photo.Source = new BitmapImage(new Uri(FilePath));
                                CopyTo = $@"{Directory.GetCurrentDirectory()}\Photos\{Id}.{extension}";
                                isEmpty = false;
                            }
                        }
                        if(isEmpty)
                        {
                            File.Copy(FilePath, CopyTo);
                            Photo.Source = new BitmapImage(new Uri(FilePath));
                        }
                        people.AddImageForPerson(Id, CopyTo);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Extension is not good!");
                }
            }
        }

        private string Extension(string fileName)
        {
            Regex regex = new Regex(@"^[\w,\s-]+\.([A-Za-z]{3})$");

            Match match = regex.Match(fileName);

            string extension = match.Groups[1].Value;

            return extension;
        }
    }
}
