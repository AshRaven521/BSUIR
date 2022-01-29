using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace uwp_test
{
    public class Country
    {
        public string Name { get; set; }
        public List<Region> regions { get; set; }
        public override string ToString()
        {
            return Name;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < regions.Count; i++)
            {
                yield return regions.ElementAt(i);
            }
        }
    }
}
