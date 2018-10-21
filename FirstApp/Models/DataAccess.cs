using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FirstApp.Models
{
    public class DataAccess : DbContext
    {
        public DataAccess():base("name=MyDbConnStr")
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}