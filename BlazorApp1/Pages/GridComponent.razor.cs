using BlazorApp1.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Telerik.Blazor.Components;

namespace BlazorApp1.Pages
{

    
    //ahányszor megnyitjuk a komponenst, új példány készül a classból
    public partial class GridComponent: INotifyPropertyChanged
    {

        // így tudunk DI-t csinálni, de ezt a hivatkozást akár a html-ben is hagyhatnánk
        // service-ből csak egy példány készül összesen
        [Inject]
        CountryService m_Service { get; set; }
        
        List<Country> Countries { get; set; }
        IEnumerable<Country> IE_Countries { get; set; }
        List<Continent> Continents { get; set; } = new List<Continent>( );
        Country CurrentlyEditedCountry { get; set; }
        List<string> ContinentStrings { get; set; } = new List<string>( ) { "AMERIKA", "EURÓPA", "ÁZSIA" };        

        int SelectedContinentId { get; set; } = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName, object before, object after)
        {
            if (propertyName == "SelectedContinentId")
            {
                IE_Countries = Countries.Where(x => x.ContinentId == (int)after);
                StateHasChanged();
            }
        }



        //= new System.Collections.ObjectModel.ObservableCollection<Country>( );
        //LoaderType LoaderType = LoaderType.Pulsing;

        protected override async Task OnInitializedAsync( )
        {
            Continents = await m_Service.GetContinents( );
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
            //Countries = new System.Collections.ObjectModel.ObservableCollection<Country>( );
            Countries = await m_Service.GetCountries( );
            IE_Countries = Countries;
            //foreach ( Country country in lista )
            //    Countries.Add( country );

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

        protected void SaveComponentGUIState(string xml)
        { 
           //userid, xml elmegya db-ben;
        }



        void EditHandler( GridCommandEventArgs args )
        {
            Country country = (Country)args.Item;

            //prevent opening for edit based on condition
            if ( country.Id > 10 )
            {
                args.IsCancelled = true;//the general approach for cancelling an event
            }
        }

        void UpdateHandler( GridCommandEventArgs args )
        { 
            Country item = (Country)args.Item;

            // perform actual data source operations here through your service
            Country updatedItem = new Country( )
            {
                Id = item.Id,
                ContinentId = item.ContinentId,
                Name = item.Name,
                Population = item.Population,
                Size = item.Size
            };

            // update the local view-model data with the service data
            var index = Countries.FindIndex( i => i.Id == updatedItem.Id );
            if ( index != -1 )
            {
                Countries[ index ] = updatedItem;
            }
        }

        void DeleteHandler( GridCommandEventArgs args )
        { 
            Country item = (Country)args.Item;

            Countries.Remove( item );
        }

        void CreateHandler( GridCommandEventArgs args )
        {
            Country item = (Country)args.Item;

            // perform actual data source operation here through your service
            Country insertedItem = new Country( )
            {
                Id = item.Id,
                ContinentId = item.ContinentId,
                Name = item.Name,
                Population = item.Population,
                Size = item.Size
            };

            // update the local view-model data with the service data
            Countries.Insert( 0, insertedItem );
        }

        async Task CancelHandler( GridCommandEventArgs args )
        {
            Country item = (Country)args.Item;

            // if necessary, perform actual data source operation here through your service

            await Task.Delay( 1000 ); //simulate actual long running async operation
        }


    }
}
