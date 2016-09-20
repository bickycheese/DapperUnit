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

            //using (var uow = new MyUnit(connectionString))
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        uow.PirateRepository.Add(new Pirate { Name = $"Pirate {i}" });
            //    }

            //    uow.Commit();                
            //}

            using (var uow = new MyUnit(connectionString))
            {
                //var pirates = uow.PirateRepository.Page(13, 0, 6);

                //var pirate = uow.PirateRepository.Find(1);
                //pirate.Name = "Stanny";

                //uow.PirateRepository.Update(pirate);
                //uow.Commit();

                var pirate = uow.Repository<Pirate>().Find(1);
            }
        }
    }
}
