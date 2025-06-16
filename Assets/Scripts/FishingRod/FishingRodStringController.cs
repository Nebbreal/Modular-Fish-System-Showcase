using UnityEngine;

public class FishingRodStringController : MonoBehaviour
{
    [SerializeField] private GameObject stringTop;
    [SerializeField] private GameObject rodTip;
    private float _stringLength;
    private float _localStringPivotY;
    private float yString;
    
    [SerializeField] private float stringLength;

    [SerializeField] AnimationCurve FishingRodCurve;

    private void Start()
    {
        float stringTopY = stringTop.transform.position.y;
        float rodTipY = rodTip.transform.position.y;

        _localStringPivotY = transform.localPosition.y;
        _stringLength = stringTopY - rodTipY;
    }
    
    public void RotateZ(float angle)
    {
        transform.localEulerAngles = new Vector3(0f, 0f, angle);
    }

    public void MoveDownwardWithPressure(float pressure, float yOffset = 0f)
    {
        //Get distance based on pressure
        float loweringDistance = pressure / 100f * _stringLength;
        Vector3 transformVector = new Vector3(0f, -1f * loweringDistance + _localStringPivotY + yOffset, 0f);
        
        transform.localPosition = transformVector;
        //Force the x position to be in line with the fishing rod tip
        transform.position = new Vector3(rodTip.transform.position.x, transform.position.y);
    }

    public float GetYOffset(bool _readyToReelIn)
    {
        if (_readyToReelIn)
        {
            yString = FishingRodCurve.Evaluate((Time.time % FishingRodCurve.length)) + stringLength;
        }
        return yString;
    }
}