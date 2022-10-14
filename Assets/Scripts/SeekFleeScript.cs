using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekFleeScript : MonoBehaviour
{
    public GameObject target;

    public bool fleeMode = false;

    private Vector3 velocity;

    public float mass = 15;
    public float maxVelocity = 3;
    public float maxForce = 15;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 finalVelocity;
        Vector3 desiredVelocity = target.transform.position - gameObject.transform.position;
        desiredVelocity = desiredVelocity.normalized * maxVelocity;

        Vector3 steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce);
        steering /= mass;

        velocity = Vector3.ClampMagnitude(velocity + steering, maxVelocity);

        if (fleeMode)
        {
            finalVelocity = -1 * velocity;
        } else
        {
            finalVelocity = velocity;
        }

        velocity.y = 0;

        gameObject.transform.position += finalVelocity * Time.deltaTime;
        gameObject.transform.forward = finalVelocity.normalized;
    }
}
