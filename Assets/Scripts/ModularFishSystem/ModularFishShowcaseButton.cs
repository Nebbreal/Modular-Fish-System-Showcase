using UnityEngine;

public class ModularFishShowcaseButton : MonoBehaviour
{
   [SerializeField]
   private FishGenerator fishGenerator;

   private GameObject _lastSpawnedFish;
   public void GenerateFish()
   {
      if(_lastSpawnedFish) Destroy(_lastSpawnedFish);
      
      GameObject fish = fishGenerator.GenerateFish();
      fish.SetActive(true);
      
      _lastSpawnedFish = fish;
      
      fish.transform.position = Vector3.zero;
   }
}
