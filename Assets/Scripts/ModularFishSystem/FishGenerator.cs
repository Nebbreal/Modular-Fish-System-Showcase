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
    private Sprite[] shiftableTopFinSprites, shiftableBottomFinSprites, setTopFinSprites, setBottomFinSprites;
    private const string TopFinTag = "TopFin"; //Tag serialization seems to be missing from unity so this is a band aid for now
    private const string BottomFinTag = "BottomFin"; //Tag serialization seems to be missing from unity so this is a band aid for now
    
    [Header("Configuration")]
    [SerializeField, Range(0f, 1f), Tooltip("Chance to select a randomly colored part for the fish. 0 = 0% chance, 1 = 100% chance")]
    private float shiftableColorChance;

    public GameObject GenerateFish()
    {
        //Get a random FishBase
        int randomIndex = Random.Range(0, fishBases.Length);
        GameObject fish = Instantiate(fishBases[randomIndex]);
        
        SpriteRenderer[] fishPartRenderers = fish.GetComponentsInChildren<SpriteRenderer>();

        //Assign a random sprite to each fish part
        foreach (SpriteRenderer fishPartRenderer in fishPartRenderers)
        {
            GameObject partObject = fishPartRenderer.gameObject;
            
            float randomValue = Random.Range(0f, 1f);
            Debug.Log(randomValue);
            
            Sprite[] sprites = GetSpritesForPart(partObject.tag, randomValue <= shiftableColorChance);

            if (sprites != null && sprites.Length > 0)
            {
                SetPartToRandomSprite(fishPartRenderer, sprites);
            }
            else
            {
                Debug.LogError($"Sprite for {partObject.name} not found, check if the correct tag is assigned to the fish part");
            }
        }
        
        return fish;
    }

    private Sprite[] GetSpritesForPart(string spriteTag, bool useShiftable)
    {
        switch (spriteTag)
        {
            case TopFinTag:
                return useShiftable ? shiftableTopFinSprites : setTopFinSprites;
            case BottomFinTag:
                return useShiftable ? shiftableBottomFinSprites : setBottomFinSprites;
            default:
                return null;
        }
    }
    
    private void SetPartToRandomSprite(SpriteRenderer fishPartRenderer, Sprite[] sprites)
    {
        int randomSpriteValue = Random.Range(0, sprites.Length);
        Sprite selectedSprite = sprites[randomSpriteValue];
        
        fishPartRenderer.sprite = selectedSprite;
    }
}
