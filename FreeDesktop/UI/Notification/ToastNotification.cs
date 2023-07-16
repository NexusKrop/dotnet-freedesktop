namespace FreeDesktop.UI.Notification;

public class ToastNotification
{
    internal uint _id = 0;

    public ToastNotification(string summary)
    {
        Summary = summary;
    }

    public string? AppName { get; set; }
    public string? AppIcon { get; set; }
    public string Summary { get; set; }
    public string? Body { get; set; }

    // TODO actions

    /// <summary>
    /// Gets the hints dictionary of this notification.
    /// </summary>
    /// <remarks>
    /// <note type="warning">
    /// While you can pass any object to this dictionary, passing non-primitive types could lead to errors
    /// and we recommend you do not pass reference types for any reason.
    /// </note>
    /// <para>
    /// Hints are key-value objects used to provide additional properties and context associated with the notification.
    /// A notification service ("server") does not have to support any of the hints, nor may define hints required;
    /// instead, if a server fails to understand a hint, the hint is ignored silently, and the server does not
    /// except a hint that always will be sent.
    /// </para>
    /// </remarks>
    public IDictionary<string, object> Hints { get; } = new Dictionary<string, object>();

    public NotificationUrgency Urgency
    {
        get
        {
            if (!Hints.TryGetValue("urgency", out var outVal)
             || outVal is not byte x || x < 0 || x > 2)
            {
                return NotificationUrgency.Normal;
            }

            return (NotificationUrgency)x;
        }
        set => Hints["urgency"] = (byte)value;
    }

    public string? SoundName
    {
        get
        {
            if (!Hints.TryGetValue("sound-name", out var outVal))
            {
                return null;
            }

            return outVal.ToString();
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Hints.Remove("sound-name");
                return;
            }

            Hints["sound-name"] = value;
        }
    }

    /// <summary>
    /// Gets or sets the time in milliseconds that this notification will expire and be hidden.
    /// </summary>
    /// <value>
    /// Time in milliseconds that this notification will expire and be hidden. If <c>0</c>, this notification never expires; if
    /// <c>-1</c>, this notification will have a default expire timeout.
    /// </value>
    public int Timeout { get; set; } = -1;

    public bool HadSent() => _id != 0;
}