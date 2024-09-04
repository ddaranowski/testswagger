using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApplication2;

public class PolymorphismSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(Shape))
        {
            schema.Discriminator = new OpenApiDiscriminator
            {
                PropertyName = "shapeType",
                Mapping = new Dictionary<string, string>
                {
                    { "rectangle", "#/components/schemas/Rectangle" },
                    { "circle", "#/components/schemas/Circle" }
                }
            };

            schema.OneOf = new List<OpenApiSchema>
            {
                new OpenApiSchema { Reference = new OpenApiReference { Id = "Rectangle", Type = ReferenceType.Schema } },
                new OpenApiSchema { Reference = new OpenApiReference { Id = "Circle", Type = ReferenceType.Schema } }
            };
        }
    }
}