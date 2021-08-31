using UnityEngine;

public class RenameFieldAttribute : PropertyAttribute
{
    public readonly string Name;
    public RenameFieldAttribute(string name) => Name = name;
}
