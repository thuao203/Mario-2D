using UnityEngine;

public class Camera_move : MonoBehaviour
{
    [SerializeField]
    private float cameraMoveSpeed = 0f;
    private Transform cameraTransform;
    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }
    private void Update()
    {
        Vector3 newPosition = cameraTransform.position;
        newPosition.x += cameraMoveSpeed ;
        cameraTransform.position = newPosition;
    }
}
