using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// 
// Language Description is at the top (start with #)
// Every string is below a section: ( [key] )
// Every string has name and fieldName separated by dots, fieldName can be Text, Items or Nodes
// Every string has a value after an equation sign ( = value string ) value can be String or List<String> (seperated by '|')
// You can Comment with ';' (full line, not after a field)
// String resources names starts with @ (they are global to the project)
// 
// Usage Example
// 
// #Name			= English
// #Author			= Author's Name
// #Version      	= v1.1.5
// [frmMain]
// this.Text = Form's title
// btnClose.Text = Close
// cmbItems.Items = One | Two | Three 
// ;This is a comment about the menu
// menuStrip.Items=File|Tools|Help
// menuStrip.Items[0].Items=Open...|Save As...|Exit
// menuStrip.Items[1].Items=Tool 1|Tool 2
// menuStrip.Items[1].Items[0].Items=Tool 1.1|Tool 1.2
// menuStrip.Items[2].Items=TOC|About                   //*Nodes structure are the same but with Nodes[?]...
// @MessageDelete=Are you sure you want to delete?
//
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace IntelligentLevelEditor
{
    public class LanguageDescriptor
    {
        public string Culture;
        public string Name;
        public string Author;
        public string Version;

        public LanguageDescriptor(string culture)
        {
            Culture = culture;
        }
    }

    public static class Localization {

        private static LanguageDescriptor _current;

        private static Dictionary<string, Dictionary<string, string>> _contents;
        private static Dictionary<string, string> _strings;

        public static LanguageDescriptor getCurrentDescriptor()
        {
            return _current;
        }

        private static LanguageDescriptor getDescriptorFor(string filePath)
        {
            var langDesc = new LanguageDescriptor(Path.GetFileNameWithoutExtension(filePath));
            var reader = new StreamReader(filePath);
            var line = reader.ReadLine();
            if (line != null)
                line = line.Trim();
            while (line != null && line.StartsWith("#"))
            {
                if (line.StartsWith("#Name") && line.Contains("="))
                    langDesc.Name = line.Substring(line.IndexOf('=')+1).Trim();
                if (line.StartsWith("#Author") && line.Contains("="))
                    langDesc.Author = line.Substring(line.IndexOf('=') + 1).Trim();
                if (line.StartsWith("#Version") && line.Contains("="))
                    langDesc.Version = line.Substring(line.IndexOf('=') + 1).Trim();
                line = reader.ReadLine();
                if (line != null)
                    line = line.Trim();
            }
            reader.Close();
            return langDesc;
        }

        public static List<LanguageDescriptor> GetLanguages(string path)
        {
            var files = Directory.GetFiles(path, "*.ini", SearchOption.TopDirectoryOnly);
            return files.Select(filePath => getDescriptorFor(filePath)).ToList();
        }

        public static void Load(string culture)
        {
            var filePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty, culture + ".ini");
            if (!File.Exists(filePath)) return;
            var reader = new StreamReader(filePath);
            
            _current = getDescriptorFor(filePath);
            if (_contents == null)
                _contents = new Dictionary<string, Dictionary<string, string>>();
            else
                _contents.Clear();
            if (_strings == null)
                _strings = new Dictionary<string, string>();
            else
                _strings.Clear();

            Dictionary<string, string> currentSection = null;

            string line; //= reader.ReadLine();
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                if (line == "") continue;
                if (line[0] == ';') continue;
                if (line[0] == '@' && line.Contains("="))
                {
                    var eqPos = line.IndexOf('=');
                    _strings[line.Substring(1, eqPos - 1).Trim()] = line.Substring(eqPos + 1).Trim().Replace(@"\n", "\n");
                }
                else if (line.StartsWith("[") && line.EndsWith("]") && line.Length > 2)
                {
                    var currentSectionName = line.Substring(1, line.Length - 2);
                    if (_contents.ContainsKey(currentSectionName))
                    {
                        currentSection = _contents[currentSectionName];
                    }
                    else
                    {
                        currentSection = new Dictionary<string, string>();
                        _contents[currentSectionName] = currentSection;
                    }
                }
                else if (currentSection != null && line.Contains("=") && line.Contains("."))
                {
                    var dotPos = line.IndexOf('.');
                    var eqPos = line.IndexOf('=');
                    if (dotPos < eqPos) //must have a structure of {A.B=C}
                        currentSection[line.Substring(0, eqPos).Trim()] = line.Substring(eqPos + 1).Trim().Replace(@"\n", "\n");
                }
                //line = reader.ReadLine();
            }
            reader.Close();
        }

        public static bool Contains(string stringKey)
        {
            return _strings != null && _strings.ContainsKey(stringKey);
        }

        public static bool Contains(string section, string key)
        {
            return _contents != null && _contents.ContainsKey(section) && _contents[section].ContainsKey(key);
        }

        public static string Get(string section, string key) {
            //caller should check for availability before!
            if (_contents == null || !_contents.ContainsKey(key) || !_contents[section].ContainsKey(key)) 
                return string.Format("<Err {0}:{1}>", section, key);
            return _contents[section][key];
        }

        public static string GetString(string key)
        {
            //caller should check for availability before!
            if (_strings == null || !_strings.ContainsKey(key))
                return string.Format("<Err {0}>", key);
            return _strings[key];
        }

        public static void ApplyToContainer(Control container, string section) {
            if (_contents == null || !_contents.ContainsKey(section)) return;
            var referred = _contents[section];
            if (referred.ContainsKey("this.Text"))
                container.Text = referred["this.Text"];
            ApplyToContainer(container, referred);
        }

        private static void ApplyToContainer(Control container, IDictionary<string, string> referred) 
        {
            foreach (Control control in container.Controls) 
            {
                if (referred.ContainsKey(control.Name + ".Text")) {
                    control.Text = referred[control.Name + ".Text"];
                }
                if (control is ToolStrip || control is ComboBox)
                if (referred.ContainsKey(control.Name + ".Items")) //ComboBox and ToolStrip
                {
                    var stringValues = referred[control.Name + ".Items"].Split('|');
                    if (control is ToolStrip) //includes MenuStrip
                    {
                        var ts = control as ToolStrip;
                        for (var i = 0; i < ts.Items.Count && i < stringValues.Length; i++)
                        {
                            ts.Items[i].Text = stringValues[i];
                            if (ts.Items[i] is ToolStripDropDownItem)
                                ApplyToToolStripItem(control.Name + ".Items[" + i + "]", ts.Items[i] as ToolStripDropDownItem, referred);
                        }
                    }
                    else
                    {
                        var cb = control as ComboBox;
                        for (var i = 0; i < cb.Items.Count && i < stringValues.Length; i++)
                            cb.Items[i] = stringValues[i];
                    }
                }
                if (control is ListView)
                if (referred.ContainsKey(control.Name + ".Columns")) //ListView
                {
                    var stringValues = referred[control.Name + ".Columns"].Split('|');
                    var items = (control as ListView).Columns;
                    for (var i = 0; i < items.Count && i < stringValues.Length; i++)
                        items[i].Text = stringValues[i];
                }
        
                if (control is TreeView)
                if (referred.ContainsKey(control.Name + ".Nodes")) //TreeNode
                {
                    var stringValues = referred[control.Name + ".Nodes"].Split('|');
                        var ts = control as TreeView;
                        if (ts!= null)    
                            for (var i = 0; i < ts.Nodes.Count && i < stringValues.Length; i++)
                            {
                                ts.Nodes[i].Text = stringValues[i];
                                if (ts.Nodes[i].Nodes.Count > 0)
                                    ApplyToTreeNodeCollection(control.Name + ".Nodes[" + i + "]", ts.Nodes[i].Nodes, referred);
                            }
                }
                if (control.ContextMenuStrip != null)
                {
                    if (referred.ContainsKey(control.ContextMenuStrip.Name)) 
                        ApplyToContainer(control.ContextMenuStrip,referred);
                }
                if (control.Controls.Count > 0) {
                    ApplyToContainer(control, referred);
                }
            }
        }

        private static void ApplyToToolStripItem(string containerName, ToolStripDropDownItem dropDown, IDictionary<string, string> referred)
        {
            var searchString = containerName + ".Items";
            if (!referred.ContainsKey(searchString)) return;
            var stringValues = referred[searchString].Split('|');
            for (var i = 0; i < dropDown.DropDownItems.Count && i < stringValues.Length; i++)
            {
                dropDown.DropDownItems[i].Text = stringValues[i];
                if (dropDown.DropDownItems[i] is ToolStripDropDownItem)
                    ApplyToToolStripItem(searchString + "[" + i + "]", dropDown.DropDownItems[i] as ToolStripDropDownItem, referred);
            }
        }

        private static void ApplyToTreeNodeCollection(string containerName, TreeNodeCollection collection, IDictionary<string, string> referred)
        {
            var searchString = containerName + ".Nodes";
            if (!referred.ContainsKey(searchString)) return;
            var stringValues = referred[searchString].Split('|');
            for (var i = 0; i < collection.Count && i < stringValues.Length; i++)
            {
                collection[i].Text = stringValues[i];
                if (collection[i].Nodes.Count > 0)
                    ApplyToTreeNodeCollection(searchString + "[" + i + "]", collection[i].Nodes, referred);
            }
        }
    }
}
