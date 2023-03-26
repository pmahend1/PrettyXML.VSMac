namespace XmlFormatter;

public class Options
{
    public int IndentLength { get; set; } = 4;


    public bool UseSelfClosingTags { get; set; } = true;


    public bool UseSingleQuotes { get; set; }

    public bool AllowSingleQuoteInAttributeValue { get; set; } = true;


    public bool AddSpaceBeforeSelfClosingTag { get; set; } = true;


    public bool WrapCommentTextWithSpaces { get; set; } = true;


    public bool AllowWhiteSpaceUnicodesInAttributeValues { get; set; } = true;

}
