namespace KebabFury.Innopolice.Docs;

public sealed class ProviderType
{
    private ProviderType(string value) { Value = value; }

    public string Value { get; private set; }

    public static ProviderType Todoist => new("Todoist");
}
