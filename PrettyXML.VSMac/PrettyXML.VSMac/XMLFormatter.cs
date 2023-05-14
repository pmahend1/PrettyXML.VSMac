using System.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using XmlFormatter;

namespace PrettyXML.VSMac;

public class XMLFormatter : CommandHandler
{
    private ITextBuffer? _textBuffer;
    private ITextDocument? _document;

    protected override void Run(object dataItem)
    {
        base.Run(dataItem);
        if (EnsureValidDocument())
        {
            FormatDocument();
        }
    }

    protected override void Update(CommandInfo info)
    {
        info.Enabled = EnsureValidDocument();
    }

    private void FormatDocument()
    {
        if (_textBuffer == null)
        {
            return;
        }

        var currentDocumentText = GetCurrentDocumentText();

        if (currentDocumentText == null)
        {
            return;
        }

        var formatter = new Formatter();
        var formattedText = formatter.Format(currentDocumentText);

        _textBuffer.Replace(new Span(0, _textBuffer.CurrentSnapshot.Length), formattedText);
    }

    private string? GetCurrentDocumentText()
    {
        return _textBuffer?.CurrentSnapshot.AsText().ToString();
    }

    private bool EnsureValidDocument()
    {
        _textBuffer = GetBuffer();
        if (_textBuffer == null)
        {
            Debug.WriteLine("Editor text is null");
            return false;
        }

        var document = _document = GetDocument();
        if (document == null)
        {
            Debug.WriteLine("Editor text is null");
            return false;
        }

        var isValidContentType = GetIsValidContentType();
        var isValidFileExtension = GetIsValidFileExtension();

        if (!isValidContentType && !isValidFileExtension)
        {
            Debug.WriteLine("File is invialid");
            return false;
        }

        return true;
    }

    private bool GetIsValidFileExtension()
    {
        var fileExtension = GetFileExtension();

        if (fileExtension == null)
        {
            Debug.WriteLine("Invalid file extension.");
            return false;
        }

        return fileExtension.EndsWith("csproj") ||
               fileExtension.EndsWith("config") ||
               fileExtension.EndsWith("mobileconfig") ||
               fileExtension.EndsWith("xsd") ||
               fileExtension.EndsWith("xml") ||
               fileExtension.EndsWith("xsl") ||
               fileExtension.EndsWith("xaml") ||
               fileExtension.EndsWith("axml") ||
               fileExtension.EndsWith("resx") ||
               fileExtension.EndsWith("plist");
    }

    private bool GetIsValidContentType()
    {
        var contentTypeDisplayName = _textBuffer?.ContentType.DisplayName.ToLower() ?? string.Empty;

        return contentTypeDisplayName.EndsWith("xml") ||
               contentTypeDisplayName.EndsWith("resx") ||
               contentTypeDisplayName.EndsWith("xsd") ||
               contentTypeDisplayName.EndsWith("xaml");
    }

    private string? GetFileExtension()
    {
        return _document?.FilePath[(_document.FilePath?.LastIndexOf(".") ?? 0)..];
    }

    private ITextDocument? GetDocument()
    {
        return _textBuffer?.Properties.PropertyList.FirstOrDefault(x => x.Key is ITextDocument || x.Value is ITextDocument).Value as ITextDocument;
    }

    private static ITextBuffer GetBuffer()
    {
        return IdeApp.Workbench.ActiveDocument.GetContent<ITextBuffer>();
    }
}
