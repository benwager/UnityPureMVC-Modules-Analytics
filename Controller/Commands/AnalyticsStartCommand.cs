using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityPureMVC.Modules.Analytics.Controller.Notes;
using UnityPureMVC.Modules.Analytics.Model.Proxies;

namespace UnityPureMVC.Modules.Analytics.Controller.Commands
{
    class AnalyticsStartCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            // Proxies
            Facade.RegisterProxy(new AnalyticsProxy());

            // Register commands
            Facade.RegisterCommand(AnalyticsNote.REQUEST_LOG_ANALYTICS_EVENT, typeof(RequestLogAnalyticsEventCommand));
            Facade.RegisterCommand(AnalyticsNote.REQUEST_SET_ANALYTICS_BASE_URL, typeof(RequestSetAnalyticsBaseURLCommand));

            Facade.RemoveCommand(AnalyticsNote.START);
        }
    }
}