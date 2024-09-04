using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using WebApplication2;
using System.Collections.Generic;

public class CircleTypeJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(CircleType).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var jo = JObject.Load(reader);
        string type = jo["type"].ToString();

        CircleType circleType;
        if (jo.ContainsKey("AStringProperty"))
        {
            circleType = new BigCircle();
        }
        else if (jo.ContainsKey("outline"))
        {
            circleType = new SmallCircle();
        }
        else
        {
            throw new ArgumentException("Unknown CircleType type");
        }

        circleType.Type = type;
        serializer.Populate(jo.CreateReader(), circleType);
        return circleType;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var circleType = value as CircleType;
        var jo = JObject.FromObject(value, serializer);
        jo.WriteTo(writer);
    }
}

public class ShapeJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(Shape).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var jo = JObject.Load(reader);
        string shapeType = jo["shapeType"].Value<string>();

        Shape shape;
        switch (shapeType)
        {
            case "rectangle":
                shape = new Rectangle();
                break;
            case "circle":
                shape = new Circle();
                break;
            default:
                throw new ArgumentException("Unknown shape type");
        }

        serializer.Populate(jo.CreateReader(), shape);
        return shape;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var shape = value as Shape;
        var jo = JObject.FromObject(value, serializer);
        jo.AddFirst(new JProperty("shapeType", shape.ShapeType));
        jo.WriteTo(writer);
    }
}

