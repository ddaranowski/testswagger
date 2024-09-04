using Microsoft.OpenApi.Any;
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


        if (context.Type == typeof(Circle))
        {
            schema.Properties.Add("discriminatorValue", new OpenApiSchema()
            {
                Enum = new List<IOpenApiAny>
                {
                    new OpenApiString("Circle"),
                }
                ,Default = new OpenApiString("Circle"),
                
            });
        }

        if (context.Type == typeof(Rectangle))
        {
            schema.Properties.Add("discriminatorValue", new OpenApiSchema()
            {
                Enum = new List<IOpenApiAny>
                {
                    new OpenApiString("Rectangle"),
                }
                ,
                Default = new OpenApiString("Rectangle"),

            });
        }


        if (context.Type == typeof(CircleBase))
        {
            schema.Discriminator = new OpenApiDiscriminator
            {
                PropertyName = "circleType",
                Mapping = new Dictionary<string, string>
                {
                    { "BigCircle", "#/components/schemas/BigCircle" },
                    { "SmallCircle", "#/components/schemas/SmallCircle" }
                }
            };

            schema.OneOf = new List<OpenApiSchema>
            {
                new OpenApiSchema { Reference = new OpenApiReference { Id = "BigCircle", Type = ReferenceType.Schema } },
                new OpenApiSchema { Reference = new OpenApiReference { Id = "SmallCircle", Type = ReferenceType.Schema } }
            };
        }
    }
}