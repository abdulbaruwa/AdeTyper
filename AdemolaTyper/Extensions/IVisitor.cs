namespace AdemolaTyper.Extensions
{
    public interface IVisitor<ItemToVisit>
    {
        void visit(ItemToVisit item);
    }
}