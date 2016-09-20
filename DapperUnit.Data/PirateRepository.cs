using DapperUnit.Core;
using DapperUnit.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace DapperUnit.Data
{
    public class PirateRepository : Repository<Pirate>
    {
        public PirateRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public override void Add(Pirate entity)
        {
            entity.Id = Connection.ExecuteScalar<int>(
                "INSERT INTO Pirate(Name) VALUES(@Name); SELECT SCOPE_IDENTITY()",
                param: new { Name = entity.Name },
                transaction: Transaction);
        }

        public override void Delete(Pirate entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(Pirate entity)
        {
            throw new NotImplementedException();
        }
    }
}
