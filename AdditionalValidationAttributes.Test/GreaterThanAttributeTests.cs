using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace AdditionalValidationAttributes.Test
{
    public class GreaterThanAttributeTests
    {
        class Class1
        {
            [GreaterThan(nameof(Second))]
            public int First { get; set; }
            public int Second { get; set; }
        }

        class Class2
        {
            [GreaterThan(nameof(Second))]
            public int First { get; set; }
            public double Second { get; set; }
        }

        class Class3
        {
            [GreaterThan(nameof(Second), GreaterOrEqual = true)]
            public int First { get; set; }
            public int Second { get; set; }
        }

        class Class4
        {
            [GreaterThan(nameof(Second))]
            public int First { get; set; }
            public int? Second { get; set; }
        }

        [Fact]
        public void SucceedsWhenFirstIsGreaterThanSecond()
        {
            var obj = new Class1
            {
                First = 10,
                Second = 5
            };
            Validator.ValidateObject(obj, new ValidationContext(obj), validateAllProperties: true);
        }

        [Fact]
        public void ThrowsWhenFirstIsEqualToSecondAndComparisonIsStrict()
        {
            var obj = new Class1
            {
                First = 10,
                Second = 10
            };
            Assert.Throws<ValidationException>(() => Validator.ValidateObject(obj, new ValidationContext(obj), validateAllProperties: true));
        }

        [Fact]
        public void SucceedsWhenFirstIsEqualToSecondAndComparisonIsNotStrict()
        {
            var obj = new Class3
            {
                First = 10,
                Second = 10
            };
            Validator.ValidateObject(obj, new ValidationContext(obj), validateAllProperties: true);
        }

        [Fact]
        public void ThrowsWhenFirstIsLessThanSecond()
        {
            var obj = new Class1
            {
                First = 10,
                Second = 15
            };
            Assert.Throws<ValidationException>(() => Validator.ValidateObject(obj, new ValidationContext(obj), validateAllProperties: true));
        }

        [Fact]
        public void ThrowsWhenDifferentTypes()
        {
            var obj = new Class2
            {
                First = 10,
                Second = 15.5
            };
            Assert.Throws<ArgumentException>(() => Validator.ValidateObject(obj, new ValidationContext(obj), validateAllProperties: true));
        }

        [Fact]
        public void SucceedsWhenNullableIsUsed()
        {
            var obj = new Class4
            {
                First = 10,
                Second = 3
            };
            Validator.ValidateObject(obj, new ValidationContext(obj), validateAllProperties: true);
        }

        [Fact]
        public void SucceedsWhenSeconfIsNull()
        {
            var obj = new Class4
            {
                First = 10,
                Second = null
            };
            Validator.ValidateObject(obj, new ValidationContext(obj), validateAllProperties: true);
        }
    }
}
