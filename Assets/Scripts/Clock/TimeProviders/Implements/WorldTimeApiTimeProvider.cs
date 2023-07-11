using System;
using UnityEngine.Networking;

namespace DDX.Clock.TimeProviders.Implements
{
    public class WorldTimeApiTimeProvider : ITimeProvider
    {
        //http://worldtimeapi.org/api/ip

        public TimeSpan GetTime()
        {
            return new TimeSpan();
        }

        //private void a()
        //{
        //    using (UnityWebRequest webRequest = UnityWebRequest.Get(""))
        //    {
        //        // Request and wait for the desired page.
        //        yield return webRequest.SendWebRequest();

        //        string[] pages = uri.Split('/');
        //        int page = pages.Length - 1;

        //        switch (webRequest.result)
        //        {
        //            case UnityWebRequest.Result.ConnectionError:
        //            case UnityWebRequest.Result.DataProcessingError:
        //                Debug.LogError(pages[page] + ": Error: " + webRequest.error);
        //                break;
        //            case UnityWebRequest.Result.ProtocolError:
        //                Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
        //                break;
        //            case UnityWebRequest.Result.Success:
        //                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
        //                break;
        //        }
        //    }
        //}
    }

}
