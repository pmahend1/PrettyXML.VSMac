using System;
using System.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using XmlFormatter;

namespace PrettyXML.VSMac2022;

public class XMLFormatter : CommandHandler
{
    #region [ Fields - Text and Document ]
    private ITextBuffer? _buffer;
    private ITextDocument? _document;
    #endregion

    #region [ Override Methods - CommandHandler ]
    protected override void Run(object dataItem)
    {
        base.Run(dataItem);

        var valid = EnsureValidDocument();
        if (valid)
        {
            FormatDocument();
        }
    }

    protected override void Update(CommandInfo info)
    {
        info.Enabled = EnsureValidDocument();
    }
    #endregion

    #region [ Private Methods - Text and Document ]
    private void FormatDocument()
    {
        if (_buffer == null)
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
        _buffer.Replace(new Span(0, _buffer.CurrentSnapshot.Length), formattedText);
    }

    private string? GetCurrentDocumentText()
    {
        return _buffer?.CurrentSnapshot?.AsText()?.ToString();
    }

    private bool EnsureValidDocument()
    {
        var buffer = _buffer = GetBuffer();
        if (buffer == null)
        {
            EditorIsNull();
            return false;
        }

        var document = _document = GetDocument();
        if (document == null)
        {
            EditorIsNull();
            return false;
        }

        var isValidContentType = GetIsValidContentType();
        var isValidFileExtension = GetIsValidFileExtension();

        if (!isValidContentType && !isValidFileExtension)
        {
            FileIsInvalid();
            return false;
        }

        return true;
    }

    private bool GetIsValidFileExtension()
    {
        var fileExtension = GetFileExtension();

        if (fileExtension == null)
        {
            return false;
        }

        if (fileExtension.EndsWith("csproj") ||
            fileExtension.EndsWith("config") ||
            fileExtension.EndsWith("mobileconfig") ||
            fileExtension.EndsWith("xsd") ||
            fileExtension.EndsWith("xml") ||
            fileExtension.EndsWith("xsl") ||
            fileExtension.EndsWith("xaml") ||
            fileExtension.EndsWith("axml") ||
            fileExtension.EndsWith("resx") ||
            fileExtension.EndsWith("plist"))
        {
            return true;
        }

        return false;
    }

    private bool GetIsValidContentType()
    {
        var buffer = _buffer;

        var contentTypeDisplayName = buffer?.ContentType?.DisplayName?.ToLower() ?? string.Empty;

        if (contentTypeDisplayName.EndsWith("xml") ||
            contentTypeDisplayName.EndsWith("resx") ||
            contentTypeDisplayName.EndsWith("xsd") ||
            contentTypeDisplayName.EndsWith("xaml"))
        {
            return true;
        }

        return false;
    }

    private string? GetFileExtension()
    {
        return _document?.FilePath?.Substring(_document?.FilePath?.LastIndexOf(".") ?? 0);
    }

    private ITextDocument? GetDocument()
    {
        return _buffer?.Properties?.PropertyList?.FirstOrDefault(x => x.Key is ITextDocument || x.Value is ITextDocument).Value as ITextDocument;
    }

    private static ITextBuffer? GetBuffer()
    {
        return IdeApp.Workbench?.ActiveDocument?.GetContent<ITextBuffer>();
    }
    #endregion

    #region [ Private Methods - Failed message ]
    private void EditorIsNull()
    {
        Debug.WriteLine("Editor text is null");
    }

    private void FileIsInvalid()
    {
        Debug.WriteLine("File is invialid");
    }
    #endregion
}
