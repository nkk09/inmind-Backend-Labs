using System;
using System.Collections.Generic;
using System.Linq;
using lab1_nour_kassem.Models;

namespace lab1_nour_kassem.Services
{
    public class UserService
    {
        private readonly List<User> _users;

        public UserService()
        {
            _users = new List<User>
            {
                new User { Id = 1, Name = "Nour", Email = "nour@example.com" },
                new User { Id = 2, Name = "Bob", Email = "bob@example.com" },
                new User { Id = 3, Name = "Jill", Email = "jill@example.com" },
                new User { Id = 4, Name = "Sam", Email = "sam@example.com" }
            };
        }

        //i am checking for errors and sending error messages but I didn't throw exceptions "literally"
        //I assumed it was enough because I'm running out of time, but for later, what exactly does the question mean?

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public User GetUserById(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid Id");
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        public List<User> GetUsersByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.");
            var users = _users.Where(u => u.Name.Contains(name)).ToList();
            //I tried to make it case-insensitive but it didn't work...
            //var users = _users.Where(u => u.Name.toLower().Contains(name.toLower()));
            if (users.Count == 0) throw new KeyNotFoundException("No user matches the query");
            return users;
        }

        public void ChangeUserName(UserUpdateRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id <= 0) throw new ArgumentException("Invalid Id");
            var user = _users.FirstOrDefault(u => u.Id == request.Id);
            if (user == null) throw new KeyNotFoundException("User not found");

            user.Name = request.NewName;
            user.Email = request.Email;
        }

        public void DeleteUser(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid Id");
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) throw new KeyNotFoundException("User not found");

            _users.Remove(user);
        }
    }
}
