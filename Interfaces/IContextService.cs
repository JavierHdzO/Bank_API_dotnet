using Microsoft.AspNetCore.Mvc;

namespace bank_api.Interfaces;

public interface IContextService<T>
{

    
    Task<ActionResult<IEnumerable<T>>> GetAll();

    Task<ActionResult<T>> GetOne();

    Task<ActionResult<T>> CreateOne( T obj );

    Task<IActionResult> UpdateOne(long Id, T obj);

    Task<IActionResult> DeleteOne(long Id);

}