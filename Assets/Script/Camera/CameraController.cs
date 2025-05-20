
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float rotateSpeed = 1f;
    private void Update()
    {
        Vector3 inputMoveDirection = new Vector3(0,0,0);
        if(Input.GetKey(KeyCode.W))
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
        Vector3 moveDirection = (inputMoveDirection.z * transform.forward  + inputMoveDirection.x * transform.right).normalized ;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

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


}
