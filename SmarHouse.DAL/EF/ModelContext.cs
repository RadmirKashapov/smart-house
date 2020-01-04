using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace SmarHouse.DAL.EF
{
    public class ModelContext : DbContext
    {

        public ModelContext()
        {

        }

        public class DbInitializer : DropCreateDatabaseIfModelChanges<ModelContext>
        {
            protected override void Seed(ModelContext db)
            {
                
            }
        }
    }
}
