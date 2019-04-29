using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NIM.Robot
{
    /// <summary>
    /// 机器人模板消息
    /// </summary>
    public class MessageTemplate
    {
        public List<ILayout> Layouts { get; set; }

        public string ID { get; set; }

        public string Version { get; set; }

        public string Params { get; set; }

        /// <summary>
        /// 解析机器人模板消息
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public static MessageTemplate ParseTemplate(string template)
        {
            MessageTemplate msgTemplate = new MessageTemplate();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(template);
            var rootNode = doc.SelectSingleNode("template");
            msgTemplate.ID = GetAttributeValue(rootNode.Attributes["id"]);
            msgTemplate.Version = GetAttributeValue(rootNode.Attributes["version"]);
            msgTemplate.Layouts = new List<ILayout>();
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                if (node.Name == "LinearLayout")
                {
                    LinearLayout layout = new LinearLayout();
                    layout.Orientation = GetAttributeValue(node.Attributes["orientation"]);
                    layout.Weight = GetAttributeValue(node.Attributes["weight"]);
                    layout.Width = GetAttributeValue(node.Attributes["width"]);
                    layout.Height = GetAttributeValue(node.Attributes["heigth"]);
                    layout.Items = new List<IElement>();
                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        var element = ParseElement(subNode);
                        layout.Items.Add(element);
                    }
                    msgTemplate.Layouts.Add(layout);
                }
            }
            return msgTemplate;
        }

        private static string GetAttributeValue(XmlAttribute attribute)
        {
            return attribute != null ? attribute.Value : null;
        }

        private static IElement ParseElement(XmlNode subNode)
        {
            IElement element = null;
            if (subNode.Name == "text")
            {
                Text text = new Text();
                text.Name = GetAttributeValue(subNode.Attributes["name"]);
                text.Width = GetAttributeValue(subNode.Attributes["width"]);
                text.Color = GetAttributeValue(subNode.Attributes["color"]);
                text.Value = subNode.InnerText;
                element = text;
            }
            if (subNode.Name == "image")
            {

                Image image = new Image();
                image.Name = GetAttributeValue(subNode.Attributes["name"]);
                image.Url = GetAttributeValue(subNode.Attributes["url"]);
                image.Width = GetAttributeValue(subNode.Attributes["width"]);
                image.Height = GetAttributeValue(subNode.Attributes["height"]);
                element = image;
            }
            if (subNode.Name == "link")
            {
                Link link = new Link();
                link.Style = GetAttributeValue(subNode.Attributes["style"]);
                link.Type = GetAttributeValue(subNode.Attributes["type"]);
                link.Target = GetAttributeValue(subNode.Attributes["target"]);
                link.Params = GetAttributeValue(subNode.Attributes["params"]);
                link.Url = GetAttributeValue(subNode.Attributes["url"]);
                link.SubItems = new List<IElement>();
                foreach (XmlNode child in subNode.ChildNodes)
                {
                    link.SubItems.Add(ParseElement(child));
                }
                element = link;
            }
            return element;
        }
    }

    public interface ILayout
    {
        List<IElement> Items { get; set; }
    }

    public class LinearLayout : ILayout
    {
        public List<IElement> Items { get; set; }
        public string Orientation { get; set; }

        public string Weight { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }
    }

    public interface IElement
    {

    }

    public class Text : IElement
    {
        public string Name { get; set; }

        public string Width { get; set; }

        public string Color { get; set; }

        public string Value { get; set; }
    }

    public class Image : IElement
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }
    }

    public class Link : IElement
    {
        public List<IElement> SubItems { get; set; }

        public string Type { get; set; }

        public string Style { get; set; }

        public string Target { get; set; }

        public string Params { get; set; }

        public string Url { get; set; }
    }
}
