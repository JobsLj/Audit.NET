using System;

namespace Audit.Core.ConfigurationApi
{
    public class ActionEventSelector : IActionEventSelector
    {
        public void OnEventSaved(Action<AuditScope> action)
        {
            Configuration.AddCustomAction(ActionType.OnEventSaved, action);
        }

        public void OnEventSaving(Action<AuditScope> action)
        {
            Configuration.AddCustomAction(ActionType.OnEventSaving, action);
        }

        public void OnScopeCreated(Action<AuditScope> action)
        {
            Configuration.AddCustomAction(ActionType.OnScopeCreated, action);
        }
    }
}