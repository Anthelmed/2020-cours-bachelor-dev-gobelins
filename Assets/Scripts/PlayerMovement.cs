using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Via l'éditeur
    [SerializeField] private Camera camera;

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float minDistanceFloor = 1.1f;
    [SerializeField] private float jumpSpeed = 100f;


    private Camera _camera;
    private bool _isFalling = false;
    private Vector3 _moveDirection = Vector3.zero;

    private void Start()
    {
        //Via le script
        _camera = Camera.main; //Reviens à faire un GetComponentByTag
    }

    void Update()
    {
        _moveDirection = Vector3.zero;

        _moveDirection.x = Input.GetAxis("Horizontal");
        _moveDirection.z = Input.GetAxis("Vertical");

        if (Physics.Raycast(transform.position, Vector3.down, out var hit, Mathf.Infinity))
        {
            _isFalling = (hit.distance > minDistanceFloor);
        }

        if (Input.GetKey(KeyCode.Space) && !_isFalling)
        {
            _moveDirection.y += 1f;
        }

        _moveDirection.Normalize();

        transform.rotation = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0f);
    }

    private void FixedUpdate()
    {
        if (_moveDirection.y > 0f)
        {
            rigidbody.AddForce(Vector3.up * jumpSpeed);
        }

        var viewDirection = transform.TransformDirection(new Vector3(_moveDirection.x, 0f, _moveDirection.z));

        rigidbody.MovePosition(transform.position
            + viewDirection
            * speed
            * Time.fixedDeltaTime);
    }
}
