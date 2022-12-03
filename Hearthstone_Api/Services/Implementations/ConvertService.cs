using Mapster;

namespace Hearthstone_Api.Services;

public class ConvertService<TM, TD> : IConvertService<TM, TD>
    where TM : class, new()
    where TD : class, new()
{
    public TM ToModel(TD input)
    {
        TM output = new TM();
        input.Adapt(output);
        return output;
    }

    public TD toDTO(TM input)
    {
        TD output = new TD();
        input.Adapt(output);
        return output;
    }
}