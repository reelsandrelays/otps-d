using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using UnityEngine;
using UnityEditor;
using System.Threading;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ClientInformation", menuName = "SpreadSheet/ClientInformation")]
public class ClientInformation : ScriptableSingleton<ClientInformation>
{
    #region FIELD
    private SheetsService service;
    private Spreadsheet apiData;
    
    [SerializeField] private string clientID;    
    [SerializeField] private string clientSecret;

    #endregion
    public SheetsService Service
    {
        get
        {
            if (service.Equals(null))
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
            }

            return service;
        }

        set => service = value;
    }

    public string ClientID { get => clientID;}
    public string ClientSecret { get => clientSecret;}

    public Spreadsheet GetSpreadSheetData(string sheetID)
    {
        service = GetAuthentication(clientID, clientSecret);
        apiData = service.Spreadsheets.Get(sheetID).Execute();

        return apiData;
    }
    public SheetsService GetAuthentication(string id, string password)
    {
        var secret = new ClientSecrets
        {
            ClientId = id,
            ClientSecret = password
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

    public string GetSpreadSheetName(string sheetID)
    {
        SetServiceApiData(sheetID);
        return apiData.Properties.Title;
    }
    public int GetSpreadSheetCount(string sheetID)
    {
        SetServiceApiData(sheetID);
        return apiData.Sheets.Count;
    }
    public string GetSpreadSheetURL(string sheetID)
    {
        SetServiceApiData(sheetID);
        return apiData.SpreadsheetUrl;
    }
    public List<string> GetWorksheetName(string sheetID)
    {
        SetServiceApiData(sheetID);

        List<string> worksheetName = new List<string>();

        for (int i = 0; i < apiData.Sheets.Count; ++i)
        {
            worksheetName.Add(apiData.Sheets[i].Properties.Title);
        }

        return worksheetName;
    }
    public SpreadsheetsResource.ValuesResource.GetRequest GetWorkSheetAPIData(string sheetID, string workSheetName)
    {
        var spreadSheetService =
            GetAuthentication(clientID, clientSecret);

        return spreadSheetService.Spreadsheets.Values.Get(sheetID, workSheetName);
    }
    private void SetServiceApiData(string sheetID)
    {
        if (service == null)
        {
            Debug.LogError("Service Data is NULL");
            service = GetAuthentication(clientID, clientSecret);
        }
            
        if (apiData == null)
        {
            Debug.LogError("ApiData is NULL");
            apiData = service.Spreadsheets.Get(sheetID).Execute();
        }   

        return;
    }
}
