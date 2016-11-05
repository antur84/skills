namespace Cinode.Skills.Api.Mappers
{
    public interface IMapper<T, TY>
    {
        TY Map(T from);

        T MapReverse(TY from);
    }
}