namespace LowLevelDotNET.Features.Users.Mappers;
public interface IMapper<TSource, TDestination>
{
    TDestination Map(TSource source);
}