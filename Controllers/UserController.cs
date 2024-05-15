using Microsoft.AspNetCore.Mvc;
using lms.Models;
namespace lms.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly testContext dbContext;

    public UserController(testContext ctx)
    {
        this.dbContext = ctx;
    }

    [HttpGet("get-all-user")]
    public IActionResult Get()
    {
        var users = this.dbContext.Users.ToList();
        return Ok(users);
    }

    [HttpGet("get-one-user/{id}")]
    public IActionResult GetOne(int id)
    {
        var user = this.dbContext.Users.FirstOrDefault(i => i.Id == id);
        return Ok(user);
    }

    [HttpDelete("delete-user/{id}")]
    public IActionResult deleteOne(int id)
    {
        var user = this.dbContext.Users.FirstOrDefault(i => i.Id == id);
        if (user != null)
        {
            this.dbContext.Remove(user);
            return Ok(true);
        };

        return Ok(false);
    }

    [HttpDelete("create-one")]
    public IActionResult createOne([FromBody] User payload)
    {
        var user = this.dbContext.Users.FirstOrDefault(i => i.Id == payload.Id);
        if (user != null)
        {
            user.Username = payload.Username;
            user.Password = payload.Password;
            this.dbContext.SaveChanges();
        }
        else
        {
            this.dbContext.Users.Add(payload);
            this.dbContext.SaveChanges();
        };

        return Ok(true);
    }
}
