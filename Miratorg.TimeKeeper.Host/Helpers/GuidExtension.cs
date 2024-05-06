namespace Miratorg.TimeKeeper.Host.Helpers;

public static class GuidExtension
{
    public static string ToStringClear(this Guid guid)
    {
        if (guid == Guid.Empty)
        {
            throw new Exception("Guid equal empty");
        }

        var str = guid.ToString().Replace("-", string.Empty);

        return str;
    }
}
