using System.Collections.Generic;
using System.Text;

namespace ShinyPush.Functions.Models
{
    public class User
    {
        public User()
        {
            Devices = new List<Device>();
            Logins = new List<Login>();
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public List<Device> Devices { get; set; }

        public List<Login> Logins { get; set; }
    }
}
