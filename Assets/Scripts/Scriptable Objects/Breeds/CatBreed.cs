using UnityEngine;

[CreateAssetMenu(fileName = "CatBreed", menuName = "ScriptableObjects/CatBreed", order = 1)]
public class CatBreed : ScriptableObject
{
    public string name;
    public RuntimeAnimatorController variantAnimationController;
    public Sprite variantSprite;
    public Material variantMaterial;
    public Color32[] colors;

    //cat breed behaviour multipliers
    public float breedFondnessMult = 1;
    public float breedSellValue = 5.0f;
    public float value = 10f;
}
