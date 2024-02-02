using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 goalPosition = Vector3.zero;
    public float speed = 1;

    private Vector3 _max;
    private Vector3 _min;

    private CarryRigidbodies carryRigidbodies;
    private bool going = true;

    private float lastPingPong;

    private void Start()
    {
        _min = transform.position;
        _max = _min + goalPosition;
        carryRigidbodies = GetComponent<CarryRigidbodies>();
    }

    private void FixedUpdate()
    {
        float pingPong = Mathf.PingPong(Time.time * speed, 1);
        // transform.position = Vector3.Lerp(_min, _max, pingPong);
        GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(_min, _max, pingPong));


        if (pingPong < lastPingPong && going)
        {
            going = false;
            carryRigidbodies.SignalDirectionChange();
        }

        if (pingPong > lastPingPong && !going)
        {
            going = true;
            carryRigidbodies.SignalDirectionChange();
        }


        lastPingPong = pingPong;
    }
}