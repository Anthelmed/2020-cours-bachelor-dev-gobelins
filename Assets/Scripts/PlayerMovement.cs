using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera camera;

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float minDistanceFloor = 1.1f;
    [SerializeField] private float jumpSpeed = 100f;

    private bool _isFalling = false;
    private Vector3 _moveDirection = Vector3.zero;

    void Update()
    {
        _moveDirection = Vector3.zero;

        _moveDirection.x = Input.GetAxis("Horizontal");
        _moveDirection.z = Input.GetAxis("Vertical");

        //Envoi un rayon vers le sol (down) 
        if (Physics.Raycast(transform.position, Vector3.down, out var hit, Mathf.Infinity))
        {
            _isFalling = (hit.distance > minDistanceFloor);

        }

        if (Input.GetKey(KeyCode.Space) && !_isFalling)
        {
            _moveDirection.y += 1f;
        }

        _moveDirection.Normalize();

        //Tourne le player dans la même direction que la caméra
        transform.rotation = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0f);
    }

    private void FixedUpdate()
    {
        if (_moveDirection.y > 0f)
        {
            rigidbody.AddForce(Vector3.up * jumpSpeed);
        }

        //Converti le vecteur en prenant en compte que le transform du player est l'origin du world
        var viewDirection = transform.TransformDirection(new Vector3(_moveDirection.x, 0f, _moveDirection.z));

        rigidbody.MovePosition(transform.position
            + viewDirection
            * speed
            * Time.fixedDeltaTime);
    }
}
