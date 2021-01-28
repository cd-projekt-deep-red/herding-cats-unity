using UnityEngine;

[CreateAssetMenu(fileName = "CatBreed", menuName = "ScriptableObjects/CatBreed", order = 1)]
public class CatBreed : ScriptableObject
{
    public string name;
    public Sprite variantSprite;
    public Material variantMaterial;
    public Color32[] colors;
}
