using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using Newtonsoft.Json;
using XmlFormatter;

namespace PrettyXML.VSMac
{
    public class XMLFormatter : CommandHandler
    {
        public XMLFormatter()
        {
        }

        protected override void Run(object dataItem)
        {
            base.Run(dataItem);

            var editor = IdeApp.Workbench.ActiveDocument.Editor;
            var xmlText = editor.Text;

            //format

            var formatter = new XmlFormatter.Formatter();

            var dto = new JSInputDTO { IndentLength = 4, UseSelfClosingTags = true, UseSingleQuotes = false, XMLString = xmlText };
            var json = JsonConvert.SerializeObject(dto);
            var formattedText = formatter.Format(json).Result;
            editor.Text = formattedText.ToString();
        }

        protected override void Update(CommandInfo info)
        {
            info.Enabled = IdeApp.Workbench.ActiveDocument?.Editor != null;
        }
    }
}
