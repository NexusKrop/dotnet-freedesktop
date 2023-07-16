using FreeDesktop.UI.Notification;

namespace FreeDesktop.Tests;

public class NotificationTest
{
    [Test]
    public void CapabilitiesTest()
    {
        var service = new NotificationService();

        Assert.That(service.Capabilities.Any());
    }

    [Test]
    public void SendTest()
    {
        var service = new NotificationService();
        var toast = new ToastNotification("Test")
        {
            AppName = "Tests"
        };

        Assert.DoesNotThrowAsync(async () =>
        {
            await service.ShowAsync(toast);
        });

        Assert.That(toast.HadSent());
    }
}