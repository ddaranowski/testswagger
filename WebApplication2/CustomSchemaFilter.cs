using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApplication2;

public class CustomSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {

        if (schema.Properties == null || !schema.Properties.Any())
            return;

        // Example of applying the "x-hide-if-falsy" property to a specific field
        if (schema.Properties.ContainsKey("enableRejects"))
        {
            schema.Properties["enableRejects"].Extensions.Add("x-hide-if-false",
                new Microsoft.OpenApi.Any.OpenApiArray
                {
                    new Microsoft.OpenApi.Any.OpenApiString(nameof(RejectTime)) // Specify the name of the property to hide
                });
        }


        if (schema.Properties.ContainsKey("duplicateToSAM"))
        {
            schema.Properties["duplicateToSAM"].Extensions.Add("x-hide-if-false",
                new Microsoft.OpenApi.Any.OpenApiArray
                {
                    new Microsoft.OpenApi.Any.OpenApiString(nameof(SAMSettings)) // Specify the name of the property to hide
                });
        }
    }
}