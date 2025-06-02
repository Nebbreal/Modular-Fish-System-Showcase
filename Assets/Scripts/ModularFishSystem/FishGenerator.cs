using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class FishGenerator : MonoBehaviour
{   
    [Header("Fish parts")]
    [SerializeField]
    private GameObject[] fishBases;
    
    [SerializeField] 
    private Sprite[] shiftableTopFinSprites, setTopFinSprites,
        shiftableBottomFinSprites, setBottomFinSprites, 
        setEyeSprites, 
        shiftableMouthSprites, setMouthSprites, 
        shiftableRearSprites, setRearSprites;
    private const string TopFinTag = "TopFin"; //Tag serialization seems to be missing from unity so this is a band aid for now, this should be done with classes later
    private const string BottomFinTag = "BottomFin"; 
    private const string EyeTag = "Eye";
    private const string MouthTag = "Mouth";
    private const string RearFinTag = "RearFin";
    
    [Header("Configuration")]
    [SerializeField, Range(0f, 1f), Tooltip("Chance to select a randomly colored part for the fish. 0 = 0% chance, 1 = 100% chance")]
    private float shiftableColorChance;
    [SerializeField, Range(0f, 1f), Tooltip("Minimum value for the randomly selected color")]
    private float minimumValue;
    [SerializeField, Range(0f, 1f), Tooltip("Minimum saturation value for the randomly selected color")]
    private float minimumSaturation;

    public GameObject GenerateFish(bool randomizeBodyColor = true)
    {
        if (fishBases.Length == 0)
        {
            Debug.LogError("No fish found in FishBases");
            return null;
        }
        
        //Get a random FishBase
        int randomIndex = Random.Range(0, fishBases.Length);
        
        GameObject fish = Instantiate(fishBases[randomIndex]);
        fish.SetActive(false);
        Transform fishTransform = fish.transform;
        
        if (randomizeBodyColor && fish.TryGetComponent(out SpriteRenderer fishBaseRenderer))
        {
            SetRendererToRandomColor(fishBaseRenderer);
        }
        
        //Get the SpriteRenderers from only the direct child objects
        List<SpriteRenderer> fishPartRenderers = GetDirectChildSpriteRenderers(fishTransform);

        //Assign a random sprite to each fish part
        foreach (SpriteRenderer fishPartRenderer in fishPartRenderers)
        {
            GameObject partObject = fishPartRenderer.gameObject;
            float randomValue = Random.Range(0f, 1f);
            //Exception made for eye's due to them looking unnatural with a random color
            bool isColorShiftable = randomValue <= shiftableColorChance && !partObject.CompareTag(EyeTag);
            Sprite[] sprites = GetSpritesForPart(partObject.tag, isColorShiftable);

            if (sprites != null && sprites.Length > 0)
            {
                SetRendererToRandomSprite(fishPartRenderer, sprites, isColorShiftable);
            }
            else
            {
                Debug.LogError($"Sprite for {partObject.name} not found, check if the correct tag is assigned to the fish part");
            }
        }
        
        return fish;
    }

    #region Part modification
    private Sprite[] GetSpritesForPart(string spriteTag, bool useShiftable)
    {
        switch (spriteTag)
        {
            case TopFinTag:
                return useShiftable ? shiftableTopFinSprites : setTopFinSprites;
            case BottomFinTag:
                return useShiftable ? shiftableBottomFinSprites : setBottomFinSprites;
            case EyeTag:
                return setEyeSprites;
            case MouthTag:
                return useShiftable ? shiftableMouthSprites : setMouthSprites;
            case RearFinTag:
                return useShiftable ? shiftableRearSprites : setRearSprites;
            default:
                return null;
        }
    }
    
    private void SetRendererToRandomSprite(SpriteRenderer spriteRenderer, Sprite[] sprites, bool shiftColor = false)
    {
        int randomSpriteValue = Random.Range(0, sprites.Length);
        Sprite selectedSprite = sprites[randomSpriteValue];

        if (shiftColor)
        {
            SetRendererToRandomColor(spriteRenderer);
        }
        
        spriteRenderer.sprite = selectedSprite;
    }

    private void SetRendererToRandomColor(SpriteRenderer fishPartRenderer)
    {
        float randomHueValue = Random.Range(0f, 1f);
        float randomSaturationValue = Random.Range(minimumSaturation, 1f);
        float randomValue = Random.Range(minimumValue, 1f);
        
        Color newColor = Color.HSVToRGB(randomHueValue, randomSaturationValue, randomValue);
        fishPartRenderer.color = newColor;
    }
    #endregion

    #region Utility
    private static List<SpriteRenderer> GetDirectChildSpriteRenderers(Transform fishTransform)
    {
        List<SpriteRenderer> fishPartRenderers = new ();
        for (int i = 0; i < fishTransform.childCount; i++)
        {
            if (fishTransform.GetChild(i).TryGetComponent(out SpriteRenderer fishPartRenderer))
            {
                fishPartRenderers.Add(fishPartRenderer);
            };
        }
        
        return fishPartRenderers;
    }
    #endregion
}
