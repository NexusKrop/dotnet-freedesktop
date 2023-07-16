using System.Runtime.Versioning;
using NexusKrop.FreeDesktop.Native;
using Tmds.DBus;

namespace FreeDesktop.UI.Notification;

[SupportedOSPlatform("linux")]
public class NotificationService
{
    private static readonly IReadOnlyDictionary<string, NotificationCapability> CapabilityDefinitions = new Dictionary<string, NotificationCapability>()
    {
        { "action-icons", NotificationCapability.ActionIcons },
        { "actions", NotificationCapability.Actions },
        { "body", NotificationCapability.Body },
        { "body-hyperlinks", NotificationCapability.BodyHyperlinks },
        { "body-images", NotificationCapability.BodyImages },
        { "body-markup", NotificationCapability.BodyMarkup },
        { "icon-multi", NotificationCapability.IconMulti },
        { "icon-static", NotificationCapability.IconStatic },
        { "persistence", NotificationCapability.Persistence },
        { "sound", NotificationCapability.Sound }
    };

    private readonly INotifications _service;
    public readonly IReadOnlyList<NotificationCapability> Capabilities;

    public NotificationService()
    {
        _service = Connection.Session.CreateProxy<INotifications>("org.freedesktop.Notifications", "/org/freedesktop/Notifications");

        Capabilities = CalculateCapabilities();
    }

    public async Task ShowAsync(ToastNotification notification)
    {
        var id = await _service.NotifyAsync(AppName: notification.AppName ?? string.Empty,
            ReplacesId: notification._id,
            AppIcon: notification.AppIcon ?? string.Empty,
            Summary: notification.Summary,
            Body: notification.Body ?? string.Empty,
            Array.Empty<string>(),
            new Dictionary<string, object>(),
            notification.Timeout);

        notification._id = id;
    }

    private IReadOnlyList<NotificationCapability> CalculateCapabilities()
    {
        var strCapabilities = _service.GetCapabilitiesAsync().Result;
        var retVal = new List<NotificationCapability>(strCapabilities.Length);

        foreach (var capability in strCapabilities)
        {
            if (!CapabilityDefinitions.TryGetValue(capability, out var enCapability))
            {
                continue;
            }

            retVal.Add(enCapability);
        }

        return retVal.AsReadOnly();
    }
}
