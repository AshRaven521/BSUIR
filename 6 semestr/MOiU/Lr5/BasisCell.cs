using System;

namespace Lr5
{
    [Serializable]
    public class BasisCell
    {
        public int i { get; set; }
        public int j { get; set; }
        public bool Sign { get; set; }

        public BasisCell(int i, int j, bool sign = false)
        {
            this.i = i;
            this.j = j;
            Sign = sign;
        }
    }
}
