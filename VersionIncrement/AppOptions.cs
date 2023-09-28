namespace VersionIncrement;

internal sealed class AppOptions
{
    public const string MajorBump = "major";
    public const string MinorBump = "minor";
    public const string PatchBump = "patch";
    public const string PreReleaseBump = "pre";

    public string BumpPosition { get; set; } = "";
};