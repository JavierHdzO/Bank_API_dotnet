using Microsoft.AspNetCore.Mvc;

namespace bank_api.Interfaces;

public interface IContextService<T, R, W>
{

    
    Task<ActionResult<IEnumerable<T>>> GetAll();

    Task<ActionResult<T>?> GetOne(long Id);

    Task<ActionResult<T>> CreateOne( R obj );

    Task<ActionResult> UpdateOne(long Id, W obj);

    Task<bool> DeleteOne(long Id);

}