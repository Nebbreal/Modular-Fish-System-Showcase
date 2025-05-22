using UnityEngine;

public class FishingRodStringController : MonoBehaviour
{
    [SerializeField] private float stringLength = 120;
    private Camera cam;
    private Vector3 anchorOffset;

    private void Start()
    {
        anchorOffset = transform.position - transform.parent.position;
        cam = Camera.main;
    }

    public void RotateDownward()
    {
        transform.rotation = Quaternion.Euler(Vector3.down);
        stringLength = cam.pixelHeight / 9;
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