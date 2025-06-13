using UnityEngine;

public class FishingRodStringController : MonoBehaviour
{
    [SerializeField] private GameObject stringTop;
    [SerializeField] private GameObject rodTip;
    private float _stringLength;
    private float _localStringPivotY;

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

    public void MoveDownwardWithPressure(float pressure)
    {
        float loweringDistance = pressure / 100f * _stringLength;
        Vector3 transformVector = new Vector3(0f, -1f * loweringDistance + _localStringPivotY, 0f);
        
        transform.localPosition = transformVector;
        transform.position = new Vector3(rodTip.transform.position.x, transform.position.y);
    }
}