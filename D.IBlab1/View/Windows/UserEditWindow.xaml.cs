using System.Windows;

namespace D.IBlab1.View.Windows
{
    public partial class UserEditWindow : Window
    {
        public string UserName
        {
            get => tbName.Text;
            set => tbName.Text = value;
        }
        public UserEditWindow()
        {
            InitializeComponent();
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Логин не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DialogResult = true;
            Close();
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
