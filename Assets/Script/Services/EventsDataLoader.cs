using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class EventsDataLoader
{
    private readonly string FileName = "Request.json";
    private readonly string EmptyString = "";

    public RequestDTO GetSavedRequest()
    {
        var requestJson = GetRequestJson();
        if(requestJson == EmptyString)
        {
            Debug.LogError("Can't load data from " + FileName);
            return new RequestDTO();
        }
        try
        {
            var requestDTO = JsonUtility.FromJson<RequestDTO>(requestJson.ToString());
            if(requestDTO == null)
            {
                Debug.LogError("Can't load data from " + FileName);
                return new RequestDTO();
            }
            else
            {
                return requestDTO;
            }
        }
        catch(System.Exception ex)
        {
            Debug.LogError("Can't load data from " + FileName + ": " + ex);
            return new RequestDTO();
        }
    }

    public void SaveRequest(RequestDTO requestDTO)
    {
        var requestJson = JsonUtility.ToJson(requestDTO);
        var filePath = Path.Combine(Application.persistentDataPath, FileName);
        try
        {
            using(var writer = new StreamWriter(File.Create(filePath)))
            {
                writer.Write(requestJson);
            }
        }
        catch(System.ArgumentException ex)
        {
            Debug.LogError("Can't open file to save inventory data to " + filePath + ": " + ex);
        }
        catch(System.ObjectDisposedException ex)
        {
            Debug.LogError("Can't save inventory data to " + filePath + ": " + ex);
        }
        catch(System.NotSupportedException ex)
        {
            Debug.LogError("Can't save inventory data to " + filePath + ": " + ex);
        }
        catch(System.IO.IOException ex)
        {
            Debug.LogError("Can't save inventory data to " + filePath + ": " + ex);
        }
    }

    private string GetRequestJson()
    {
        var requestJson = EmptyString;
        var filePath = Path.Combine(Application.persistentDataPath, FileName);
        try
        {
            using(var reader = new StreamReader(File.OpenRead(filePath)))
            {
                requestJson = reader.ReadToEnd();
            }
        }
        catch(System.ArgumentException ex)
        {
            Debug.LogError("Can't open file to load inventory data from " + filePath + ": " + ex);
        }
        catch(System.OutOfMemoryException ex)
        {
            Debug.LogError("Can't load inventory data from " + filePath + ": " + ex);
        }
        catch(System.IO.IOException ex)
        {
            Debug.LogError("Can't load inventory data from " + filePath + ": " + ex);
        }
        return requestJson;
    }
}
