using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using ShinyPush.Functions.Models;

namespace ShinyPush.Functions.Services
{
    public class DataContext
    {
        private const string DataFile = "data.json";
        private List<User> Users { get; set; }

        public void RegisterUser(string email, string password, string name, string deviceId, string os)
        {
            if(Users.Any(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new Exception("User already registered");
            }

            Users.Add(new User
            {
                Email = email.ToLower(),
                Password = HashText(password),
                Name = name,
                Devices = new List<Device>
                {
                    new Device
                    {
                        DeviceId = deviceId,
                        OS = os.ToLower()
                    }
                },
                Logins = new List<Login>
                {
                    new Login
                    {
                        DeviceId = deviceId,
                        Timestamp = DateTime.Now
                    }
                }
            });

            SaveData();
        }

        public bool Login(string email, string password, string deviceId, string os)
        {
            var user = Users.FirstOrDefault(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));

            if(user != null && user.Password == HashText(password))
            {
                if(!user.Devices.Any(x => x.DeviceId == deviceId))
                {
                    user.Devices.Add(new Device
                    {
                        DeviceId = deviceId,
                        OS = os.ToLower()
                    });
                }

                user.Logins.Add(new Models.Login
                {
                    DeviceId = deviceId,
                    Timestamp = DateTime.Now
                });

                SaveData();
            }

            return false;
        }

        public static string HashText(string text)
        {
            var salt = nameof(DataContext);
            var hasher = new SHA1CryptoServiceProvider();
            byte[] textWithSaltBytes = Encoding.UTF8.GetBytes(string.Concat(text, salt));
            byte[] hashedBytes = hasher.ComputeHash(textWithSaltBytes);
            hasher.Clear();
            return Convert.ToBase64String(hashedBytes);
        }

        private void LoadData()
        {
            if(!File.Exists(DataFile))
            {
                Users = new List<User>();
                SaveData();
                return;
            }

            Users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(DataFile));
        }

        private void SaveData()
        {
            File.WriteAllText(DataFile, JsonConvert.SerializeObject(Users));
        }
    }
}
