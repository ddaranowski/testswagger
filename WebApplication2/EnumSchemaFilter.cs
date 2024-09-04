using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApplication2;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            schema.Enum = context.Type
                .GetEnumNames()
                .Select(name => new Microsoft.OpenApi.Any.OpenApiString(name))
                .Cast<Microsoft.OpenApi.Any.IOpenApiAny>()
                .ToList();
            schema.AllOf = null;
        }
    }
}