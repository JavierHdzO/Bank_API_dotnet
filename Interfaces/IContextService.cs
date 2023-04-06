using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

namespace bank_api.Interfaces;

public interface IContextService<T, R, W>
{

    
    Task<ActionResult<IEnumerable<T>>> GetAll();

    Task<ActionResult<T>?> GetOne(long Id);

    Task<ActionResult<T>> CreateOne( R obj );

    Task<bool>? UpdateOne(long Id, W obj);

    Task<IActionResult>? PatchUpdateOne(JsonPatchDocument jsonPatchDocument);

    Task<bool> DeleteOne(long Id);

}