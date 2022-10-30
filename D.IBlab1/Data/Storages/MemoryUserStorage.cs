using D.IBlab1.Models;
using System.Collections.Generic;
using System.Linq;

namespace D.IBlab1.Data.Storages
{
    internal class MemoryUserStorage
    {
        private readonly Dictionary<string, User> _storage;

        public MemoryUserStorage(IEnumerable<User> storage)
        {
            _storage = storage.ToDictionary(u => u.Login, u => u);
        }
        public MemoryUserStorage()
        {
            _storage = new Dictionary<string, User>();
        }

        public IEnumerable<User> GetAll()
        {
            return _storage.Values.AsEnumerable();
        }

        public bool Add(User newUser)
        {
            if(_storage.ContainsKey(newUser.Login))
                return false;
           
             _storage.Add(newUser.Login, newUser);
             return true;
        }

        public User Get(string login)
        {
            if(_storage.TryGetValue(login, out User? user))
                return user;

            return null;
        }
        public bool Edit(User editedUser, string userLastLogin)
        {
            if (_storage.ContainsKey(userLastLogin) == false)
                return false;

            _storage[userLastLogin] = editedUser;
            return true;
        }

        public bool Remove(User deletedUser)
        {
            return _storage.Remove(deletedUser.Login);
        }
    }
}
