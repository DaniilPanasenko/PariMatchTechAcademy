using System.Collections.Generic;

namespace DesignPatterns.Builder
{
    public class CustomStringBuilder : ICustomStringBuilder
    { 
        public List<string> _values;

        public CustomStringBuilder()
        {
            _values = new List<string>();
        }

        public CustomStringBuilder(string text)
        {
            _values = new List<string>() { text };
        }

        public ICustomStringBuilder Append(string str)
        {
            _values.Add(str);
            return this;
        }

        public ICustomStringBuilder Append(char ch)
        {
            _values.Add(ch.ToString());
            return this;
        }

        public ICustomStringBuilder AppendLine()
        {
            _values.Add("\n");
            return this;
        }

        public ICustomStringBuilder AppendLine(string str)
        {
            _values.Add("\n");
            _values.Add(str);
            return this;
        }

        public ICustomStringBuilder AppendLine(char ch)
        {
            _values.Add("\n");
            _values.Add(ch.ToString());
            return this;
        }

        public string Build()
        {
            string result = "";
            foreach (var value in _values)
            {
                result += value;
            }
            return result;
        }
    }
}