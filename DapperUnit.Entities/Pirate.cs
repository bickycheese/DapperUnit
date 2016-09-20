using DapperUnit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnit.Entities
{
    public class Pirate : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
