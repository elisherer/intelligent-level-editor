using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace IntelligentLevelEditor.Games.Crashmo
{
    public enum Difficulty : byte
    {
        [Description("1 - Easy")]
        Easy = 1,
        [Description("2 - Pretty Simple")]
        PrettySimple = 2,
        [Description("3 - Average")]
        Average = 3,
        [Description("4 - Tricky")]
        Tricky = 4,
        [Description("5 - Hard")]
        Hard = 5
    }

    [TypeConverter(typeof(PropertySorter))]
    [DefaultPropertyAttribute("Name")]
    class CrashmoLevelData
    {
        private string _name = "";

        [CategoryAttribute("Crashmo"), DescriptionAttribute("Name of the level"), PropertyOrder(0)]
        public string Name
        {
            get { return _name;}
            set
            {
                _name = value.Contains('\0') ? value.Substring(0, value.IndexOf('\0')) : value;
                if (_name.Length <= 16) return;
                MessageBox.Show(Localization.GetString("ErrorNameLength"));
                _name = _name.Substring(0,16);
            }
        }
        
        [CategoryAttribute("Crashmo"), DescriptionAttribute("The difficulty level (1-5)"), TypeConverter(typeof(EnumDescriptionConverter)), PropertyOrder(1)]
        public Difficulty Difficulty { set; get; }
        [CategoryAttribute("Crashmo"), DescriptionAttribute("Toggle editing inside the studio."), PropertyOrder(2)]
        public bool Locked { set; get; }
        [CategoryAttribute("Crashmo"), DescriptionAttribute("Toggle if the size is 32x32 or 16x16."), PropertyOrder(3)]
        public bool Large { set; get; }
    }

    public class PropertySorter : ExpandableObjectConverter
    {
        #region Methods
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            //
            // This override returns a list of properties in order
            //
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(value, attributes);
            ArrayList orderedProperties = new ArrayList();
            foreach (PropertyDescriptor pd in pdc)
            {
                Attribute attribute = pd.Attributes[typeof(PropertyOrderAttribute)];
                if (attribute != null)
                {
                    //
                    // If the attribute is found, then create an pair object to hold it
                    //
                    PropertyOrderAttribute poa = (PropertyOrderAttribute)attribute;
                    orderedProperties.Add(new PropertyOrderPair(pd.Name, poa.Order));
                }
                else
                {
                    //
                    // If no order attribute is specifed then given it an order of 0
                    //
                    orderedProperties.Add(new PropertyOrderPair(pd.Name, 0));
                }
            }
            //
            // Perform the actual order using the value PropertyOrderPair classes
            // implementation of IComparable to sort
            //
            orderedProperties.Sort();
            //
            // Build a string list of the ordered names
            //
            ArrayList propertyNames = new ArrayList();
            foreach (PropertyOrderPair pop in orderedProperties)
            {
                propertyNames.Add(pop.Name);
            }
            //
            // Pass in the ordered list for the PropertyDescriptorCollection to sort by
            //
            return pdc.Sort((string[])propertyNames.ToArray(typeof(string)));
        }
        #endregion
    }
    #region Helper Class - PropertyOrderAttribute
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyOrderAttribute : Attribute
    {
        //
        // Simple attribute to allow the order of a property to be specified
        //
        private int _order;
        public PropertyOrderAttribute(int order)
        {
            _order = order;
        }

        public int Order
        {
            get
            {
                return _order;
            }
        }
    }
    #endregion
    #region Helper Class - PropertyOrderPair
    public class PropertyOrderPair : IComparable
    {
        private int _order;
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public PropertyOrderPair(string name, int order)
        {
            _order = order;
            _name = name;
        }

        public int CompareTo(object obj)
        {
            //
            // Sort the pair objects by ordering by order value
            // Equal values get the same rank
            //
            int otherOrder = ((PropertyOrderPair)obj)._order;
            if (otherOrder == _order)
            {
                //
                // If order not specified, sort by name
                //
                string otherName = ((PropertyOrderPair)obj)._name;
                return string.Compare(_name, otherName);
            }
            else if (otherOrder > _order)
            {
                return -1;
            }
            return 1;
        }
    }
    #endregion

    class EnumDescriptionConverter : EnumConverter
    {
        private Type _enumType;
        /// Initializing instance
        /// type Enum
        ///this is only one function, that you must
        ///to change. All another functions for enums
        ///you can use by Ctrl+C/Ctrl+V
        public EnumDescriptionConverter(Type type)
            : base(type)
        {
            _enumType = type;
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
        {
            return destType == typeof(string);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
        {
            FieldInfo fi = _enumType.GetField(Enum.GetName(_enumType, value));
            DescriptionAttribute dna = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
            if (dna != null)
                return dna.Description;
            else
                return value.ToString();
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type srcType)
        {
            return srcType == typeof(string);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            foreach (FieldInfo fi in _enumType.GetFields())
            {
                DescriptionAttribute dna =
                (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
                if ((dna != null) && ((string)value == dna.Description))
                    return Enum.Parse(_enumType, fi.Name);
            }
            return Enum.Parse(_enumType, (string)value);
        }
    }
}
