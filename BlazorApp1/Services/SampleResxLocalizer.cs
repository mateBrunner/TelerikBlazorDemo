using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Telerik.Blazor.Services;

public class SampleResxLocalizer : ITelerikStringLocalizer
{
    // this is the indexer you must implement
    public string this[ string name ]
    {
        get
        {
            return GetStringFromResource( name );
        }
    }

    // sample implementation - uses .resx files in the ~/Resources folder named TelerikMessages.<culture-locale>.resx
    public string GetStringFromResource( string key )
    {
        return BlazorApp1.Resources.TelerikMessages.ResourceManager.GetString( key, BlazorApp1.Resources.TelerikMessages.Culture ); ;
    }
}
