namespace Reoria.Client.Core.Screens.Attributes;

/// <summary>
/// Defines a marker attribute to mark a screen as the default screen to open first.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class DefaultScreenAttribute : Attribute
{
    // Nothing needs to be done in this class, it is a marker attribute.
}