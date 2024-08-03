using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Yomikaze.Application.Helpers.API;
using Notification = Yomikaze.Domain.Entities.Notification;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
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
    public async Task<ActionResult> Test([Required] string title, [Required] string body, [Required] string path, string? fcmToken)
    {
        Message message = new Message
        {
            Webpush = new WebpushConfig
            {
                Data = new Dictionary<string, string>
                {
                    [nameof(title)] = title, [nameof(body)] = body, [nameof(path)] = path
                }
            },
            Android = new AndroidConfig
            {
                Data = new Dictionary<string, string>
                {
                    [nameof(path)] = path, [nameof(title)] = title, [nameof(body)] = body
                }
            }
        };
        if (fcmToken is not null)
        {
            message.Token = fcmToken;
        }
        else
        {
            message.Topic = User.TryGetId(out ulong userId) ? userId.ToString() : "all";
        }

        var result = await FirebaseMessaging.SendAsync(message);
        return Ok(result);
    }

    [HttpPost("[action]")]
    public ActionResult Unsubscribe([FromForm] [Required] string fcmToken)
    {
        ulong userId = User.GetId();
        // remove fcmToken from database
        FirebaseMessaging
            .UnsubscribeFromTopicAsync(new ReadOnlyCollection<string>([fcmToken]), userId.ToString());
        return NoContent();
    }

    [HttpGet]
    public ActionResult<IEnumerable<NotificationModel>> ListNotifications(
        [FromServices] NotificationRepository repository)
    {
        ulong userId = User.GetId();
        List<Notification> query = repository.GetByUserId(userId).ToList();
        List<NotificationModel> result = query.Select(x => new NotificationModel
        {
            Id = x.Id.ToString(),
            Title = x.Title,
            Content = x.Content,
            Read = x.Read,
            CreationTime = x.CreationTime
        }).ToList();
        return Ok(result);
    }

    [HttpPut("{key}/read")]
    public ActionResult MarkAsRead([FromServices] NotificationRepository repository, [FromRoute] ulong key)
    {
        ulong userId = User.GetId();
        Notification? notification = repository.Get(key);
        if (notification == null)
        {
            return NotFound();
        }

        if (notification.UserId != userId)
        {
            return Forbid();
        }

        notification.Read = true;
        repository.Update(notification);
        return NoContent();
    }
}