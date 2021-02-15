namespace DesignPatterns.ChainOfResponsibility
{
    public class ToUpperMutator : StringMutator
    {
        protected override string Operation(string str)
        {
            return str.ToUpper();
        }
    }
}