namespace WiseReminder.Application.Reactions;

public sealed record ReactionStat
{
    public int Likes { get; init; }
    public int Dislikes { get; init; }
}