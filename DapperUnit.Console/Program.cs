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
                //var pirate = new Pirate { Name = "Arne" };

                //uow.PirateRepository.Add(pirate);
                //uow.Commit();

                var stanny = uow.PirateRepository.Find(1);
            }
        }
    }
}
