_AdditionalValidationAttributes_ package adds a few validation attributes to collection of attributes from `System.ComponentModel.DataAnnotations` namespace that are often required when for example validating request objects in asp.net 

#### NonDefaultValueAttribute
```csharp
class Class1
{
    [NonDefaultValue]
    public TimeSpan ValueType { get; set; }

    [NonDefaultValue]
    public string ReferenceType { get; set; }
}
```
For classes `NonDefaultValueAttribute` works same as `RequiredAttribute`. For structs it ensures that the value is not equal to default value for this type

#### GreaterThanAttribute
```csharp
class Class1
{
    [GreaterThan(nameof(FromDate))]
    public DateTime ToDate { get; set; }

    public DateTime FromDate { get; set; }
}
```
The `GreaterThanAttribute` works with types that implement `IComparable`. The attribute ensures that the property marked with it is greater than other property specified in attribute constructor. 

It's possible to enable 'greater or equal' comparison by setting property `GreaterOrEqual` of the attribute: `[GreaterThan(nameof(FromDate), GreaterOrEqual = true)]`