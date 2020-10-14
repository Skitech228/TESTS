using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WindowsFormsApp2
{
    public class MyDbContext: DbContext
    { 
        protected MyDbContext():base("DbConnectionString")
        { }
    }
}
