namespace Triarch.Repositories.Exceptions;

public class CoreRulesetConflictException : Exception
{
    public int UpdateId { get; private set; }
    public string OriginalName { get; private set; }
    public string ProposedName { get; private set; }
    public int ConflictId { get; private set; }

    public CoreRulesetConflictException(string message, int updateId, string originalName, string proposedName, int conflictId) : base(message)
    {
        UpdateId = updateId;
        OriginalName = originalName;
        ProposedName = proposedName;
        ConflictId = conflictId;
    }
}
