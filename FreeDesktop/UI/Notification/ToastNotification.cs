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
    // TODO hints

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