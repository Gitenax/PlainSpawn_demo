using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _rotateSpeed = 10f;
    


    private void Update()
    {
        Move();
        Rotate();
    }


    private void Move()
    {
        var inputVector = new Vector2(
            Input.GetAxis("Horizontal"), 
            Input.GetAxis("Vertical"));

        Vector3 destination = new Vector3(inputVector.x, 0, inputVector.y);
        destination.Normalize();
        destination *= _moveSpeed;
        
        transform.Translate(destination * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 10f, transform.position.z);
        
        
        //transform.position = new Vector3(transform.position.x + destination.x, 0, transform.position.z + destination.z) * Time.deltaTime;
    }

    private void Rotate()
    {
        if (Input.GetMouseButton(2))
        {
            var direction = Input.GetAxis("Mouse X");
            transform.RotateAround(transform.position, Vector3.up, direction * _rotateSpeed * Time.deltaTime);
        }
    }
}
