using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApplication2
{
    using Microsoft.OpenApi.Writers;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;


    public enum ShapeTypeEnum
    {
        rectangle,
        circle,
    }


    [JsonConverter(typeof(ShapeJsonConverter))]
    public abstract class Shape
    {
        [JsonProperty("shapeType")]
        public string ShapeType { get; set; }
    }

    //[SwaggerDiscriminator("ShapeType")]
  
    
    public class Rectangle : Shape
    {
        public Rectangle()
        {
            ShapeType = "rectangle";
        }

        [JsonProperty("width")]
        public double Width { get; set; }

        [Required]
        [JsonProperty("height")]
        public double Height { get; set; }


        //[SwaggerSchema("Rectangle", ReadOnly = true, Title = "Title")]
        //[DefaultValue(ShapeTypeEnum.rectangle)]

        //public string DiscriminatorValue { get; set; } = "Rectangle";

    }

    public class Circle : Shape
    {
        public Circle()
        {
            ShapeType = "circle";
        }

        [Required]
        [JsonProperty("radius")]
        public double Radius { get; set; }


        public TypeOfCircle TypeOfCircle { get; set; }

        //[SwaggerSchema("Circle", ReadOnly = true, Title = "Title")]
        //[DefaultValue(ShapeTypeEnum.rectangle)]
        //public string DiscriminatorValue { get; set; }

    }

    public class BigCircle : CircleBase
    {
        public BigCircle()
        {
            CircleType = "BigCircle";
        }

        [JsonProperty("AStringProperty")]
        public string AStringProperty { get; set; }
    }

    public class SmallCircle : CircleBase
    {
        public SmallCircle()
        {
            CircleType = "SmallCircle";
        }
        [JsonProperty("outline")]
        public string Outline { get; set; }

    }

    public abstract class CircleBase
    {
        [JsonProperty("circleType")]
        public string CircleType { get; set; }
    }


    public enum TypeOfCircle
    {
        Big,
        Small,
    }
}
