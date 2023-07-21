using UnityEngine;

[CreateAssetMenu (fileName = "New Object Equivalent", menuName = "ObjectEquivalentSO")]
public class ObjectEquivalentSO : ScriptableObject
{
    /// <summary>
    /// This Scriptable Object will represent the value of the investment in the form of a car, chocolate bar, Iphone, etc.
    /// </summary>

    public GameObject objectEquivalent;

    public string objectName;
    public float cost;
}
