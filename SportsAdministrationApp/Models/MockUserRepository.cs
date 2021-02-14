using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Models
{
    public class MockUserRepository : IUserRepository
    {
        private List<User> _UserList;

        public MockUserRepository()
        {
            _UserList = new List<User>()
        {
            new User() { Id = 1, Name = "Mary", Team = "Swim", Email = "mary@gmail.com" },
            new User() { Id = 2, Name = "John", Team = "Swim", Email = "john@gmail.com" },
            new User() { Id = 3, Name = "Sam", Team = "Tennis", Email = "sam@gmail.com" },
        };
        }

        public User Add(User user)
        {
            throw new NotImplementedException();
        }

        public User Delete(int id)
        {
            User user = _UserList.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _UserList.Remove(user);
            }
            return user;
        }

        public IEnumerable<User> GetAllUser()
        {
            return _UserList;
        }

        public User GetUser(int Id)
        {
            return this._UserList.FirstOrDefault(e => e.Id == Id);
        }

        public User Update(User userChanges)
        {
            User user = _UserList.FirstOrDefault(u => u.Id == userChanges.Id);
            if (user != null)
            {
                user.Name = userChanges.Name;
                user.Email = userChanges.Email;
                user.Team = userChanges.Team;
            }
            return user;
        }
    }
}
