using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavior : MonoBehaviour
{
    public float Mass = 15;
    public float MaxVelocity = 3;
    public float MaxForce = 15;
    public float detectionDistance = 20.0f;
    public GameObject target;


    private Vector3 velocity;
    private Rigidbody2D rb2d;
    private RaycastHit2D results;


    public enum Deceleration : int
    {
        none,
        fast,
        normal,
        slow
    }

    private void Start()
    {
        velocity = Vector3.zero;
        rb2d = target.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        this.Steering(this.Wander());
    }

    private void FixedUpdate()
    {
        results = Physics2D.BoxCast(transform.position, transform.localScale, 0.0f, transform.right, detectionDistance);

        if (results.collider != null)
        {
            Debug.Log(transform.InverseTransformPoint(results.collider.transform.position));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (results.collider != null)
        {
            Gizmos.DrawWireCube(transform.position + transform.right * results.distance, transform.localScale);
        } else
        {
            Gizmos.DrawWireCube(transform.position + transform.right * detectionDistance, transform.localScale);
        }
    }

    public Vector3 Pursuit(Rigidbody2D evader)
    {
        Vector3 ToEvader = evader.transform.position - transform.position;
        float relativePosition = Vector3.Dot(ToEvader.normalized, transform.right);
        float relativeHeading = Vector3.Dot(evader.velocity.normalized, transform.right);

        if (relativePosition > 0 && relativeHeading < -0.95)
        {
            return Seek(evader.transform.position);
        }

        float lookAheadTime = ToEvader.magnitude / (MaxVelocity + evader.velocity.magnitude);

        return Seek(evader.transform.position + (Vector3)evader.velocity * lookAheadTime);
    }

    public Vector3 Evade(Rigidbody2D pursuer)
    {
        Vector3 ToPursuer = pursuer.transform.position - transform.position;

        float lookAheadTime = ToPursuer.magnitude / (MaxVelocity + pursuer.velocity.magnitude);

        return Flee(pursuer.transform.position + (Vector3)pursuer.velocity * lookAheadTime);
    }

    public Vector3 Wander()
    {
        float wanderRadius = 10.0f;
        float wanderDistance = 10.0f;
        float wanderJitter = 2.0f;

        Vector3 wanderTarget = new Vector3(wanderRadius, 0.0f);

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, Random.Range(-1.0f, 1.0f) * wanderJitter);

        wanderTarget = wanderTarget.normalized;

        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(wanderDistance, 0.0f);

        Vector3 targetWorld = transform.TransformVector(targetLocal);

        return targetWorld - transform.position;
    }

    public Vector3 Seek(Vector3 t)
    {
        Vector3 desiredVelocity = t - transform.position;
        desiredVelocity = desiredVelocity.normalized * MaxVelocity;
        return desiredVelocity;
    }

    public Vector3 Flee(Vector3 t)
    {
        Vector3 desiredVelocity = transform.position - t;
        desiredVelocity = desiredVelocity.normalized * MaxVelocity;
        return desiredVelocity;
    }

    public Vector3 Arrive(Vector3 t, float stopDistance, Deceleration deceleration)
    {
        Vector3 ToTarget = t - transform.position;
        float distance = ToTarget.magnitude;

        if (distance > stopDistance)
        {
            float decelerationAdjustment = 0.3f;
            float speed = distance / (((float)deceleration + 0.1f) * decelerationAdjustment);
            speed = Mathf.Min(speed, MaxVelocity);

            Vector3 desiredVelocity = ToTarget * speed / distance;
            return (desiredVelocity - velocity);
        }
        return Vector3.zero;
    }

    public void Steering(Vector3 desiredVelocity)
    {
        Vector3 steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, MaxForce);
        steering /= Mass;

        velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);

        transform.position += velocity * Time.deltaTime;
        transform.right = velocity.normalized;

        Debug.DrawRay(transform.position, velocity.normalized * 2, Color.green);
        Debug.DrawRay(transform.position, desiredVelocity.normalized * 2, Color.magenta);
    }
}
