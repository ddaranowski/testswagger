using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new ShapeJsonConverter());
        options.SerializerSettings.Converters.Add(new CircleTypeJsonConverter()); 
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
       
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Shapes API", Version = "v1" });
    c.UseAllOfToExtendReferenceSchemas();
    c.UseOneOfForPolymorphism();
    c.EnableAnnotations();
    c.SchemaFilter<PolymorphismSchemaFilter>();

      
    c.SupportNonNullableReferenceTypes();
    //c.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddSwaggerGenNewtonsoftSupport();
var app = builder.Build();

   // Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shapes API V1");
    });


app.UseAuthorization();

app.MapControllers();

app.Run();