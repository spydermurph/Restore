using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
  [HttpGet("not-found")]
  public IActionResult GetNotFound()
  {
    return NotFound();
  }

  [HttpGet("bad-request")]
  public IActionResult GetBadRequest()
  {
    return BadRequest("This was not a good request");
  }

  [HttpGet("unauthorized")]
  public IActionResult GetUnauthorized()
  {
    return Unauthorized();
  }

  [HttpGet("validation-error")]
  public IActionResult GetValidationError()
  {
    ModelState.AddModelError("username", "This username has already been taken");
    ModelState.AddModelError("email", "This email has already been taken");
    return ValidationProblem();
  }

  [HttpGet("server-error")]
  public IActionResult GetServerError()
  {
    throw new Exception("This is a server error");
  }
}
