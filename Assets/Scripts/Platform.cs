using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Vector3 direction = Vector3.forward;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float scale = 1f;

    private Vector3 _defaultPosition;

    private void Awake()
    {
        _defaultPosition = transform.position;
    }

    void Update()
    {
        transform.position = _defaultPosition
            + direction
            * Mathf.Sin(Time.realtimeSinceStartup * speed) * scale;
    }
}
