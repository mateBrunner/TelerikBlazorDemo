using BlazorApp1.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApp1.Data
{
    public class CountryService
    {

        private List<Country> m_Countries = new List<Country>( )
        {
            new Country() {Id = 1, ContinentId = 1, Continent = ContinentEnum.AMERIKA, Name = "USA", Population = 1239813, Size = 23423},
            new Country() {Id = 2, ContinentId = 1, Continent = ContinentEnum.AMERIKA, Name = "Mexikó", Population = 234234, Size = 2634},
            new Country() {Id = 3, ContinentId = 1, Continent = ContinentEnum.AMERIKA, Name = "Kanada", Population = 264236, Size = 234623},
            new Country() {Id = 4, ContinentId = 2, Continent = ContinentEnum.EURÓPA, Name = "Németország", Population = 278782, Size = 815},
            new Country() {Id = 5, ContinentId = 2, Continent = ContinentEnum.EURÓPA, Name = "Franciaország", Population = 2342, Size = 14525},
            new Country() {Id = 6, ContinentId = 2, Continent = ContinentEnum.EURÓPA, Name = "Spanyolország", Population = 165987623, Size = 12121},
            new Country() {Id = 7, ContinentId = 2, Continent = ContinentEnum.EURÓPA, Name = "Hollandia", Population = 23432, Size = 17143},
            new Country() {Id = 8, ContinentId = 2, Continent = ContinentEnum.EURÓPA, Name = "Olaszország", Population = 3472362, Size = 12345121},
            new Country() {Id = 9, ContinentId = 2, Continent = ContinentEnum.EURÓPA, Name = "Svájc", Population = 34929, Size = 272345},
            new Country() {Id = 10, ContinentId = 3, Continent = ContinentEnum.ÁZSIA, Name = "Kína", Population = 195347, Size = 35987},
            new Country() {Id = 11, ContinentId = 3, Continent = ContinentEnum.ÁZSIA, Name = "India", Population = 1234134, Size = 3459},
        };

        public Task<List<Continent>> GetContinents()
        {
            //Thread.Sleep( 2000 );
            return Task.FromResult( new List<Continent>( )
            {
                new Continent() {Id = 1, Name = "Amerika"},
                new Continent() {Id = 2, Name = "Európa"},
                new Continent() {Id = 3, Name = "Ázsia"}
            } );
        }


        //Async is lehet, ha pl. DB-hez kell visszamenni
        //public Task<List<Country>> GetCountries( int continentId )
        //{
        //    return Task.FromResult( m_Countries.Where( x => x.ContinentId == continentId ).ToList( ) );
        //}

        public List<Country> GetCountries(int continentId)
        {
            return m_Countries.Where(x => x.ContinentId == continentId).ToList();
        }

        private static int counter = 0;

        public Task<List<Country>> GetCountries()
        {
            Console.WriteLine( counter );
            Thread.Sleep( 3000 );
            return Task.FromResult( m_Countries );
        }

        public Task<List<Country>> TestWaiting()
        {
            Thread.Sleep( 1500 );
            return Task.FromResult( m_Countries );
        }
    }

}
