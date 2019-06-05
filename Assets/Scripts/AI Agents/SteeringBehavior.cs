using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavior : MonoBehaviour
{
    public float Mass = 15.0f;
    public float MaxVelocity = 3.0f;
    public float MaxForce = 15.0f;
    public float detectionDistance = 10.0f;
    public float wallAvoidance = 10.0f;
    public float wallAvoidanceFOV = 90.0f;
    public float waypointSeekDistance = 5.0f;
    public GameObject target;

    private Vector3 velocity;
    private RaycastHit2D hit;
    private RaycastHit2D circleHit;
    private List<RaycastHit2D> feelers = new List<RaycastHit2D>();
    private Collider2D[] hitColliders;
    private LayerMask layerMask;
    private Waypoint waypoint;
    private Vector3 wanderTarget; 

    [HideInInspector] public Vector3 _steeringForce;

    public enum Deceleration : int
    {
        none,
        fast,
        normal,
        slow
    }

    public virtual void Start()
    {
        layerMask = LayerMask.GetMask("Obstacles");
        velocity = Vector3.zero;
        waypoint = GetComponent<Waypoint>();
        _steeringForce = Vector3.zero;
        wanderTarget = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * AIFlockingController.Instance.wanderRadius;
    }

    public virtual void Update()
    {
        
    }

    public virtual void FixedUpdate()
    {
        
    }

    public bool AccumulateForce(Vector3 forceToAdd)
    {
        float magnitudeSofar = _steeringForce.magnitude;
        float magnitudeRemaining = MaxForce - magnitudeSofar;

        if (magnitudeRemaining <= 0.0)
        {
            return false;
        }

        float magnitudeToAdd = forceToAdd.magnitude;

        if (magnitudeToAdd < magnitudeRemaining)
        {
            _steeringForce += forceToAdd;
        } else
        {
            _steeringForce += forceToAdd.normalized * magnitudeRemaining;
        }

        return true;
    }

    public List<Collider2D> FindNeighborAgents()
    {
        List<Collider2D> neighbors = new List<Collider2D>();
        Collider2D[] hitDetected = Physics2D.OverlapCircleAll(transform.position, AIFlockingController.Instance.agentNeighborRadius);

        for (int i = 0; i < hitDetected.Length; i++)
        {
            if (hitDetected[i].tag == "Flock" && (hitDetected[i].name != transform.name))
            {
                Debug.DrawLine(transform.position, hitDetected[i].transform.position, Color.black);
                neighbors.Add(hitDetected[i]);
            }
            
        }

        return neighbors;
    }

    public void ObstacleDetectionRoutines()
    {
        hit = Physics2D.BoxCast(transform.position, transform.localScale, transform.eulerAngles.z, transform.right, detectionDistance);

        if (hit.collider != null)
        {
            Debug.DrawLine(transform.position, hit.point, Color.gray);
        }
    }

    public void HidingDetectionRoutines()
    {
        hitColliders = Physics2D.OverlapCircleAll(target.transform.position, 20.0f, layerMask);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            Debug.DrawLine(target.transform.position, hitColliders[i].transform.position, Color.black);
            Debug.DrawRay(hitColliders[i].transform.position, (hitColliders[i].transform.position - target.transform.position).normalized * hitColliders[i].transform.localScale.magnitude, Color.red);
        }
    }

    public void WallDetectionRoutines()
    {
        feelers.Clear();

        Quaternion fovAngle = Quaternion.AngleAxis(wallAvoidanceFOV / 2.0f, new Vector3(0.0f, 0.0f, 1.0f));

        Vector3 feelerTop = fovAngle * transform.right;
        Vector3 feelerCenter = Quaternion.Inverse(fovAngle) * feelerTop;
        Vector3 feelerBottom = Quaternion.Inverse(fovAngle) * feelerCenter;

        feelers.Add(Physics2D.Raycast(transform.position, feelerTop, wallAvoidance));
        feelers.Add(Physics2D.Raycast(transform.position, feelerCenter, wallAvoidance));
        feelers.Add(Physics2D.Raycast(transform.position, feelerBottom, wallAvoidance));

        if (feelers[0].collider != null)
        {
            Debug.DrawRay(transform.position, feelerTop * feelers[0].distance, Color.red);
            Debug.DrawRay(feelers[0].point, feelers[0].normal * (wallAvoidance - feelers[0].distance), Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, feelerTop * wallAvoidance, Color.cyan);
        }

        if (feelers[1].collider != null)
        {
            Debug.DrawRay(transform.position, feelerCenter * feelers[1].distance, Color.red);
            Debug.DrawRay(feelers[1].point, feelers[1].normal * (wallAvoidance - feelers[1].distance), Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, feelerCenter * wallAvoidance, Color.cyan);
        }

        if (feelers[2].collider != null)
        {
            Debug.DrawRay(transform.position, feelerBottom * feelers[2].distance, Color.red);
            Debug.DrawRay(feelers[2].point, feelers[2].normal * (wallAvoidance - feelers[2].distance), Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, feelerBottom * wallAvoidance, Color.cyan);
        }
    }

    public Vector3 Cohesion(List<Collider2D> neighbors)
    {
        Vector3 centerOfMass = new Vector3(0.0f, 0.0f);
        Vector3 steeringForce = new Vector3(0.0f, 0.0f);

        foreach(Collider2D neighbor in neighbors)
        {
            centerOfMass += neighbor.transform.position;
        }

        if (neighbors.Count > 0)
        {
            centerOfMass /= (float)neighbors.Count;

            steeringForce = Seek(centerOfMass);
        }

        return steeringForce;
    }

    public Vector3 Alignment(List<Collider2D> neighbors)
    {
        Vector3 averageHeading = new Vector3(0.0f, 0.0f);
        
        foreach(Collider2D neighbor in neighbors)
        {
            averageHeading += neighbor.transform.right;
        }

        if (neighbors.Count > 0)
        {
            averageHeading /= (float)neighbors.Count;

            averageHeading -= transform.right;
                       
        }

        
        return averageHeading;
    }

    public Vector3 Seperation(List<Collider2D> neighbors)
    {
        Vector3 steeringForce = new Vector3(0.0f, 0.0f);

        foreach(Collider2D neighbor in neighbors)
        {
            Vector3 toAgent = transform.position - neighbor.transform.position;
            toAgent.z = 0.0f;
            steeringForce += toAgent.normalized / toAgent.magnitude;
        }

        
        return steeringForce;
    }

    public Vector3 OffsetPursuit(Rigidbody2D leader, Vector3 offset)
    {
        offset = leader.transform.TransformVector(offset);
        Vector3 worldOffset = leader.transform.position + offset;
        Vector3 toOffset = worldOffset - transform.position;

        float lookAheadTime = toOffset.magnitude / (MaxVelocity + leader.velocity.magnitude);
        
        return Arrive(worldOffset + (Vector3)leader.velocity * lookAheadTime, 0.0f, Deceleration.fast);
    }

    public Vector3 FollowPath()
    {
        if((waypoint.CurrentWaypoint() - transform.position).sqrMagnitude < waypointSeekDistance)
        {
            waypoint.SetNextWaypoint();
        }

        if(!waypoint.isFinished)
        {
            return Seek(waypoint.CurrentWaypoint());
        } else
        {
            return Arrive(waypoint.CurrentWaypoint(), 0.0f, Deceleration.normal);
        }
    }

    public Vector3 Interpose(Rigidbody2D targetA, Rigidbody2D targetB)
    {
        Vector3 midPoint = (targetA.transform.position + targetB.transform.position) / 2.0f;

        float timeToReachMidPoint = (midPoint - transform.position).magnitude / MaxVelocity;

        Vector3 aPos = targetA.transform.position + (Vector3)targetA.velocity * timeToReachMidPoint;
        Vector3 bPos = targetB.transform.position + (Vector3)targetB.velocity * timeToReachMidPoint;

        midPoint = (aPos + bPos) / 2.0f;

        return Arrive(midPoint, 0.0f, Deceleration.fast);
    }

    public Vector3 WallAvoidance()
    {
        float distanceToClosest = Mathf.Infinity;
        RaycastHit2D closestWall = new RaycastHit2D();

        foreach (RaycastHit2D ahit in feelers)
        {
            if (ahit.collider != null)
            {
                if (ahit.distance < distanceToClosest)
                {
                    distanceToClosest = ahit.distance;
                    closestWall = ahit;
                }
            }
        }

        if (closestWall.collider != null)
        {
            float overShoot = wallAvoidance - closestWall.distance;
            Vector3 steeringForce = closestWall.normal * overShoot;
            return steeringForce;
        }

        return Vector3.zero;
    }

    public Vector3 ObstacleAvoidance()
    {
        if(hit.collider != null)
        {
            float multiplier = 1.0f + (detectionDistance - hit.distance) / detectionDistance;
            float lateralForce = transform.InverseTransformPoint(hit.point).y * multiplier;

            float brakingWeight = 0.5f;
            float brakingForce = transform.InverseTransformPoint(hit.point).x * brakingWeight;
                                               
            return transform.TransformVector(new Vector3(-brakingForce, -lateralForce));
        }
        return Vector3.zero;
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

    public float TurnAroundTime(Rigidbody2D target)
    {
        Vector3 toTarget = target.transform.position - transform.position;
        
        float dotProduct = Vector3.Dot(toTarget.normalized, transform.right);

        const float coefficient = 0.5f;

        return (dotProduct - 1.0f) * -coefficient;
    }

    public Vector3 Wander()
    {
        float wanderRadius = AIFlockingController.Instance.wanderRadius;
        float wanderDistance = AIFlockingController.Instance.wanderDistance;
        float wanderJitter = AIFlockingController.Instance.wanderJitter;

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, Random.Range(-1.0f, 1.0f) * wanderJitter);

        wanderTarget = wanderTarget.normalized;

        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(wanderDistance, 0.0f);

        //Vector3 toTarget = targetLocal - transform.position;

        return transform.InverseTransformVector(targetLocal) - transform.position;
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
