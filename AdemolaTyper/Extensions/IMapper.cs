namespace AdemolaTyper.Extensions
{
    public interface IMapper<Input, Output>
    {
        Output map_from(Input item);
    }
}