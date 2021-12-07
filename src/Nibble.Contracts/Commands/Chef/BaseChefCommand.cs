using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Contracts.Commands.Chef
{
    public class BaseChefCommand
    {
        public Guid ChefId { get; set; }
    }
}
