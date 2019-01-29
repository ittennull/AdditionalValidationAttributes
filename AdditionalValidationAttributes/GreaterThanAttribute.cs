using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AdditionalValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class GreaterThanAttribute : ValidationAttribute
    {
        private readonly string _otherPropertyName;

        public GreaterThanAttribute(string otherPropertyName)
        {
            _otherPropertyName = otherPropertyName;
        }

        public bool GreaterOrEqual { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return null;

            var otherProperty = validationContext.ObjectType.GetProperties()
                .Where(x => x.Name == _otherPropertyName)
                .SingleOrDefault();
            if (otherProperty == null)
                throw new ArgumentException($"Property '{_otherPropertyName}' not found in type {validationContext.ObjectType.Name}");

            var otherValue = otherProperty.GetValue(validationContext.ObjectInstance);
            if (otherValue == null)
                return null;

            var comparable = value as IComparable;
            if (value == null)
                throw new ArgumentException($"Type of property {validationContext.MemberName} doesn't implement interface {nameof(IComparable)}");

            var compareResult = comparable.CompareTo(otherValue);
            var isGreater = GreaterOrEqual
                ? compareResult >= 0
                : compareResult > 0;

            if (isGreater)
                return null;

            return new ValidationResult($"{validationContext.MemberName} must be greater than {_otherPropertyName}");
        }
    }
}
