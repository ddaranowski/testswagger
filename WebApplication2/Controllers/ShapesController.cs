using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication2;

[ApiController]
[Route("[controller]")]
public class ShapesController : ControllerBase
{
    private static readonly List<Shape> shapes = new List<Shape>();

    [HttpGet]
    public IEnumerable<Shape> Get()
    {
        return shapes;
    }
}