using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tmds.DBus;

[assembly: InternalsVisibleTo(Tmds.DBus.Connection.DynamicAssemblyName)]
namespace NexusKrop.FreeDesktop.Native
{
    [DBusInterface("org.freedesktop.Notifications")]
    interface INotifications : IDBusObject
    {
        Task<uint> NotifyAsync(string AppName, uint ReplacesId, string AppIcon, string Summary, string Body, string[] Actions, IDictionary<string, object> Hints, int Timeout);
        Task CloseNotificationAsync(uint Id);
        Task<string[]> GetCapabilitiesAsync();
        Task<(string name, string vendor, string version, string specVersion)> GetServerInformationAsync();
        Task<uint> InhibitAsync(string DesktopEntry, string Reason, IDictionary<string, object> Hints);
        Task UnInhibitAsync(uint arg0);
        Task<IDisposable> WatchNotificationClosedAsync(Action<(uint id, uint reason)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchActionInvokedAsync(Action<(uint id, string actionKey)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchNotificationRepliedAsync(Action<(uint id, string text)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchActivationTokenAsync(Action<(uint id, string activationToken)> handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<NotificationsProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class NotificationsProperties
    {
        private bool _Inhibited = default(bool);
        public bool Inhibited
        {
            get
            {
                return _Inhibited;
            }

            set
            {
                _Inhibited = (value);
            }
        }
    }

    static class NotificationsExtensions
    {
        public static Task<bool> GetInhibitedAsync(this INotifications o) => o.GetAsync<bool>("Inhibited");
    }

    [DBusInterface("org.freedesktop.Application")]
    interface IApplication : IDBusObject
    {
        Task ActivateAsync(IDictionary<string, object> PlatformData);
        Task OpenAsync(string[] Uris, IDictionary<string, object> PlatformData);
        Task ActivateActionAsync(string ActionName, object[] Parameter, IDictionary<string, object> PlatformData);
    }
}