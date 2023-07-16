using FreeDesktop.UI.Notification;

namespace FreeDesktop.Tests;

public class NotificationTest
{
    [Test]
    public void CapabilitiesTest()
    {
        var service = new NotificationService();

        Assert.That(service.Capabilities.Any());

        foreach (var capability in service.Capabilities)
        {
            Console.WriteLine(capability);
        }
    }

    [Test]
    public void SendTest()
    {
        var service = new NotificationService();
        var toast = new ToastNotification("Test")
        {
            AppName = "Tests",
            SoundName = "dialog-error"
        };

        Assert.DoesNotThrowAsync(async () =>
        {
            await service.ShowAsync(toast);
        });

        Assert.That(toast.HadSent());
    }
}