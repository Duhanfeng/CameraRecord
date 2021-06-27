using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace vQkmLib.Camera
{
    /// <summary>
    /// 枚举内容入口
    /// </summary>
    public class EnumEntryDescription
    {
        /// <summary>
        /// 节点名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 数值
        /// </summary>
        public long Value { get; set; }

        /// <summary>
        /// 工具提示
        /// </summary>
        public string ToolTip { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 访问权限
        /// </summary>
        public string ImposedAccessMode { get; set; }

        /// <summary>
        /// 创建EnumEntryDescription新实例
        /// </summary>
        public EnumEntryDescription()
        {

        }

        /// <summary>
        /// 创建EnumEntryDescription新实例
        /// </summary>
        /// <param name="name">名称</param>
        public EnumEntryDescription(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 创建EnumEntryDescription新实例
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="displayName">显示名</param>
        /// <param name="toolTip">提示</param>
        /// <param name="description">描述</param>
        /// <param name="imposedAccessMode">访问权限</param>
        public EnumEntryDescription(string name, string displayName, string toolTip, string description, string imposedAccessMode)
        {
            Name = name;
            DisplayName = displayName;
            ToolTip = toolTip;
            Description = description;
            ImposedAccessMode = imposedAccessMode;
        }

        /// <summary>
        /// 转为字符串
        /// </summary>
        /// <returns>字符串结果</returns>
        public override string ToString()
        {
            return $"{Name} ({Value})";
        }
    }

    /// <summary>
    /// 枚举节点内容
    /// </summary>
    public class EnumNodeDescription
    {
        /// <summary>
        /// EnumEntryDescription集合访问器
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>EnumEntryDescription实例</returns>
        public EnumEntryDescription this[string name]
        {
            get
            {
                foreach (var enumEntriy in EnumEntries)
                {
                    if (enumEntriy.Name == name)
                    {
                        return enumEntriy;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// EnumEntryDescription集合访问器
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public EnumEntryDescription this[long value]
        {
            get
            {
                foreach (var enumEntriy in EnumEntries)
                {
                    if (enumEntriy.Value == value)
                    {
                        return enumEntriy;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// 节点名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 枚举列表
        /// </summary>
        public List<EnumEntryDescription> EnumEntries { get; } = new List<EnumEntryDescription>();

        /// <summary>
        /// 创建EnumNodeDescription新实例
        /// </summary>
        public EnumNodeDescription()
        {

        }

        /// <summary>
        /// 创建EnumNodeDescription新实例
        /// </summary>
        /// <param name="name">名称</param>
        public EnumNodeDescription(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 获取枚举参数整型数值
        /// </summary>
        /// <param name="name">枚举名</param>
        /// <returns>枚举参数整型数值</returns>
        public long? GetValue(string name)
        {
            return this[name]?.Value;
        }

        /// <summary>
        /// 获取枚举参数名字
        /// </summary>
        /// <param name="value">枚举参数值</param>
        /// <returns>枚举参数名</returns>
        public string GetName(long value)
        {
            return this[value]?.Name;
        }

        /// <summary>
        /// 转为字符串
        /// </summary>
        /// <returns>字符串结果</returns>
        public override string ToString()
        {
            return $"{Name}, Count = {EnumEntries.Count}";
        }

    }

    /// <summary>
    /// 相机节点
    /// </summary>
    public class NodeDescription
    {
        /// <summary>
        /// 节点名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 工具提示
        /// </summary>
        public string ToolTip { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 可见性
        /// </summary>
        public string Visibility { get; set; }

        /// <summary>
        /// 访问权限
        /// </summary>
        public string ImposedAccessMode { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public string NodeType { get; set; }

        /// <summary>
        /// 数值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 创建NodeDescription新实例
        /// </summary>
        public NodeDescription()
        {

        }

        /// <summary>
        /// 创建NodeDescription新实例
        /// </summary>
        /// <param name="name">名称</param>
        public NodeDescription(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 创建NodeDescription新实例
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="displayName">显示名</param>
        /// <param name="toolTip">提示</param>
        /// <param name="description">描述</param>
        /// <param name="visibility">可见性</param>
        /// <param name="imposedAccessMode">访问权限</param>
        public NodeDescription(string name, string displayName, string toolTip, string description, string visibility, string imposedAccessMode)
        {
            Name = name;
            DisplayName = displayName;
            ToolTip = toolTip;
            Description = description;
            Visibility = visibility;
            ImposedAccessMode = imposedAccessMode;
        }

        /// <summary>
        /// 转为字符串
        /// </summary>
        /// <returns>字符串结果</returns>
        public override string ToString()
        {
            return $"{Name}  [{NodeType}]";
        }
    }

    /// <summary>
    /// XML种类描述
    /// </summary>
    public class CategoryDescription
    {
        /// <summary>
        /// NodeDescription集合访问器
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>NodeDescription实例</returns>
        public NodeDescription this[string name]
        {
            get
            {
                foreach (var node in NodeDescriptions)
                {
                    if (node.Name == name)
                    {
                        return node;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// 种类名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 工具提示
        /// </summary>
        public string ToolTip { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 可见性
        /// </summary>
        public string Visibility { get; set; }

        /// <summary>
        /// 访问权限
        /// </summary>
        public string ImposedAccessMode { get; set; }

        /// <summary>
        /// 节点列表
        /// </summary>
        public List<NodeDescription> NodeDescriptions { get; } = new List<NodeDescription>();

        /// <summary>
        /// 创建CategoryDescription新实例
        /// </summary>
        public CategoryDescription()
        {

        }

        /// <summary>
        /// 创建CategoryDescription新实例
        /// </summary>
        /// <param name="name">名称</param>
        public CategoryDescription(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 创建CategoryDescription新实例
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="displayName">显示名</param>
        /// <param name="toolTip">提示</param>
        /// <param name="description">描述</param>
        /// <param name="visibility">可见性</param>
        /// <param name="imposedAccessMode">访问权限</param>
        public CategoryDescription(string name, string displayName, string toolTip, string description, string visibility, string imposedAccessMode)
        {
            Name = name;
            DisplayName = displayName;
            ToolTip = toolTip;
            Description = description;
            Visibility = visibility;
            ImposedAccessMode = imposedAccessMode;
        }

        /// <summary>
        /// 转为字符串
        /// </summary>
        /// <returns>字符串结果</returns>
        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// 相机XML描述
    /// </summary>
    public class CameraDescription
    {
        /// <summary>
        /// 获取CategoryDescription
        /// </summary>
        /// <param name="xmlNode">XML节点</param>
        /// <param name="categoryDescription">CategoryDescription</param>
        private static void GetCategoryDescription(XmlNode xmlNode, ref CategoryDescription categoryDescription)
        {
            foreach (XmlNode xmlCategoryChild in xmlNode.ChildNodes)
            {
                if (xmlCategoryChild.Name == "Group")
                {
                    GetCategoryDescription(xmlCategoryChild, ref categoryDescription);
                }
                else
                {
                    var nodeDescription = categoryDescription[xmlCategoryChild.Attributes["Name"]?.Value];
                    if (nodeDescription != null)
                    {
                        //获取其节点类型
                        nodeDescription.NodeType = xmlCategoryChild.Name;

                        if (nodeDescription.NodeType == "Enumeration")
                        {
                            nodeDescription.Value = new EnumNodeDescription(xmlCategoryChild.Attributes["Name"]?.Value);
                        }

                        //若为Node,则遍历Node下的所有关键信息
                        foreach (XmlNode xmlNodeChild in xmlCategoryChild.ChildNodes)
                        {

                            switch (xmlNodeChild.Name)
                            {
                                case "ToolTip":
                                    nodeDescription.ToolTip = xmlNodeChild.InnerText;
                                    break;
                                case "Description":
                                    nodeDescription.Description = xmlNodeChild.InnerText;
                                    break;
                                case "DisplayName":
                                    nodeDescription.DisplayName = xmlNodeChild.InnerText;
                                    break;
                                case "Visibility":
                                    nodeDescription.Visibility = xmlNodeChild.InnerText;
                                    break;
                                case "ImposedAccessMode":
                                    nodeDescription.ImposedAccessMode = xmlNodeChild.InnerText;
                                    break;
                                case "EnumEntry":

                                    //若为枚举类型,则此部分为枚举内容
                                    if (nodeDescription.NodeType == "Enumeration")
                                    {
                                        var enumNodeValue = nodeDescription.Value as EnumNodeDescription;

                                        var enumEntry = new EnumEntryDescription(xmlNodeChild.Attributes["Name"]?.Value);

                                        //获取枚举内容中的所有信息
                                        foreach (XmlNode xmlEnumEntryChild in xmlNodeChild.ChildNodes)
                                        {
                                            switch (xmlEnumEntryChild.Name)
                                            {
                                                case "ToolTip":
                                                    enumEntry.ToolTip = xmlEnumEntryChild.InnerText;
                                                    break;
                                                case "Description":
                                                    enumEntry.Description = xmlEnumEntryChild.InnerText;
                                                    break;
                                                case "DisplayName":
                                                    enumEntry.DisplayName = xmlEnumEntryChild.InnerText;
                                                    break;
                                                case "ImposedAccessMode":
                                                    enumEntry.ImposedAccessMode = xmlEnumEntryChild.InnerText;
                                                    break;
                                                case "Value":
                                                    if (xmlEnumEntryChild.InnerText.Contains("0x") || xmlEnumEntryChild.InnerText.Contains("0X"))
                                                    {
                                                        enumEntry.Value = Convert.ToInt64(xmlEnumEntryChild.InnerText, 16);
                                                    }
                                                    else
                                                    {
                                                        enumEntry.Value = long.Parse(xmlEnumEntryChild.InnerText);
                                                    }

                                                    break;
                                                default:
                                                    break;
                                            }

                                        }
                                        enumNodeValue.EnumEntries.Add(enumEntry);
                                    }

                                    break;

                                default:
                                    break;
                            }
                        }

                    }
                }
            }

        }

        /// <summary>
        /// 从XML文件中解析相机信息
        /// </summary>
        /// <param name="doc">xml文档实例</param>
        /// <returns>CameraDescription 实例</returns>
        public static CameraDescription LoadfromFile(XmlDocument doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException($"XmlCameraRoot.LoadfromFile: 入参{nameof(doc)}为null");
            }

            var cameraDescription = new CameraDescription();

            //寄存器描述节点
            XmlNode registerDescriptionNode = null;

            //递归查找根目录(若不过滤注释,则doc.ChildNodes中可能会包含注释信息)
            foreach (XmlNode xmlRoot in doc.ChildNodes)
            {
                if (xmlRoot.Name == "RegisterDescription")
                {
                    registerDescriptionNode = xmlRoot;
                }
            }

            if (registerDescriptionNode == null)
            {
                return null;
            }

            //递归查找所有的Category
            foreach (XmlNode xmlRootChild in registerDescriptionNode.ChildNodes)
            {
                if (xmlRootChild.Name == "Category")
                {
                    //所有的Category信息保存在名为"Root"的Category节点中
                    if (xmlRootChild.Attributes["Name"].Value == "Root")
                    {
                        foreach (XmlNode xmlCategory in xmlRootChild.ChildNodes)
                        {
                            if (xmlCategory.Name == "pFeature")
                            {
                                cameraDescription.CategoryDescriptions.Add(new CategoryDescription(xmlCategory.InnerText));
                            }
                        }
                    }
                }
            }

            //递归添加所有相机Category(包括名字描述及所有的节点信息)
            foreach (XmlNode xmlRootChild in registerDescriptionNode.ChildNodes)
            {
                if (xmlRootChild.Name == "Category")
                {
                    var category = cameraDescription[xmlRootChild.Attributes["Name"]?.Value];

                    if (category == null)
                    {
                        continue;
                    }

                    foreach (XmlNode xmlCategoryChild in xmlRootChild.ChildNodes)
                    {
                        switch (xmlCategoryChild.Name)
                        {
                            case "ToolTip":
                                category.ToolTip = xmlCategoryChild.InnerText;
                                break;
                            case "Description":
                                category.Description = xmlCategoryChild.InnerText;
                                break;
                            case "DisplayName":
                                category.DisplayName = xmlCategoryChild.InnerText;
                                break;
                            case "Visibility":
                                category.Visibility = xmlCategoryChild.InnerText;
                                break;
                            case "ImposedAccessMode":
                                category.ImposedAccessMode = xmlCategoryChild.InnerText;
                                break;
                            case "pFeature":
                                //相机Node保存在pFeature结构中
                                category.NodeDescriptions.Add(new NodeDescription(xmlCategoryChild.InnerText));
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            //递归添加Category中所有Node的信息
            for (int i = 0; i < cameraDescription.CategoryDescriptions.Count; i++)
            {
                var categoryDescription = cameraDescription.CategoryDescriptions[i];

                //递归查找所有的XMLNode
                GetCategoryDescription(registerDescriptionNode, ref categoryDescription);
            }

            return cameraDescription;
        }

        /// <summary>
        /// 从本地加载XML文档
        /// </summary>
        /// <param name="file">本地文档</param>
        /// <returns>CameraDescription 实例</returns>
        public static CameraDescription LoadfromFile(string file)
        {
            //校验文件有效性
            if (!File.Exists(file))
            {
                throw new FileNotFoundException($"CameraDescription.LoadfromFile: 入参{nameof(file)}无效,文件不存在");
            }

            //加载XML文件
            XmlDocument doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings
            {
                IgnoreComments = true
            };
            XmlReader reader = XmlReader.Create(file, settings);
            doc.Load(reader);

            return LoadfromFile(doc);
        }

        /// <summary>
        /// CategoryDescription集合访问器
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>CategoryDescription实例</returns>
        public CategoryDescription this[string name]
        {
            get
            {
                foreach (var category in CategoryDescriptions)
                {
                    if (category.Name == name)
                    {
                        return category;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="nodeName">节点名</param>
        /// <returns>NodeDescription实例</returns>
        public NodeDescription FindNode(string nodeName)
        {
            foreach (var category in CategoryDescriptions)
            {
                var node = category[nodeName];
                if (node != null)
                {
                    return node;
                }
            }

            return null;
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="categoryName">类型实例</param>
        /// <param name="nodeName">节点名</param>
        /// <returns>NodeDescription实例</returns>
        public NodeDescription FindNode(string categoryName, string nodeName)
        {

            return this[categoryName]?[nodeName];
        }

        /// <summary>
        /// CategoryDescription集合
        /// </summary>
        public List<CategoryDescription> CategoryDescriptions { get; } = new List<CategoryDescription>();

        /// <summary>
        /// 转为字符串
        /// </summary>
        /// <returns>字符串结果</returns>
        public override string ToString()
        {
            return "CameraDescription";
        }
    }
}
