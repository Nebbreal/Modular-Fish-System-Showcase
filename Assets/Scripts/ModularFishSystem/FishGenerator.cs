using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class FishGenerator : MonoBehaviour
{   
    [Header("Fish parts")]
    [SerializeField]
    private GameObject[] shiftableFishBases, setFishBases;

    [SerializeField] 
    private Sprite[] shiftableTopFinSprites, setTopFinSprites,
        shiftableBottomFinSprites, setBottomFinSprites, 
        setEyeSprites, 
        shiftableMouthSprites, setMouthSprites, 
        shiftableRearSprites, setRearSprites;

    [SerializeField] 
    private Sprite[] setHatSprites;
    private const string TopFinTag = "TopFin"; //Tag serialization seems to be missing from unity so this is a band aid for now, this should be done with classes later
    private const string BottomFinTag = "BottomFin"; 
    private const string EyeTag = "Eye";
    private const string MouthTag = "Mouth";
    private const string RearFinTag = "RearFin";
    private const string HatTag = "Hat";
    
    [Header("Configuration")]
    [SerializeField, Range(0f, 1f), Tooltip("Chance to select a randomly colored base for the fish. 0 = 0% chance, 1 = 100% chance")]
    private float shiftableBaseChance;
    [SerializeField, Range(0f, 1f), Tooltip("Chance to select a randomly colored part for the fish. 0 = 0% chance, 1 = 100% chance")]
    private float shiftablePartChance;
    [SerializeField, Range(0f, 1f), Tooltip("Chance for the fish to have a hat if applicable. 0 = 0% chance, 1 = 100% chance")]
    private float hatChance;
    [SerializeField, Range(0f, 1f), Tooltip("Minimum value for the randomly selected color")]
    private float minimumValue;
    [SerializeField, Range(0f, 1f), Tooltip("Minimum saturation value for the randomly selected color")]
    private float minimumSaturation;

    public GameObject GenerateFish()
    {
        if (shiftableFishBases.Length == 0 && !Mathf.Approximately(shiftableBaseChance, 0) || 
            setFishBases.Length == 0 && !Mathf.Approximately(shiftableBaseChance, 1))
        {
            Debug.LogError("No fish found in FishBases, please make sure that the shiftableBaseChance is set to 0 if there are no shiftable fish bases and 1 if there are no set fish bases.");
            return null;
        }
        
        float bodyColorRngValue = Random.Range(0f, 1f);
        bool isColorShiftable = bodyColorRngValue <= shiftableBaseChance;

        GameObject fish;
        Transform fishTransform;
        if (isColorShiftable)
        {
            int randomIndex = Random.Range(0, shiftableFishBases.Length);
            
            fish = InstantiateRandomFish(shiftableFishBases, randomIndex, out fishTransform);
            SpriteRenderer fishRenderer = fish.GetComponent<SpriteRenderer>();
            SetRendererToRandomColor(fishRenderer);
        }
        else
        {
            int randomIndex = Random.Range(0, setFishBases.Length);

            fish = InstantiateRandomFish(setFishBases, randomIndex, out fishTransform);
        }
        
        //Get the SpriteRenderers from only the direct child objects
        List<SpriteRenderer> fishPartRenderers = GetDirectChildSpriteRenderers(fishTransform);

        //Assign a random sprite to each fish part
        foreach (SpriteRenderer fishPartRenderer in fishPartRenderers)
        {
            GameObject partObject = fishPartRenderer.gameObject;
            //Roll the chance for a hat if applicable
            if (partObject.CompareTag(HatTag))
            {
                float hatRngValue = Random.Range(0f, 1f);

                if (hatRngValue >= hatChance)
                {
                    fishPartRenderer.sprite = null;
                    continue;
                }
            }
            
            float colorRngValue = Random.Range(0f, 1f);
            //Exception made for eye's and hats due to them looking unnatural with a random color
            //TODO: In rework make a blacklist instead of manually adding them here
            bool arePartsColorShiftable = colorRngValue <= shiftablePartChance && !partObject.CompareTag(EyeTag) && !partObject.CompareTag(HatTag);
            Sprite[] sprites = GetSpritesForPart(partObject.tag, arePartsColorShiftable);
            //Fall back to the other sprite array if none are found 
            if (sprites.Length == 0)
            {
                arePartsColorShiftable = !arePartsColorShiftable;
                sprites = GetSpritesForPart(partObject.tag, arePartsColorShiftable);
            }
            
            if (sprites != null && sprites.Length > 0)
            {
                SetRendererToRandomSprite(fishPartRenderer, sprites, arePartsColorShiftable);
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
            case HatTag:
                return setHatSprites;
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
    private GameObject InstantiateRandomFish(GameObject[] fishes, int randomIndex, out Transform fishTransform)
    {
        GameObject fish;
        fish = Instantiate(fishes[randomIndex]);
        fish.SetActive(false);
        fishTransform = fish.transform;
        return fish;
    }
    
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