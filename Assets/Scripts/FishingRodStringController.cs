using UnityEngine;

public class FishingRodStringController : MonoBehaviour
{
    [SerializeField] private float stringLength = 120;
    private Camera cam;
    private Vector3 anchorOffset;
    private Vector3 startAnchorOffset;
    private float startPixelHeight;
    private float lastPixelHeight;

    private void Start()
    {
        startAnchorOffset = transform.position - transform.parent.position;
        anchorOffset = startAnchorOffset;
        cam = Camera.main;
        stringLength = cam.pixelHeight / 9;
        startPixelHeight = cam.pixelHeight;
        lastPixelHeight = cam.pixelHeight;
    }

    private void Update()
    {
        if (!Mathf.Approximately(cam.pixelHeight, lastPixelHeight))
        {
            stringLength = cam.pixelHeight / 9;
            anchorOffset = startAnchorOffset * cam.pixelHeight / startPixelHeight;
            lastPixelHeight = cam.pixelHeight;
        }
    }

    public void RotateDownward()
    {
        transform.rotation = Quaternion.Euler(Vector3.down);
    }

    public void MoveDownwardWithPressure(float pressure)
    {
        float loweringDistance = pressure / 100f * stringLength;
        Vector3 transformVector = Vector3.down * loweringDistance;

        Vector3 anchoredPosition = transform.parent.position + anchorOffset;
        Vector3 worldPosition = anchoredPosition + transformVector;
        transform.position = worldPosition;
    }
}