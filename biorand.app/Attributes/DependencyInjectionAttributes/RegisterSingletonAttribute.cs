using System;

namespace biorand.app.Attributes.DependencyInjectionAttributes
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class RegisterSingletonAttribute : Attribute
    {
    }
}
