using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TranslateJS.Core
{
    public enum JsValueState
    {
        NaN,
        Undefined,
        Null
    };

    public class JsValue : Object, IDictionary<string, JsValue>, IList<JsValue>
    {
        public static readonly JsValue NaN = new JsValue(JsValueState.NaN);
        public static readonly JsValue Null = new JsValue(JsValueState.Null);
        public static readonly JsValue Undefined = new JsValue(JsValueState.Undefined);

        bool? boolValue;
        string strValue;
        double? numberValue;

        Dictionary<string, JsValue> _properties;
        Dictionary<string, JsValue> properties
        {
            get
            {
                if (_properties == null)
                {
                    _properties = new Dictionary<string, JsValue>();
                }

                return _properties;
            }
            set { _properties = value; }
        }

        JsFunc function;

        List<JsValue> _array;
        List<JsValue> array
        {
            get
            {
                if (_array == null)
                {
                    _array = new List<JsValue>();
                }

                return _array;
            }
            set { _array = value; }
        }

        JsValueState valueState;

        public JsValue() { }
        public JsValue(double value)
        {
            this.numberValue = value;
        }
        public JsValue(bool value)
        {
            this.boolValue = value;
        }
        public JsValue(string value)
        {
            this.strValue = value;
        }
        public JsValue(IEnumerable<JsValue> array)
        {
            this.array = new List<JsValue>(array);
        }
        public JsValue(JsFunc function)
        {
            this.function = function;
        }
        public JsValue(JsValueState valueState)
        {
            this.valueState = valueState;
        }
        public JsValue Execute(params JsValue[] arguments)
        {
            return function(arguments);
        }

        public int Length { get { return array.Count; } }

        public override string ToString()
        {
            if (IsBool)
            {
                return boolValue.GetValueOrDefault().ToString();
            }
            else if (IsNumber)
            {
                return numberValue.GetValueOrDefault().ToString();
            }
            else if (IsString)
            {
                return strValue;
            }
            else if (IsObject)
            {
                return _properties.ToString();
            }
            else if (IsArray)
            {
                return String.Join(",", _array);
            }
            else
            {
                return base.ToString();
            }
        }
        public bool IsNaN
        {
            get { return valueState == JsValueState.NaN; }
        }

        public bool IsNull
        {
            get { return valueState == JsValueState.Null; }
        }

        public bool IsUndefined
        {
            get { return valueState == JsValueState.Undefined; }
        }

        public bool IsArray
        {
           get { return _array != null; }
        }

        public bool IsObject
        {
            get { return _properties != null; }
        }

        public bool IsString
        {
            get { return strValue != null; }
        }

        public bool IsNumber
        {
            get { 
                return numberValue.HasValue;
            }
        }

        public bool IsBool
        {
            get
            {
                return boolValue.HasValue;
            }
        }

        public static implicit operator JsValue(int value) { return new JsValue(value); }
        public static implicit operator JsValue(double value) { return new JsValue(value); }
        public static implicit operator JsValue(bool value) { return new JsValue(value); }
        public static implicit operator JsValue(string value) { return new JsValue(value); }
        public static implicit operator JsValue(JsFunc value) { return new JsValue(value); }
        public static implicit operator JsValue(JsValue[] value) { return new JsValue(value); }
        public static bool operator <(JsValue a, JsValue b)
        {
            if (a.IsNumber && b.IsNumber)
            {
                return a.numberValue < b.numberValue;
            }
            else if (a.IsString && a.IsString)
            {
                return a.ToString().Length < a.ToString().Length;
            }
            else if (a.IsArray && a.IsArray)
            {
                return a.array.Count < b.array.Count;
            }
            else if (a.IsObject || b.IsObject)
            {
                throw new Exception("Objects can't be used with < operator");
            }
            else
            {
                return false;
            }
        }

        public static bool operator >(JsValue a, JsValue b)
        {
            if (a.IsNumber && b.IsNumber)
            {
                return a.numberValue < b.numberValue;
            }
            else if (a.IsString && a.IsString)
            {
                return a.ToString().Length > a.ToString().Length;
            }
            else if (a.IsArray && a.IsArray)
            {
                return a.array.Count > b.array.Count;
            }
            else if (a.IsObject || b.IsObject)
            {
                throw new Exception("Objects can't be used with > operator");
            }
            else
            {
                return false;
            }
        }

        public static JsValue operator ++(JsValue a)
        {
            if (a.IsNumber)
            {   
                a.numberValue = a.numberValue.GetValueOrDefault() + 1;
                return a;
            }
            else
            {
                return JsValue.NaN;
            }
        }

        public void Add(string key, JsValue value)
        {
            properties.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return properties.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return properties.Keys; }
        }

        public bool Remove(string key)
        {
            return properties.Remove(key);
        }

        public bool TryGetValue(string key, out JsValue value)
        {
            return properties.TryGetValue(key, out value);
        }

        public ICollection<JsValue> Values
        {
            get { return properties.Values; }
        }

        public JsValue this[string key]
        {
            get
            {
                return properties[key];
            }
            set
            {
                properties[key] = value;
            }
        }

        public void Add(KeyValuePair<string, JsValue> item)
        {
            properties.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            properties.Clear();
        }

        public bool Contains(KeyValuePair<string, JsValue> item)
        {
            return properties.ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<string, JsValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return properties.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, JsValue> item)
        {
            return properties.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<string, JsValue>> GetEnumerator()
        {
            return properties.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return properties.GetEnumerator();
        }

        public int IndexOf(JsValue item)
        {
            return array.IndexOf(item);
        }

        public void Insert(int index, JsValue item)
        {
            array.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            array.RemoveAt(index);
        }

        public JsValue this[int index]
        {
            get
            {
                return array[index];
            }
            set
            {
                array[index] = value;
            }
        }

        public void Add(JsValue item)
        {
            array.Add(item);
        }

        public bool Contains(JsValue item)
        {
            return array.Contains(item);
        }

        public void CopyTo(JsValue[] array, int arrayIndex)
        {
            array.CopyTo(array, arrayIndex);
        }

        public bool Remove(JsValue item)
        {
            return array.Remove(item);
        }

        IEnumerator<JsValue> IEnumerable<JsValue>.GetEnumerator()
        {
            return array.GetEnumerator();
        }


       
    }

}
