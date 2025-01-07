namespace WiseReminder.Domain.Reactions;

public static class ReactionErrors
{
    public static Result ReactionNotFound =>
        new Error("The reaction with the specified IDs was not found");

    public static Result TheSameReaction =>
        new Error("The reaction is the same like before");
}