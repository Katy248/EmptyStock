namespace EmptyStock.Mvc;

internal static class IdentityHelpers
{
    public static readonly string[] Roles = new[] { "admin", "director", "manager", "stockWorker" };
    public static string GetRole(string unformattedRoleName)
    {
        return 
            Roles
            .FirstOrDefault(role => role.ToUpper().Trim() == unformattedRoleName.ToUpper().Trim().Replace(" ","")) 
            ?? string.Empty;
    }
}