using UnityEngine;

public class FishingRodStringController : MonoBehaviour
{
    private float _stringLength;
    private Camera _cam;
    private Vector3 _anchorOffset;
    private Vector3 _startAnchorOffset;
    private float _startPixelHeightCamera;
    private float _lastPixelHeightCamera;

    private void Start()
    {
        _startAnchorOffset = transform.position - transform.parent.position;
        _anchorOffset = _startAnchorOffset;
        _cam = Camera.main;
        
        if (_cam)
        {
            _stringLength = _cam.pixelHeight / 9;
            _startPixelHeightCamera = _cam.pixelHeight;
            _lastPixelHeightCamera = _cam.pixelHeight;
        }
    }

    private void Update()
    {
        if (_cam && !Mathf.Approximately(_cam.pixelHeight, _lastPixelHeightCamera))
        {
            _stringLength = _cam.pixelHeight / 9;
            _anchorOffset = _startAnchorOffset * _cam.pixelHeight / _startPixelHeightCamera;
            _lastPixelHeightCamera = _cam.pixelHeight;
        }
    }

    public void RotateDownward()
    {
        transform.rotation = Quaternion.Euler(Vector3.down);
    }

    public void MoveDownwardWithPressure(float pressure)
    {
        float loweringDistance = pressure / 100f * _stringLength;
        Vector3 transformVector = Vector3.down * loweringDistance;

        Vector3 anchoredPosition = transform.parent.position + _anchorOffset;
        Vector3 worldPosition = anchoredPosition + transformVector;
        transform.position = worldPosition;
    }
}