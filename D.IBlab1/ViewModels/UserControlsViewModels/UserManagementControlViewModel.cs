using D.IBlab1.Data.Storages;
using D.IBlab1.Infrastructure.Commands;
using D.IBlab1.Models;
using D.IBlab1.View.Windows;
using D.IBlab1.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace D.IBlab1.ViewModels.UserControlsViewModels
{
    internal class UserManagementControlViewModel : ViewModelBase
    {
        private readonly MemoryUserStorage _userStorage;

        private List<User> _users;
        /// <summary>Список пользователей </summary>
        public List<User> Users
        {
            get => _users;
            set => Set(ref _users, value);
        }

        private User? _selectedUser;

        /// <summary>DataGrid запись в фокусе </summary>
        public User? SelectedUser
        {
            get => _selectedUser;
            set => Set(ref _selectedUser, value);
        }


        /// <summary>Комманда, вызывающая диалог для добавления пользователя </summary>
        public LambdaCommand AddUserCommand { get; }
        private void OnAddUserCommandExecuted(object p)
        {
            var userEditWindow = new UserEditWindow();

            if (userEditWindow.ShowDialog().Value == true)
                AddUser(userEditWindow.UserName);
        }

        /// <summary>Комманда, меняющая значение IsBlocked выбранного пользователя на противоположженое </summary>
        public LambdaCommand EditUserIsBlockCommand { get; }

        /// <summary>Комманда, меняющая значение PasswordRestriction выбранного пользователя на противоположженое </summary>
        public LambdaCommand EditUserPasswordRestrictionCommand { get; }

        /// <summary>Комманда, удаляющая выбранного пользователя </summary>
        public LambdaCommand DeleteUserCommand { get; }

        public UserManagementControlViewModel(MemoryUserStorage userStorage)
        {
            _userStorage = userStorage;
            _users = _userStorage.GetAll().ToList();

            AddUserCommand = new LambdaCommand(OnAddUserCommandExecuted);
            DeleteUserCommand = new LambdaCommand(p => RemoveUser(), p => SelectedUser != null);

            EditUserIsBlockCommand = new LambdaCommand(p =>
            {
                SelectedUser.IsBlocked = !SelectedUser.IsBlocked;
                EditUser(SelectedUser);
            }, p => SelectedUser != null);

            EditUserPasswordRestrictionCommand = new LambdaCommand(p =>
            {
                SelectedUser.PasswordRestriction = !SelectedUser.PasswordRestriction;
                EditUser(SelectedUser);
            }, p => SelectedUser != null);
        }

        public UserManagementControlViewModel() : this(new MemoryUserStorage()) { }

        private void AddUser(string userName)
        {
            var user = new User { Login = userName };

            if (_userStorage.Add(user) == false)
            {
                ShowWarning("Пользователь с таким логином уже существует");
                return;
            }
            // Чтобы не доставать список из хранилища повторно
            Users.Add(user);
            UpdateDataGrid();
            ShowInfo($"Успешо добавлен логин {userName}");
        }

        private void EditUser(User edited)
        {
            // Это условие никогда не должно срабатывать,
            // либо имеем рассинхрон локального списка пользователей с данными хранилища
            if (_userStorage.Edit(edited, edited.Login) == false)
            {
                ShowWarning("Не найден пользователь с таким логином", "Быть такого не может");
                Users = _userStorage.GetAll().ToList();
                return;
            }
            UpdateDataGrid();
        }

        private void RemoveUser()
        {
            if (_userStorage.Remove(SelectedUser) == false)
            {
                ShowWarning("Не найден пользователь с таким логином", "Быть такого не может");
                Users = _userStorage.GetAll().ToList();
                return;
            }
            ShowInfo($"Успешно удален логин {SelectedUser.Login}");
            Users.Remove(SelectedUser);
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            // Мегакастыль
            var temp = Users;
            Users = null;
            Users = temp;
        }
        private void ShowWarning(string message, string caption = "Предупреждение")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ShowInfo(string message, string caption = "Успех")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
