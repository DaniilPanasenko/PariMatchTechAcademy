using System.Linq;

namespace DesignPatterns.ChainOfResponsibility
{
    public class RemoveNumbersMutator : StringMutator
    {
        protected override string Operation(string str)
        {
            char[] charArray = str.ToCharArray().Where(x => !char.IsDigit(x)).ToArray();
            str = new string(charArray);
            return str;
        }
    }
}