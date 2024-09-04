namespace WebApplication2
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonConverter(typeof(ShapeJsonConverter))]
    public abstract class Shape
    {
        [JsonProperty("shapeType")]
        public string ShapeType { get; set; }
    }

    public class Rectangle : Shape
    {
        public Rectangle()
        {
            ShapeType = "rectangle";
        }

        [JsonProperty("width")]
        public double Width { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }
    }

    public class Circle : Shape
    {
        public Circle()
        {
            ShapeType = "circle";
        }

        [JsonProperty("radius")]
        public double Radius { get; set; }

        [JsonProperty("typeofcircle")]
        public CircleType CircleType { get; set; }
    }

    public class BigCircle : CircleType
    {
        [JsonProperty("AStringProperty")]
        public string AStringProperty { get; set; }
    }

    public class SmallCircle : CircleType
    {
        [JsonProperty("outline")]
        public string Outline { get; set; }
    }

    public abstract class CircleType
    {
        [JsonProperty("circleType")]
        public string Type { get; set; }
    }
}
