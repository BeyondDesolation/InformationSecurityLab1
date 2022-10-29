namespace D.IBlab1.Models
{
    internal class User
    {
        /// <summary> Логин пользователя, уникальный </summary>
        public string Login { get; set; } = string.Empty;

        /// <summary> Пароль </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary> Роль. 0 - админ, 1 - обычный пользователь </summary>
        public int Role { get; set; } = 1;

        /// <summary> true - пользователь заблокирован и не может войти в систему, иначе false </summary>
        public bool IsBlocked { get; set; } = false;

        /// <summary> true - для пользователя включено ограничение на структуру пароля, иначе false </summary>
        public bool PasswordRestriction { get; set; } = false;
    }
}
