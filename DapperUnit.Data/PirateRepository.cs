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
        public PirateRepository(IDapperUnit dapperUnit)
            : base(dapperUnit)
        {
        }

        public override void Add(Pirate entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            entity.Id = _dapperUnit.Connection.ExecuteScalar<int>(
                "insert into Pirate(Name) values(@Name); select SCOPE_IDENTITY()",
                param: new { Name = entity.Name },
                transaction: _dapperUnit.Transaction);
        }
        
        public override void Update(Pirate entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _dapperUnit.Connection.Execute(
                "update Pirate set Name = @Name where Id = @Id",
                param: new { Id = entity.Id, Name = entity.Name },
                transaction: _dapperUnit.Transaction);
        }
    }
}
