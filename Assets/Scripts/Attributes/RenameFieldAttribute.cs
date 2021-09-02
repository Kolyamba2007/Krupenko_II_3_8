using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class RenameFieldAttribute : PropertyAttribute
{
    public readonly string Name;
    public RenameFieldAttribute(string name) => Name = name;
}
