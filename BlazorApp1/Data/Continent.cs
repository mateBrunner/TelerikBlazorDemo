using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data
{
    public class Continent
    {

        public int Id { get; set; }
        public string Name { get; set; }

    }

    public enum ContinentEnum
    {
        AMERIKA = 1,
        EURÓPA = 2,
        ÁZSIA = 3
    }
}
