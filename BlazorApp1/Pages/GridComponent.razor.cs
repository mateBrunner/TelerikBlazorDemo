﻿using BlazorApp1.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Blazor.Components;

namespace BlazorApp1.Pages
{
    //ahányszor megnyitjuk a komponenst, új példány készül a classból
    public partial class GridComponent
    {

        // így tudunk DI-t csinálni, de ezt a hivatkozást akár a html-ben is hagyhatnánk
        // service-ből csak egy példány készül összesen
        [Inject]
        CountryService m_Service { get; set; }

        System.Collections.ObjectModel.ObservableCollection<Country> Countries { get; set; }
        //= new System.Collections.ObjectModel.ObservableCollection<Country>( );
        //LoaderType LoaderType = LoaderType.Pulsing;

        protected override async Task OnInitializedAsync( )
        {

            await base.OnInitializedAsync( );
        }

        protected override async Task OnAfterRenderAsync( bool first )
        {
            // ez kell, különben minden ui módosulás után lefut!! a paramétert a blazor állítja automatikusan
            if ( !first )
                return;

            //Azért érdemes a renderbe tenni, mert ha sok időbe tart a beolvasás, akkor itt 
            //már megtörtént a komponens renderelése, feltehetünk loading indicatort. 
            //Ha OnInitialized-ba tettük volna, akkor várjuk, hogy történjen valami.
            Countries = new System.Collections.ObjectModel.ObservableCollection<Country>( );
            var lista = await m_Service.GetCountries( );
            foreach ( Country country in lista )
                Countries.Add( country );

            StateHasChanged( );

            await base.OnAfterRenderAsync( first );

        }

        protected void AddNewCountry()
        {
            Countries.Add( new Country( )
            {
                Id = -1,
                Name = "Magyarország"
            } );

        }



    }
}
