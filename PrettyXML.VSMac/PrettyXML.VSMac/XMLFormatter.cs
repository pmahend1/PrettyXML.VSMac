using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using XmlFormatter;

namespace PrettyXML.VSMac
{
    public class XMLFormatter : CommandHandler
    {

        protected override void Run(object dataItem)
        {
            base.Run(dataItem);
            var textBuffer = IdeApp.Workbench.ActiveDocument?.GetContent<ITextBuffer>();
            var textview = IdeApp.Workbench.ActiveDocument.GetContent<ITextView>();


            var textdocument = textBuffer?.Properties?.PropertyList?.FirstOrDefault(x =>x.Key is ITextDocument || x.Value is ITextDocument);
            var documentvalue = textdocument?.Value as ITextDocument;

            var ext = documentvalue?.FilePath?.Substring(documentvalue?.FilePath?.LastIndexOf(".")??0);

            var contentType = textBuffer?.ContentType;
            var currentDocumentText = textBuffer?.CurrentSnapshot?.AsText()?.ToString();
            var formatter = new Formatter();


            var contentTypeDisplayName = contentType?.DisplayName?.ToLower() ?? string.Empty;
            if (contentTypeDisplayName.EndsWith("xml") ||
                contentTypeDisplayName.EndsWith("resx") ||
                contentTypeDisplayName.EndsWith("xsd") ||
                contentTypeDisplayName.EndsWith("xaml"))
            {
                if (currentDocumentText != null)
                {
                    var formattedText = formatter.Format(currentDocumentText);
                    var span = new Span(0, textBuffer.CurrentSnapshot.Length);
                    textBuffer.Replace(span, formattedText);
                }
                else
                {
                    Debug.WriteLine("Editor text is null");
                }

            }
            else
            {
                if(ext.EndsWith("csproj") ||
                    ext.EndsWith("config") ||
                    ext.EndsWith("mobileconfig") ||
                    ext.EndsWith("xsd") ||
                    ext.EndsWith("xml") ||
                    ext.EndsWith("xsl") ||
                    ext.EndsWith("xaml") ||
                    ext.EndsWith("axml")||
                    ext.EndsWith("resx") || 
                    ext.EndsWith("plist") )
                {

                    var formattedText = formatter.Format(currentDocumentText);
                    var span = new Span(0, textBuffer.CurrentSnapshot.Length);
                    textBuffer.Replace(span, formattedText);
                }
            }


        }

        protected override void Update(CommandInfo info)
        {

            var textBuffer = IdeApp.Workbench.ActiveDocument.GetContent<ITextBuffer>();
            if (textBuffer != null && textBuffer.AsTextContainer() is SourceTextContainer container)
            {
                var document = container.GetTextBuffer();
                info.Enabled = document != null;
            }
        }
    }
}
