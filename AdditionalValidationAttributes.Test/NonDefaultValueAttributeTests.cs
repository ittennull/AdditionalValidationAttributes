using System.ComponentModel.DataAnnotations;
using Xunit;

namespace AdditionalValidationAttributes.Test
{
    public class NonDefaultValueAttributeTests
    {
        class Class1
        {
            [NonDefaultValue]
            public int ValueType { get; set; }
        }

        class Class2
        {
            [NonDefaultValue]
            public string ReferenceType { get; set; }
        }

        [Fact]
        public void DefaultValueThrowsForValueType()
        {
            var obj = new Class1();

            Assert.Throws<ValidationException>(() => Validator.ValidateObject(obj, new ValidationContext(obj), validateAllProperties: true));
        }

        [Fact]
        public void NonDefaultValueSucceedsForValueType()
        {
            var obj = new Class1 { ValueType = 1 };

            Validator.ValidateObject(obj, new ValidationContext(obj), validateAllProperties: true);
        }

        [Fact]
        public void DefaultValueThrowsForReferenceType()
        {
            var obj = new Class2();

            Assert.Throws<ValidationException>(() => Validator.ValidateObject(obj, new ValidationContext(obj), validateAllProperties: true));
        }

        [Fact]
        public void NonDefaultValueSucceedsForReferenceType()
        {
            var obj = new Class2 { ReferenceType = "string" };

            Validator.ValidateObject(obj, new ValidationContext(obj), validateAllProperties: true);
        }
    }
}
