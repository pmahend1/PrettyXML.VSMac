using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Xml;
using Microsoft.VisualBasic;

namespace XmlFormatter;
public class Formatter
{
    private int currentAttributeSpace;

    private int currentStartLength;

    private XmlNodeType lastNodeType;

    private Options currentOptions = new Options();

    private XmlDocument ConvertToXMLDocument(string input)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(input);
        return xmlDocument;
    }

    public string Format(string input, Options formattingOptions = null)
    {
        try
        {
            if (formattingOptions != null)
            {
                currentOptions = formattingOptions;
                if (formattingOptions.UseSingleQuotes)
                {
                    currentOptions.AllowSingleQuoteInAttributeValue = false;
                }
            }
            XmlDocument xml = ConvertToXMLDocument(input);
            return FormatXMLDocument(xml);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string FormatXMLDocument(XmlDocument xml)
    {
        StringBuilder sb = new StringBuilder();
        XmlDeclaration xmlDeclaration = xml.ChildNodes.OfType<XmlDeclaration>().FirstOrDefault();
        if (xmlDeclaration != null)
        {
            lastNodeType = XmlNodeType.XmlDeclaration;
            sb.Append(xmlDeclaration.OuterXml + Constants.Newline);
        }
        if (xml.DocumentType != null)
        {
            sb.Append(xml.DocumentType!.OuterXml + Constants.Newline);
        }
        XmlElement documentElement = xml.DocumentElement;
        lastNodeType = XmlNodeType.Document;
        PrintNode(documentElement, ref sb);
        return sb.ToString();
    }

    private void PrintNode(XmlNode node, ref StringBuilder sb)
    {
        XmlNodeType xmlNodeType = lastNodeType;
        lastNodeType = node.NodeType;
        switch (node.NodeType)
        {
            case XmlNodeType.CDATA:
                {
                    string text = ((xmlNodeType == XmlNodeType.Text) ? string.Empty : Environment.NewLine);
                    string text2 = ((xmlNodeType == XmlNodeType.Text) ? string.Empty : new string(Constants.Space, currentStartLength));
                    sb.Append(text + text2 + "<![CDATA[" + node.Value + "]]>");
                    return;
                }
            case XmlNodeType.Comment:
                if (currentOptions.WrapCommentTextWithSpaces)
                {
                    StringBuilder obj2 = sb;
                    string[] obj3 = new string[6] {
                    new string (Constants.Space, currentStartLength),
                    "<!--",
                    null,
                    null,
                    null,
                    null
                };
                    char space = Constants.Space;
                    obj3[2] = space.ToString();
                    obj3[3] = node.Value?.Trim();
                    space = Constants.Space;
                    obj3[4] = space.ToString();
                    obj3[5] = "-->";
                    obj2.Append(string.Concat(obj3));
                }
                else
                {
                    sb.Append(new string(Constants.Space, currentStartLength) + "<!--" + node.Value?.Trim() + "-->");
                }
                return;
            case XmlNodeType.DocumentType:
                {
                    StringBuilder obj = sb;
                    char space = Constants.Space;
                    obj.Append("<!DOCTYPE" + space + Constants.DocTypeEnd(node.Value));
                    return;
                }
            case XmlNodeType.EntityReference:
                sb.Append(node.OuterXml);
                return;
            case XmlNodeType.ProcessingInstruction:
                sb.Append("<?" + node.Name + " " + node.Value + "?>");
                return;
            case XmlNodeType.SignificantWhitespace:
                return;
            case XmlNodeType.Text:
                sb.Append(node.OuterXml);
                return;
            case XmlNodeType.Whitespace:
                return;
        }
        string text3 = ((xmlNodeType != XmlNodeType.Text) ? new string(Constants.Space, currentStartLength) : string.Empty);
        sb.Append(text3 + "<" + node.Name);
        XmlAttributeCollection? attributes = node.Attributes;
        if (attributes != null && attributes!.Count > 0)
        {
            sb.Append(Constants.Space);
            currentAttributeSpace = currentStartLength + node.Name.Length + 2;
            for (int i = 0; i < node.Attributes!.Count; i++)
            {
                XmlAttribute xmlAttribute = node.Attributes![i];
                bool flag = i == node.Attributes!.Count - 1;
                string text4 = (flag ? string.Empty : Environment.NewLine);
                string text5 = SecurityElement.Escape(xmlAttribute.Value);
                if (currentOptions.AllowWhiteSpaceUnicodesInAttributeValues)
                {
                    if (text5.Contains("\n"))
                    {
                        text5 = text5.Replace("\n", "&#xA;");
                    }
                    if (text5.Contains("\t"))
                    {
                        text5 = text5.Replace("\t", "&#x9;");
                    }
                }
                if (currentOptions.AllowSingleQuoteInAttributeValue && text5.Contains("&apos;"))
                {
                    text5 = text5.Replace("&apos;", "'");
                }
                sb.Append(xmlAttribute.Name + (currentOptions.UseSingleQuotes ? "='" : "=\"") + text5 + (currentOptions.UseSingleQuotes ? "'" : "\"") + text4);
                if (!flag)
                {
                    sb.Append(new string(Constants.Space, currentAttributeSpace));
                }
                else if (node.HasChildNodes)
                {
                    sb.Append(">");
                }
            }
        }
        else if (node.HasChildNodes)
        {
            sb.Append(">");
        }
        if (node.HasChildNodes)
        {
            if (!(node.ChildNodes.Cast<XmlNode>().First() is XmlText))
            {
                currentStartLength += currentOptions.IndentLength;
            }
            for (int j = 0; j < node.ChildNodes.Count; j++)
            {
                XmlNode xmlNode = node.ChildNodes[j];
                if (xmlNode.NodeType != XmlNodeType.Text && xmlNode.NodeType != XmlNodeType.CDATA && xmlNode.NodeType != XmlNodeType.EntityReference && lastNodeType != XmlNodeType.Text && xmlNode.NodeType != XmlNodeType.SignificantWhitespace && xmlNode.NodeType != XmlNodeType.Whitespace)
                {
                    sb.Append(Constants.Newline);
                }
                PrintNode(xmlNode, ref sb);
            }
            if (node.NodeType != XmlNodeType.Comment && node.NodeType != XmlNodeType.CDATA && node.NodeType != XmlNodeType.DocumentType && node.NodeType != XmlNodeType.Text)
            {
                if (currentStartLength >= currentOptions.IndentLength && lastNodeType != XmlNodeType.Text && lastNodeType != XmlNodeType.CDATA && lastNodeType != XmlNodeType.DocumentType && lastNodeType != XmlNodeType.EntityReference)
                {
                    currentStartLength -= currentOptions.IndentLength;
                }
                string text6 = ((lastNodeType != XmlNodeType.Text && lastNodeType != XmlNodeType.CDATA && lastNodeType != XmlNodeType.EntityReference) ? Constants.Newline : string.Empty);
                string text7 = ((lastNodeType != XmlNodeType.Text && lastNodeType != XmlNodeType.EntityReference) ? new string(Constants.Space, currentStartLength) : string.Empty);
                sb.Append(text6 + text7 + "</" + node.Name + ">");
                lastNodeType = node.NodeType;
            }
        }
        else if (currentOptions.UseSelfClosingTags)
        {
            if (currentOptions.AddSpaceBeforeSelfClosingTag)
            {
                StringBuilder obj4 = sb;
                char space = Constants.Space;
                obj4.Append(space + "/>");
            }
            else
            {
                sb.Append("/>");
            }
        }
        else
        {
            sb.AppendFormat("></" + node.Name + ">");
        }
    }

    public string Minimize(string xmlString)
    {
        XmlDocument xmlDocument = ConvertToXMLDocument(xmlString);
        XmlDeclaration xmlDeclaration = xmlDocument.ChildNodes.OfType<XmlDeclaration>().FirstOrDefault();
        StringWriter stringWriter = (string.IsNullOrEmpty(xmlDeclaration?.Encoding) ? new StringWriterWithEncoding() : new StringWriterWithEncoding(Encoding.GetEncoding(xmlDeclaration.Encoding)));
        XmlWriterSettings settings = new XmlWriterSettings
        {
            Indent = false,
            IndentChars = string.Empty,
            NewLineChars = string.Empty,
            NewLineHandling = NewLineHandling.Entitize,
            NewLineOnAttributes = false,
            NamespaceHandling = NamespaceHandling.OmitDuplicates
        };
        using (XmlWriter w = XmlWriter.Create(stringWriter, settings))
        {
            xmlDocument.Save(w);
        }
        return stringWriter.ToString();
    }
}
