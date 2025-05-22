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

    public GameObject GenerateFish(bool randomizeBodyColor = true)
    {
        //Get a random FishBase
        int randomIndex = Random.Range(0, fishBases.Length);
        
        GameObject fish = Instantiate(fishBases[randomIndex]);
        fish.SetActive(false);
        Transform fishTransform = fish.transform;
        SpriteRenderer fishRenderer = fish.GetComponent<SpriteRenderer>();

        if (randomizeBodyColor)
        {
            SetRendererToRandomColor(fishRenderer);
        }
        
        //Get the SpriteRenderers from only the direct child objects
        SpriteRenderer[] fishPartRenderers = GetDirectChildSpriteRenderers(fishTransform);

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

    private static void SetRendererToRandomColor(SpriteRenderer fishPartRenderer)
    {
        float randomRedValue = Random.Range(0f, 1f);
        float randomGreenValue = Random.Range(0f, 1f);
        float randomBlueValue = Random.Range(0f, 1f);
            
        Color newColor = new Color(randomRedValue, randomGreenValue, randomBlueValue);
        fishPartRenderer.color = newColor;
    }
    #endregion

    #region Utility
    private static SpriteRenderer[] GetDirectChildSpriteRenderers(Transform fishTransform)
    {
        SpriteRenderer[] fishPartRenderers = new SpriteRenderer[fishTransform.childCount];
        for (int i = 0; i < fishPartRenderers.Length; i++)
            fishPartRenderers[i] = fishTransform.GetChild(i).GetComponent<SpriteRenderer>();
        return fishPartRenderers;
    }
    #endregion
}
