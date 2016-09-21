using DapperUnit.Core;
using DapperUnit.Data;
using DapperUnit.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnit.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DapperUnit"].ConnectionString;

            using (var uow = new MyUnit(connectionString))
            {
                var country = uow.Repository<Country>().Find(1);

                uow.Repositories.Add(typeof(Pirate), new PirateRepository(uow));
                uow.Repository<Pirate>().Add(new Pirate { Name = "Stanny", Country = country });
                uow.Commit();

                var count = uow.Repository<Pirate>().Count();
            }

            using (var uow = new MyUnit(connectionString))
            {
                uow.Repositories.Add(typeof(Pirate), new PirateRepository(uow));
                var last = uow.Repository<Pirate>().Last();
            }

            using (var uow = new MyUnit(connectionString))
            {
                uow.Repositories.Add(typeof(Pirate), new PirateRepository(uow));
                ((IPirateRepository)uow.Repository<Pirate>()).ChangeCountry(null, null);
            }
        }
    }
}
