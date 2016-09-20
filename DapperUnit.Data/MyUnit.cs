using DapperUnit.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnit.Data
{
    public class MyUnit : Core.DapperUnit
    {
        private PirateRepository pirateRepository;
        public PirateRepository PirateRepository
        {
            get { return pirateRepository ?? (pirateRepository = new PirateRepository(Transaction)); }
        }
        
        public MyUnit(IDbConnection connection)
            :base(connection)
        {

        }

        public MyUnit(string connectionString)
            : base(new SqlConnection(connectionString))
        {

        }
    }
}
