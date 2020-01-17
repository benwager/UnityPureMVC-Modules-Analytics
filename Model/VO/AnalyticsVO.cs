using System;
using System.Collections.Generic;

namespace UnityPureMVC.Modules.Analytics.Model.VO
{
    [System.Serializable]
    internal class AnalyticsEvent
    {
        internal string name;
        internal object data;
    }

    [System.Serializable]
    internal class AnalyticsVO
    {
        internal string api_base_uri;
        internal DateTime lastServerPush;
        internal List<AnalyticsEvent> events;
    }
}