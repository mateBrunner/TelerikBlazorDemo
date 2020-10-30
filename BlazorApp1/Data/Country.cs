using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data
{
    public class Country
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int ContinentId { get; set; }
        public string ContinentName { get { return ( (ContinentEnum)ContinentId ).ToString( ); } }
        public ContinentEnum Continent { get; set; }
        public int Population { get; set; }
        public int Size { get; set; }

    }
}
