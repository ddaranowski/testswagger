using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new ShapeJsonConverter());
        options.SerializerSettings.Converters.Add(new CircleTypeJsonConverter());
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shapes API V1");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();