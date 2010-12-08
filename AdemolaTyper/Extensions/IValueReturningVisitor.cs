namespace AdemolaTyper.Extensions
{
    public interface IValueReturningVisitor<ItemToVisit, ValueToReturn> : IVisitor<ItemToVisit>
    {
       ValueToReturn GetResult();    
    }
}