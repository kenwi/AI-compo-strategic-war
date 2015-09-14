using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneGhost
{
    public class ApplicationModule
    {
        public Type ClassType { get; set; }
        public string Title { get; set; }
        public override string ToString() => Title;
        
    }
}
