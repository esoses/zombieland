
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 10f;
    public Vector3 offSet;

    private void Start()
    {
        gameObject.GetComponent<Camera>().orthographicSize = target.gameObject.GetComponent<HeroController>().viewRange;
    }
    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desirePosition = target.position + offSet;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desirePosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
