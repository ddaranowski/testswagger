using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class FlattenInheritanceSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        // Check if the type has a base class and it's not the base Object type
        if (context.Type.BaseType != null && context.Type.BaseType != typeof(object))
        {
            // Get the base type's properties
            var baseProperties = context.Type.BaseType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in baseProperties)
            {
                var propName = prop.Name;
                var propType = prop.PropertyType;

                // Add base class properties to the schema properties
                if (!schema.Properties.ContainsKey(propName))
                {
                    schema.Properties.Add(propName, context.SchemaGenerator.GenerateSchema(propType, context.SchemaRepository));
                }
            }
            if(schema.AllOf != null)
                schema.AllOf.Clear();
        }
    }
}