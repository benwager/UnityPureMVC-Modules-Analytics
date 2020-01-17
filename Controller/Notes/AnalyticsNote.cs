namespace UnityPureMVC.Modules.Analytics.Controller.Notes
{
    internal class AnalyticsNote
    {
        /// <summary>
        /// Log an analytics event. Pass a new AnalyticsEvent object as notification body
        /// </summary>
        internal const string START = "Analytics/start";
        internal const string REQUEST_LOG_ANALYTICS_EVENT = "Analytics/requestLogAnalyticsEvent";
        internal const string REQUEST_SET_ANALYTICS_BASE_URL = "Analytics/requestSetAnalyticsBaseURL";
    }
}
