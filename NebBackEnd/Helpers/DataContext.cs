using System.Collections.Generic;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class DataContext
    {

        public DataContext()
        {
            if(Users == null)
            {
                Users = new List<User>();
            }
        }

        public List<User> Users { get; set; }
    }
}