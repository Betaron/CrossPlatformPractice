using System.Collections;

namespace DataBaseClient
{
    internal class DataContext
    {
        public Hashtable Users { get; private set; }

        DataContext()
        {
            Users = new Hashtable();
        }
    }
}
