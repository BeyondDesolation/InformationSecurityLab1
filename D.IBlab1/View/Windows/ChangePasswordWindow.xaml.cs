using D.IBlab1.Models;
using D.IBlab1.Services;
using System.Windows;

namespace D.IBlab1.View.Windows
{
    public partial class ChangePasswordWindow : Window
    {
        /// <summary>Пользователь, для которого изменяется пароль </summary>
        public User User { get; }
        public ChangePasswordWindow(User user)
        {
            InitializeComponent();
            User = user;
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {
            if(PasswordHelperService.VerifyPassword(User.Password, User.Salt, pbOldPassword.Password) == false)
            {
                MessageBox.Show("Введен неверный пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(pbNewPassword1.Password.Equals(pbNewPassword2.Password) == false)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (User.PasswordRestriction && PasswordHelperService.IsValidPassword(pbNewPassword1.Password) == false)
            {
                MessageBox.Show(
                    "Пароль должен содержать латинские буквы и знаки арифметических операций (+, -, *, /, =, (, ), %, ^)",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var newSaltandPass = PasswordHelperService.HashPassword(pbNewPassword1.Password);

            User.Salt = newSaltandPass.salt;
            User.Password = newSaltandPass.hashedPass;
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
