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

        public override int Add(Pirate entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if(entity.Country == null)
                throw new ArgumentNullException("entity.Country");

            entity.Id = _dapperUnit.Connection.ExecuteScalar<int>(
                "insert into Pirate(Name, CountryId) values(@Name, @CountryId); select SCOPE_IDENTITY()",
                param: new { Name = entity.Name, CountryId = entity.Country.Id },
                transaction: _dapperUnit.Transaction);

            return entity.Id;
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

        public override Pirate Last()
        {
            return _dapperUnit.Connection.Query<Pirate, Country, Pirate>(
                $"select top 1 * from Pirate p join Country c on c.Id = p.CountryId order by p.Id desc",
                (pirate, country) => { pirate.Country = country; return pirate; },
                transaction: _dapperUnit.Transaction).FirstOrDefault();
        }
    }
}
