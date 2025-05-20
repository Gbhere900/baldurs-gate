
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] float zoomAmount = 1f;

    const float MIN_FOLLOW_Y_OFFSET = 2f; 
    const float MAX_FOLLOW_Y_OFFSET = 122f;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineTransposer cinemachineTransposer;
    private Vector3 targetFollowOffset;

    private void Awake()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }
    private void Update()
    {
        
        HandleMove();
        HandleRotate();
        HandleZoom();
    }

    private void HandleMove()
    {
        Vector3 inputMoveDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDirection.z = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDirection.z = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDirection.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDirection.x = 1;
        }
        Vector3 moveDirection = (inputMoveDirection.z * transform.forward + inputMoveDirection.x * transform.right).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void HandleRotate()
    {
        Vector3 inputRotateDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            inputRotateDirection.y = -1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            inputRotateDirection.y = 1;
        }
        transform.eulerAngles += inputRotateDirection * rotateSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += zoomAmount;
        }
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime);
    }
}
