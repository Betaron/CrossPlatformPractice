using DataBaseClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient.Gateways;

public class UserDbModel
{
    public string Login { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public UserDbModel() { }

    public UserDbModel(User InstanceToCopy)
    {
        Login = InstanceToCopy.Login;
        Email = InstanceToCopy.Email;
        PhoneNumber = InstanceToCopy.PhoneNumber;
    }
}
