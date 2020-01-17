namespace UnityPureMVC.Modules.Analytics
{
    using PureMVC.Interfaces;
    using PureMVC.Patterns.Facade;
    using UnityPureMVC.Modules.Analytics.Controller.Commands;
    using UnityPureMVC.Modules.Analytics.Controller.Notes;
    using System;
    using UnityEngine;

    internal class AnalyticsModule : MonoBehaviour
    {
        /// <summary>
        /// The core facade.
        /// </summary>
        private IFacade facade;

        /// <summary>
        /// Start this instance.
        /// </summary>
        protected virtual void Awake()
        {
            try
            {
                facade = Facade.GetInstance("Core");
                facade.RegisterCommand(AnalyticsNote.START, typeof(AnalyticsStartCommand));
                facade.SendNotification(AnalyticsNote.START, this);
            }
            catch (Exception exception)
            {
                throw new UnityException("Unable to initiate Facade", exception);
            }
        }

        /// <summary>
        /// On destroy.
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (facade != null)
            {
                facade.Dispose();
                facade = null;
            }
        }
    }
}