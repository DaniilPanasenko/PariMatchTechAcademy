namespace DesignPatterns.ChainOfResponsibility
{
    public class TrimMutator : StringMutator
    {
        protected override string Operation(string str)
        {
            return str.Trim();
        }
    }
}