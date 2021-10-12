/* ClientInformation.cs
 * Wayway Studio
 * Spreadsheet Clientinformation
 * for Validation
 * ScriptableObjectCreator, Odin Inspector Require
 * 2021.09.13 */

using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using UnityEngine;

using System.Threading;

namespace Wayway.Engine.SpreadSheets
{
    public class ClientInformation : ScriptableObject
    {
        [SerializeField] private string clientID;
        [SerializeField] private string clientSecret;

        private SheetsService service;

        public string ClientID => clientID;
        public string ClientSecret => clientSecret;

        public SheetsService Service { get => service ??= GetAuthentication(); set => service = value; }
        public Spreadsheet GetSpreadSheetData(string sheetID) => Service.Spreadsheets.Get(sheetID).Execute();

        public SpreadsheetsResource.ValuesResource.GetRequest GetWorkSheetAPIData(string sheetID, string workSheetName)
        {
            var spreadSheetService = GetAuthentication();

            return spreadSheetService.Spreadsheets.Values.Get(sheetID, workSheetName);
        }

        private SheetsService GetAuthentication()
        {
            var secret = new ClientSecrets
            {
                ClientId = clientID,
                ClientSecret = clientSecret
            };

            var scopes = new string[] { SheetsService.Scope.Spreadsheets };
            var credit = GoogleWebAuthorizationBroker.AuthorizeAsync(secret, scopes, "user", CancellationToken.None).Result;
            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credit,
                ApplicationName = "SpreadSheet Reader"
            });

            return service;
        }
    }
}


