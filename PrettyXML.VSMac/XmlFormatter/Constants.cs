using System;

namespace XmlFormatter;

public static class Constants
{
    public const string StartTagStart = "<";

    public const string StartTagEnd = ">";

    public const string EndTagStart = "</";

    public const string EndTagEnd = ">";

    public const string InlineEndTag = "/>";

    public static readonly char Space = ' ';

    public const string CommentTagStart = "<!--";

    public const string CommentTagEnd = "-->";

    public const string AssignmentStart = "=\"";

    public const string AssignmentEnd = "\"";

    public const string AssignmentStartSingleQuote = "='";

    public const string AssignmentEndSingleQuote = "'";

    public const string CDataStart = "<![CDATA[";

    public const string CDataEnd = "]]>";

    public const string DocTypeStart = "<!DOCTYPE";

    public static readonly string Newline = Environment.NewLine;

    public const string Apos = "&apos;";

    public static string DocTypeEnd(string val)
    {
        return $"[{val}]";
    }
}
