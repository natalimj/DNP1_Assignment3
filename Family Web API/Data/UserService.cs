using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Family_Web_API.Models;


namespace Family_Web_API.Data
{
    public class UserService: IUserService

    {
        private string usersFile = "users.json";
        public IList<User> Users { get; private set; }
        public IList<User> BasicUsers { get; set; }
        
        public UserService()
        {
         
            if (!File.Exists(usersFile))
            {
                Seed();
                WriteUsersToFile();
            }
            else
            {
            
                string content = File.ReadAllText(usersFile);
                Users = JsonSerializer.Deserialize<List<User>>(content);
            }
        }

        
        private void Seed()
        {
            Users = new[] {
                new User {
                   
                    Password = "535353",
                    UserType="admin",
                    UserName = "admin"
                },
                new User {
                   
                    Password = "123456",
                    UserType="admin",
                    UserName = "admin2"
                },
                new User {
                  
                    Password = "123456",
                    UserType="admin",
                    UserName = "admin3"
                }
            }.ToList();
        }

        private void WriteUsersToFile()
        {
            string usersAsJson = JsonSerializer.Serialize(Users,new JsonSerializerOptions{WriteIndented = true});
            File.WriteAllText(usersFile, usersAsJson);
        }


        public User ValidateUser(string userName, string password)
        {
            User first = Users.FirstOrDefault(user => user.UserName.Equals(userName));
            if (first == null) {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(password)) {
                throw new Exception("Incorrect password");
            }

            return first;
        }

        public async Task AddUserAsync(User user)
        {

            if (Users.Where(u => u.UserName.Equals(user.UserName)).ToList().Any())
            {
                throw new Exception("Username is already taken");
            }
            
            user.UserType = "user";
            Users.Add(user);
            WriteUsersToFile();
            
                
        }

        public async Task<IList<User>> GetUsersAsync()
        {
            return Users;
        }

        public async Task<IList<User>>GetBasicUsersAsync()
        { 
            BasicUsers =new List<User>();
            foreach (var user in Users)
            {
                if (user.UserType.Equals("user"))
                {
                    BasicUsers.Add(user);
                }
            }
            return BasicUsers;
        }
        
        
        public async Task<User> RemoveUserAsync(string username)
        {
            User user=Users.Where(a => a.UserName.Equals(username)).ToList()[0];
            Users.Remove(user);
            WriteUsersToFile();
            return user;
        }
    }
}