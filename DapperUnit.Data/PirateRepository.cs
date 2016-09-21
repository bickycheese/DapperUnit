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
    public interface IPirateRepository : IRepository<Pirate>
    {
        
    }

    public class PirateRepository : Repository<Pirate>, IPirateRepository
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

            try
            {
                entity.CountryId = entity.Country.Id;
                entity.Id = _dapperUnit.Connection.ExecuteScalar<int>(
                    "insert into Pirate(Name, CountryId) values(@Name, @CountryId); select SCOPE_IDENTITY()",
                    param: new { Name = entity.Name, CountryId = entity.Country.Id },
                    transaction: _dapperUnit.Transaction);

                return entity.Id;
            }
            catch (Exception ex)
            {
                _dapperUnit.Log(ex.ToString());
                return -1;
                throw;
            }
        }
        
        public override void Update(Pirate entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            try
            {
                _dapperUnit.Connection.Execute(
                    "update Pirate set Name = @Name where Id = @Id",
                    param: new { Id = entity.Id, Name = entity.Name },
                    transaction: _dapperUnit.Transaction);
            }
            catch (Exception ex)
            {
                _dapperUnit.Log(ex.ToString());
                throw;
            }
        }

        public override Pirate Last()
        {
            try
            {
                return _dapperUnit.Connection.Query<Pirate, Country, Pirate>(
                    $"select top 1 * from Pirate p join Country c on c.Id = p.CountryId order by p.Id desc",
                    (pirate, country) => { pirate.Country = country; return pirate; },
                    transaction: _dapperUnit.Transaction).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _dapperUnit.Log(ex.ToString());
                return null;
                throw;
            }
        }

        public override Pirate Find(int id)
        {
            return _dapperUnit.Connection.Query<Pirate, Country, Pirate>(
                    $"select * from Pirate p join Country c on c.Id = p.CountryId where p.Id = @Id",
                    (pirate, country) => { pirate.Country = country; return pirate; },
                    param: new { Id = id },
                    transaction: _dapperUnit.Transaction).FirstOrDefault();
        }
    }
}
