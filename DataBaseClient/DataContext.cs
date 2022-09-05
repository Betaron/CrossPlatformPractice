using DataBaseClient.Models;
using System.Collections;

namespace DataBaseClient
{
    internal class DataContext
    {
        public Dictionary<Guid, User> Users { get; private set; }

        DataContext()
        {
            Users = new Dictionary<Guid, User>();
        }
    }
}
