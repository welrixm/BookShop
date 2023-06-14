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
using System.Windows.Shapes;
using Storekeeeper.Components;

namespace Storekeeeper.Pages
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

            try

            {

                Message message = new Message()

                {

                    Name = NameTbx.Text,
                    Text = TextTbx.Text,

                    StateMessageId = 1,
                    DateMessage = DateTime.Now,
                    UserId = Navigation.AuthUser.Id

                };

               DBConnect.db.Message.Add(message);

               DBConnect.db.SaveChanges();

               MessageBox.Show("Отправлено");

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);

            }

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CleanBtn_Click(object sender, RoutedEventArgs e)
        {
            NameTbx.Text = "";
            TextTbx.Text = "";
        }
    }
}
