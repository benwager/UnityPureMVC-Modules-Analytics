using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityPureMVC.Core.Controller.Notes;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
using UnityPureMVC.Core.Model.VO;
using UnityPureMVC.Modules.Analytics.Model.Proxies;
using UnityPureMVC.Modules.Analytics.Model.VO;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityPureMVC.Modules.Analytics.Controller.Commands
{
    internal class RequestLogAnalyticsEventCommand : SimpleCommand
    {
        AnalyticsProxy analyticsProxy;

        public override void Execute(INotification notification)
        {
            // Get AnalyticsProxy
            analyticsProxy = Facade.RetrieveProxy(AnalyticsProxy.NAME) as AnalyticsProxy;

            // Check incoming eventData

            if (!(notification.Body is AnalyticsEvent analyticsEvent))
            {
                Debug.LogWarning("Could not process analytics Event");
                return;
            }

            // Store event in pending list
            analyticsProxy.AddEvent(analyticsEvent);

            // Check if it is appropriate to push events to server
            if (DateTime.Now >= analyticsProxy.GetNextServerPushTime())
            {
                // Start coroutine to push to server via UnityWebRequest
                Facade.SendNotification(CoreNote.REQUEST_START_COROUTINE, new RequestStartCoroutineVO
                {
                    coroutine = Upload(analyticsProxy.GetPendingEventsAsEncodedString())
                });
            }
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
