using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Yomikaze.Application.Helpers.API;
using Action = FirebaseAdmin.Messaging.Action;
using Notification = FirebaseAdmin.Messaging.Notification;

namespace Yomikaze.API.Main.Controllers;

// TODO)) Add RU for notifications
[ApiController]
[Route("[controller]")]
public class NotificationController(FirebaseApp firebase, ILogger<NotificationController> logger)
    : ControllerBase
{
    private FirebaseMessaging FirebaseMessaging { get; } = FirebaseMessaging.GetMessaging(firebase);

    private ILogger<NotificationController> Logger { get; } = logger;

    [HttpPost("[action]")]
    public ActionResult Subscribe([FromForm] [Required] string fcmToken)
    {
        ulong userId = User.GetId();
        // insert fcmToken into database
        FirebaseMessaging
            .SubscribeToTopicAsync(new ReadOnlyCollection<string>([fcmToken]), userId.ToString());
        return NoContent();
    }
    
    [HttpPost("[action]")]
    public ActionResult Test([Required] string title, [Required] string body, [Required] string path, string? fcmToken)
    {
        var message = new Message
        {
            Webpush = new WebpushConfig
            {
                Data = new Dictionary<string, string>
                {
                    [nameof(title)] = title,
                    [nameof(body)] = body,
                    [nameof(path)] = path,                  
                }
            },
            Android = new AndroidConfig
            {
                Notification = new AndroidNotification()
                {
                    Title = title,
                    Body = body,
                    ClickAction = path
                },
                Priority = Priority.High,
                CollapseKey = "test"
            }
        };
        if (fcmToken is not null)
        {
            message.Token = fcmToken;
        }
        else
        {        
            message.Topic = User.Identity is { IsAuthenticated: true } ? User.GetId().ToString() : "all";
        }
        FirebaseMessaging.SendAsync(message);
        return NoContent();
    }
}