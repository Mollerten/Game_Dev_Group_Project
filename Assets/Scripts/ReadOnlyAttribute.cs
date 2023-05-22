using UnityEngine;

public class ReadOnlyAttribute : PropertyAttribute
{
    public readonly bool warningIfNull = false;

    public ReadOnlyAttribute(bool _warningIfNull = false)
    {
        warningIfNull = _warningIfNull;
    }
}
