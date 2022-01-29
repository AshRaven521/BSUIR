using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace uwp_test
{
    public class Region
    {
        public string Name { get; set; }
        public List<City> cities { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < cities.Count; i++)
            {
                yield return cities.ElementAt(i);
            }
        }
    }
}
