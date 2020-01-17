using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
using UnityPureMVC.Modules.Analytics.Model.Proxies;

namespace UnityPureMVC.Modules.Analytics.Controller.Commands
{
    internal class RequestSetAnalyticsBaseURLCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            DebugLogger.Log("RequestSetAnalyticsBaseURLCommand::Execute");

            // Get AnalyticsProxy
            AnalyticsProxy analyticsProxy = Facade.RetrieveProxy(AnalyticsProxy.NAME) as AnalyticsProxy;
            analyticsProxy.APIBaseURL = notification.Body.ToString();
        }
    }
}
