using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data
{
    public class AppState
    {

        public string CurrentState { get; private set; }

        public event Action OnChange;

        public void SetState( string state )
        {
            CurrentState = state;
            NotifyStateChanged( );
        }

        private void NotifyStateChanged( ) => OnChange?.Invoke( );

    }
}
