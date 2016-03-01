using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSEditor
{
    [Serializable]
    public class Font
    {
        private string v1;
        private int v2;

        public Font(string v1, int v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }
}
