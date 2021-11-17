using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Infrastructure
{
    public class IdGenerator
    {
        private static Func<Guid> _generator;
        public static Func<Guid> GenerateId
        {
            get
            {
                _generator = _generator ?? Guid.NewGuid;
                return _generator; 
            }
            set
            {
                _generator = value;
            }
        }
    }
}
