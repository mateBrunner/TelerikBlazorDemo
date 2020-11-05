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
        //public Continent Continent { get; set; }
        public int ContinentId { get; set; }
        public string ContinentName { get { return m_continentStrings[ ContinentId - 1 ]; } }
        public int Population { get; set; }
        public int Size { get; set; }

        private List<string> m_continentStrings = new List<string>( ) { "AMERIKA", "EURÓPA", "ÁZSIA" };

    }
}
