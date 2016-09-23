using DapperUnit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnit.Entities
{
    public class Ship : IEntity, IDeleted
    {
        public Ship()
        {
            Pirates = new List<Pirate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Deleted { get; set; }

        public Country Country { get; set; }
        public List<Pirate> Pirates { get; set; }
    }
}
