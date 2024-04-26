using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDF2024;

[Authorize]
public class CustomerController : ControllerBase
{
    private readonly DataAccessLayer _dal;

    public CustomerController(DataAccessLayer dal)
    {
        _dal = dal;
    }

    /// <summary>
    /// Gibt alle Namen zurück
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("/api/customer/getnames")]
    public List<string> GetNames()
    {
        var user = User;
        
        return _dal.GetNames();
    }
}