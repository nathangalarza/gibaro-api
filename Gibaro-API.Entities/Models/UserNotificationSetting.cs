namespace Entities.Models;

public class UserNotificationSetting
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    //public bool IsFriendRequestActive { get; set; }
    //public bool IsLikeInLiveChatActive { get; set; }
    //public bool IsMessagesInLiveChatActive { get; set; }
    //public bool IsRepliesInLiveChatActive { get; set; }
    //public bool IsMentionsInLiveChatActive { get; set; }
    //public bool IsNearOffersActive { get; set; }
    //public bool IsPeopleYouMayKnowActive { get; set; }
    //public bool IsEstablishmentsNearActive { get; set; }
}