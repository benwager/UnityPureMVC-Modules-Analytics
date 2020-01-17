using PureMVC.Patterns.Proxy;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
using UnityPureMVC.Modules.Analytics.Model.VO;
using System;
using System.Collections.Generic;

namespace UnityPureMVC.Modules.Analytics.Model.Proxies
{
    internal class AnalyticsProxy : Proxy
    {
        new internal const string NAME = "AnalyticsProxy";

        internal AnalyticsVO AnalyticsVO { get { return Data as AnalyticsVO; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:UnityPureMVC.Modules.AnalyticsSystem.Model.Proxies.AnalyticsProxy"/> class.
        /// </summary>
        internal AnalyticsProxy() : base(NAME)
        {
            DebugLogger.Log(NAME + "::__Contstruct");

            Data = new AnalyticsVO();
            AnalyticsVO.events = new List<AnalyticsEvent>();
            AnalyticsVO.lastServerPush = DateTime.Now;
        }

        internal string APIBaseURL
        {
            get
            {
                return AnalyticsVO.api_base_uri;
            }
            set
            {
                AnalyticsVO.api_base_uri = value;
            }
        }

        internal void AddEvent(AnalyticsEvent analyticsEvent)
        {
            AnalyticsVO.events.Add(analyticsEvent);
        }

        internal List<AnalyticsEvent> GetPendingEvents()
        {
            return AnalyticsVO.events;
        }

        internal string GetPendingEventsAsEncodedString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(AnalyticsVO.events);
        }

        internal void ClearAllEvents()
        {
            AnalyticsVO.events.Clear();
        }

        internal void SetServerPushTime()
        {
            AnalyticsVO.lastServerPush = DateTime.Now;
        }

        internal DateTime GetNextServerPushTime()
        {
            return AnalyticsVO.lastServerPush.AddMinutes(1);
        }
    }
}
