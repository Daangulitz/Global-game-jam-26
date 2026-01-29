using UnityEngine;

[CreateAssetMenu(fileName = "Mask", menuName = "Scriptable Objects/Mask")]


public class Mask : ScriptableObject
{
    public Rarity rarity;
    public string maskName;
    public string description;
    public Sprite sprite;
    public Material material;
    public int id;
}
public enum Rarity
{
    Common,
    Uncommon,
    Special
}