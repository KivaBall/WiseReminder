namespace WiseReminder.Domain.Reactions;

public static class ReactionErrors
{
    public static readonly Result ReactionNotFound =
        new Error("The reaction with the specified IDs was not found");

    public static readonly Result TheSameReaction =
        new Error("The reaction is the same like before");
}