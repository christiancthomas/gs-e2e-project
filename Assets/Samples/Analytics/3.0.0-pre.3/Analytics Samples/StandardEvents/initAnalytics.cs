using UnityEngine;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class InitWithDefault : MonoBehaviour
{
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            List<string> consentIdentifiers = await Events.CheckForRequiredConsents();
        }
        catch (ConsentCheckException e)
        {
          // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately
        }
    }
}
