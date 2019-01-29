using System;
using System.ComponentModel.DataAnnotations;

namespace AdditionalValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NonDefaultValueAttribute : ValidationAttribute
    {
        public NonDefaultValueAttribute()
            :base("The property '{0}' requires a non-default value")
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var type = value.GetType();
            if (type.IsValueType)
            {
                var defaultValue = Activator.CreateInstance(Nullable.GetUnderlyingType(type) ?? type);
                if (value.Equals(defaultValue))
                    return false;
            }

            return true;
        }
    }
}
