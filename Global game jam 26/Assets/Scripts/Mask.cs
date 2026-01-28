using UnityEngine;

[CreateAssetMenu(fileName = "Mask", menuName = "Scriptable Objects/Mask")]
public class Mask : ScriptableObject
{
    public string maskName;
    public string description;
    public Sprite sprite;
    public int id;
}
