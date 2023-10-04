using CommandLine;

public class Options
{
    [Option('l', "list", Required = true, HelpText = "Anim List file path")]
    public string ListFilePath { get; set; }

    [Option('o', "output", Required = true, HelpText = "Output folder path")]
    public string OutputFolder{ get; set; }

    [Option('p', "prefix", Required = true, HelpText = "Mod prefix(nemesis mod folder name)")]
    public string ModPrefix { get; set; }

    [Option('n', "name", Required = true, HelpText = "Mod name")]
    public string ModName { get; set; }

    [Option('a', "author", Required = true, HelpText = "Author")]
    public string ModAuthor { get; set; }

    [Option('k', "link", Required = false, HelpText = "Mod link")]
    public string ModLink { get; set; } = "fakeLink";
}