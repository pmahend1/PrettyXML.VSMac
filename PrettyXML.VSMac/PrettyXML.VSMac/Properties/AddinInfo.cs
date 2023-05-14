using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin(
    "PrettyXML.VSMac",
    Namespace = "PrettyXML.VSMac",
    Version = "2.0.0",
    Category = "Code formatters",
    Flags = AddinFlags.None,
    EnabledByDefault = true,
    Url = "https://github.com/pmahend1/PrettyXML.VSMac"
)]

[assembly: AddinName("PrettyXML VS Mac")]
[assembly: AddinCategory("Code formatters")]
[assembly: AddinDescription("PrettyXML for Visual Studio for Mac.\nFormats XML just like Windows counterpart.\nShortcut CMD+K J or CMD+K CMD+J")]
[assembly: AddinAuthor("Prateek Mahendrakar")]
[assembly: AddinProperty("Icon32", "logo.png")]
[assembly: AddinDependency("::MonoDevelop.Core", "17.5")]
[assembly: AddinDependency("::MonoDevelop.Ide", "17.5")]