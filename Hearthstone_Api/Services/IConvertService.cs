namespace Hearthstone_Api.Services;

public interface IConvertService<TM, TD>
    where TM : class, new()
    where TD : class, new()
{
    TM ToModel(TD input);
    TD toDTO(TM input);
}
