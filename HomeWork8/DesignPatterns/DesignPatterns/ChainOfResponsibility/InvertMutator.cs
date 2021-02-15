using System;

namespace DesignPatterns.ChainOfResponsibility
{
    public class InvertMutator : StringMutator
    {
        protected override string Operation(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            str = new string(charArray);
            return str;
        }
    }
}