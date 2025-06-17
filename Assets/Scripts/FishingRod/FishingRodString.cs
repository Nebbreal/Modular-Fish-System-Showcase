using UnityEngine;

public class FishingRodString : MonoBehaviour
{
    [SerializeField] private GameObject stringTop;
    [SerializeField] private GameObject rodTip;
    [SerializeField] private GameObject topSpriteMask;
    private float _stringLength;
    private float _localStringPivotY;
    private float _topSpriteMaskY;

    private void Start()
    {
        float stringTopY = stringTop.transform.position.y;
        float rodTipY = rodTip.transform.position.y;
        
        _localStringPivotY = transform.localPosition.y;
        _stringLength = stringTopY - rodTipY;
        _topSpriteMaskY = topSpriteMask.transform.position.y;
    }
    
    public void RotateZ(float angle)
    {
        Vector3 eulerAngle = new Vector3(0f, 0f, angle);
        
        //Counteract the rotation of the rod to keep the rod (and mask) straight
        transform.localEulerAngles = eulerAngle;
        topSpriteMask.transform.localEulerAngles = eulerAngle;
    }

    public void MoveDownwardWithPressure(float pressure, float yOffset = 0f)
    {
        //Get distance based on pressure
        float loweringDistance = pressure / 100f * _stringLength;
        Vector3 transformVector = new Vector3(0f, -1f * loweringDistance + _localStringPivotY + yOffset, 0f);
        
        transform.localPosition = transformVector;
        
        //Force the x position to be in line with the fishing rod tip
        transform.position = new Vector3(rodTip.transform.position.x, transform.position.y);
        topSpriteMask.transform.position = new Vector3(rodTip.transform.position.x, topSpriteMask.transform.position.y);
    }
}