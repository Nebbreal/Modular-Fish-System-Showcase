using UnityEngine;

public class FishingRodString : MonoBehaviour
{
    [SerializeField] private GameObject stringTop;
    [SerializeField] private GameObject rodTip;
    
    [SerializeField] private GameObject stringSpriteMask;
    private float _stringLength;
    private float _localStringPivotY;
    
    [SerializeField] private GameObject hook;
    [SerializeField] private Vector3 hookedFishScale = new(3f, 3f, 3f);
    private Transform _hookTransform;
    
    [SerializeField] AnimationCurve fishingRodStringCurve;
    private float _hoverYOffset;
    private float _curveTimer;
    
    private void Start()
    {
        float stringTopY = stringTop.transform.position.y;
        float rodTipY = rodTip.transform.position.y;
        
        _hookTransform = hook.transform;
        _localStringPivotY = transform.localPosition.y;
        _stringLength = stringTopY - rodTipY;
    }

    private void Update()
    {
        _curveTimer += Time.deltaTime;
    }
    
    public void RotateZ(float angle)
    {
        Vector3 eulerAngle = new Vector3(0f, 0f, angle);
        
        //Counteract the rotation of the rod to keep the tip (and mask) straight
        rodTip.transform.localEulerAngles = eulerAngle;
    }

    public void MoveDownwardWithPressure(float pressure, float yOffset = 0f)
    {
        //Get distance based on pressure
        float loweringDistance = pressure / 100f * _stringLength;
        Vector3 transformVector = new Vector3(0f, -1f * loweringDistance + _localStringPivotY + yOffset, 0f);
        
        transform.localPosition = transformVector;
        
        //Force the x position to be in line with the fishing rod tip
        Vector3 localPos = transform.localPosition;
        transform.localPosition = new Vector3(0f, localPos.y, 0f);
        stringSpriteMask.transform.localPosition = new Vector3(0f, stringSpriteMask.transform.localPosition.y, 0f);

    }
    
    public float GetHoverYOffset(bool _readyToReelIn)
    {
        if (_readyToReelIn)
        {
            _hoverYOffset = fishingRodStringCurve.Evaluate(_curveTimer % fishingRodStringCurve.length);
        }
        else
        {
            _curveTimer = 0f;
        }
        
        return _hoverYOffset;
    }

    public void AttachFish(GameObject fish)
    {
        Transform fishTransform = fish.transform;
        
        fishTransform.SetParent(_hookTransform);
        fishTransform.localPosition = Vector3.zero;
        fishTransform.localScale = hookedFishScale;
        fishTransform.rotation = Quaternion.Euler(0f, 0f, -90f);
    }

    public void DetachFish(GameObject fish)
    {
        Transform fishTransform = fish.transform;
        fishTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}