using Microsoft.AspNetCore.Authentication;
using NLog;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
namespace BlazorApp1
{
    public class ClaimsTransformationService : IClaimsTransformation
    {
        #region Private fields

        private readonly NLog.ILogger m_Logger;
        private NLog.ILogger m_Logger2 = LogManager.GetCurrentClassLogger();
        private DateTime m_testDateTime;
        //private readonly IToken m_Token;
        //private readonly KOFTablazasAdatbetoltoSettings m_KOFTablazasAdatbetoltoSettings;
        //private readonly ICache m_Cache;
        #endregion

        #region Constructor
        public ClaimsTransformationService( NLog.ILogger logger
                                            //IToken token,
                                            //IOptions<KOFTablazasAdatbetoltoSettings> kofTablazasAdatbetoltoSettingsAccessor,
                                            //ICache cache 
                                           )
        {
            m_Logger = logger;
            m_testDateTime = DateTime.Now;

            //m_Token = token;
            //m_KOFTablazasAdatbetoltoSettings = kofTablazasAdatbetoltoSettingsAccessor.Value;
            //m_Cache = cache;
        }
        #endregion
        #region IClaimsTransformation implementation
        public Task<ClaimsPrincipal> TransformAsync( ClaimsPrincipal principal )
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)principal.Identity;
            Claim claim = new Claim( claimsIdentity.RoleClaimType, "testRole" );

            m_Logger.Info(m_testDateTime);
            m_Logger2.Info(m_testDateTime);

            if ( !claimsIdentity.Claims.Contains( claim ) )
            {
                try
                {
                    // van-e már geometa user a cache-ben?
                    //TokenFelhasznaloValaszDTO geometaUser = m_Cache.GetFromCache<TokenFelhasznaloValaszDTO>( claimsIdentity.Name );
                    //TokenFelhasznaloValaszDTO geometaUser;
                    // ha nincs
                    //if ( geometaUser == null )
                    //{
                        // lekérdezzük
                        //geometaUser = m_Token.DomainFelhasznalo( new TokenFelhasznaloParameterekDTO( ) { LoginNev = claimsIdentity.Name } );
                        // ha a geometa ismeri a usert
                        //if ( geometaUser != null && geometaUser.Sikeres )
                        //{
                        //    m_Logger.LogInformation( $"Felhasználó ({geometaUser.OlvashatoNev}) azonosítása sikeres." );
                        //}
                        // ha a geometa  nem ismeri a usert
                        //else
                        //{
                        //    m_Logger.LogWarning( $"A Felhasználót ({claimsIdentity.Name}) nem sikerült azonosítani." );
                        //}
                        // kérjük el a Windows usert
                        var windowsUser = (WindowsIdentity)principal.Identity;
                        // benne van-e a user a szükséges csoportban?
                        bool isUserInADCsoport = IsInGroup( windowsUser, "GEO\\Domain Users" );
                        // ha nincs benne a user a szükséges csoportban
                        //if ( !isUserInADCsoport )
                        //{
                        //    m_Logger.LogWarning( $"A Felhasználót ({windowsUser.Name}) Windows jogosultság hiányában nem léptetjük be." );
                        //    geometaUser.Sikeres = false;
                        //}
                        //// elmentjük, hogy ne kelljen mindig lekérdezni
                        //if ( geometaUser != null )
                        //    m_Cache.SetInCache( windowsUser.Name, geometaUser );
                    //}

                    // ha a geometa ismeri a usert és benne van a szükséges groupban
                    //if ( geometaUser != null && geometaUser.Sikeres )
                    //if ( isUserInADCsoport )
                    //{
                        // hozzáadjuk a szükséges jogot
                        claimsIdentity.AddClaim( claim );
                    //}
                }
                catch ( Exception ex )
                {
                    m_Logger.Error( ex, $"Ismeretlen hiba történt a felhasználó azonosítása közben." );
                }
            }
            return Task.FromResult( principal );
        }
        #endregion
        #region Private functions
        private bool IsInGroup( WindowsIdentity windowsUser, string groupName )
        {
            if ( windowsUser.Groups != null )
            {
                foreach ( var group in windowsUser.Groups )
                {
                    try
                    {
                        if ( group.Translate( typeof( NTAccount ) ).ToString( ) == groupName )
                            return true;
                    }
                    catch ( Exception )
                    {
                        // ignored
                    }
                }
            }
            return false;
        }
        #endregion
    }
}