using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
using UnityPureMVC.Modules.Analytics.Model.Proxies;
using System.Collections;
using UnityEngine.Networking;

namespace UnityPureMVC.Modules.Analytics.Controller.Commands
{
    internal class RequestPushAllPendingEvents : SimpleCommand
    {
        AnalyticsProxy analyticsProxy;

        public override void Execute(INotification notification)
        {
            DebugLogger.Log("RequestPushAllPendingEvents::Execute");

            // Get AnalyticsProxy
            analyticsProxy = Facade.RetrieveProxy(AnalyticsProxy.NAME) as AnalyticsProxy;

            Upload(analyticsProxy.GetPendingEventsAsEncodedString());
        }

        IEnumerator Upload(string data)
        {
            byte[] myData = System.Text.Encoding.UTF8.GetBytes(data);
            using (UnityWebRequest www = UnityWebRequest.Put(analyticsProxy.APIBaseURL, myData))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    DebugLogger.Log(www.error);
                }
                else
                {
                    DebugLogger.Log("Successfully sent Analytics data to server!");

                    // Clear data
                    analyticsProxy.ClearAllEvents();

                    // Set new push time
                    analyticsProxy.SetServerPushTime();
                }
            }
        }
    }
}
