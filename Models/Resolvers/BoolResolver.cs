using AutoMapper;
using  bank_api.Models;

public class BoolResolver : IValueResolver<Object, Object, bool>
{
    private readonly bool _value;
    public BoolResolver(bool value){
        _value = value;
    }

    public bool Resolve(object source, object destination, bool destMember, ResolutionContext context)
    {
        throw new NotImplementedException();
    }
}
