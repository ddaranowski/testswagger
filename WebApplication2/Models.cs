using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApplication2
{
    using Microsoft.OpenApi.Writers;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class RejectTime
    {
        public int Days { get; set; } // Corresponds to "Reject Time (Days)"
    }

    public class SAMSettings
    {
        public bool DuplicateToSAM { get; set; } // Corresponds to "Duplicate to SAM"
        public string? SAMRoutineId { get; set; } // Nullable property for "SAM Routine Id"
        public string? ArchiveDocType { get; set; } // Nullable property for "Archive DocType"
    }


    public class DocumentSettings
    {
        [SwaggerSchema(Description = "EnableRejectsDescription", Title = "enableRejectsTitle")]
        public bool EnableRejects { get; set; } // Corresponds to "Enable Rejects"
        public RejectTime RejectTime { get; set; } // Nullable model for Reject Time

        public bool DuplicateToSAM { get; set; } // Corresponds to "Duplicate to SAM"
        public SAMSettings SAMSettings { get; set; } // Nullable model for SAM settings
    }


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

        public DocumentSettings DocumentSettings { get; set; }
        public PaymentBase Payment { get; set; }

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

    public enum PaymentTypeEnum
    {
        BG,
        PG,
        SE,
        IBAN,
        BBAN,
    }

    public enum IBANTranferType
    {
        SepaCreditCard,
        SepaInstantCreditCard,
    }


    public enum BBANTranferType
    {
        NorvegianDomestic,
        NorvegianInstantDomestic,
    }

    [JsonConverter(typeof(ShapeJsonConverter))]
    public abstract class PaymentBase
    {
        [JsonProperty("paymentType")]
        public string paymentType { get; set; }
    }

    public class IBAN : PaymentBase
    {
        public IBAN()
        {
            paymentType = "IBAN";
        }

  
        public IBANTranferType IBANTranferType { get; set; }

    }

    public class BBAN : PaymentBase
    {
        public BBAN()
        {
            paymentType = "BBAN";
        }


        public BBANTranferType IBANTranferType { get; set; }

    }
}
