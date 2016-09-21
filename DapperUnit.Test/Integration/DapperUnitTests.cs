using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DapperUnit.Entities;
using DapperUnit.Data;
using DapperUnit.Core;

namespace DapperUnit.Test.Integration
{
    [TestFixture]
    public class DapperUnitTests
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DapperUnit"].ConnectionString;

        [Test]
        public void add_test()
        {
            var id = 0;

            using (var uow = new Core.DapperUnit(connectionString))
            {
                // specific implementation
                uow.Repositories.Add(typeof(Pirate), new PirateRepository(uow));
                // generic implementation
                uow.Repositories.Add(typeof(Country), new Repository<Country>(uow));

                var country = uow.Repository<Country>().Find(1);
                
                id = uow.Repository<Pirate>().Add(new Pirate
                {
                    Name = "Stanny",
                    CountryId = country.Id,
                    Country = country
                });

                uow.Commit();
            }

            using (var uow = new Core.DapperUnit(connectionString))
            {
                uow.Repositories.Add(typeof(Pirate), new PirateRepository(uow));

                var pirate = uow.Repository<Pirate>().Find(id);

                Assert.NotNull(pirate);
                Assert.AreEqual(id, pirate.Id);
            }
        }
    }
}
