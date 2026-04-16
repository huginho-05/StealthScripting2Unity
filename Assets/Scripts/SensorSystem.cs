using UnityEngine;

public class SensorSystem : MonoBehaviour
{
    [field: SerializeField] public float SensorRadius { get; private set; }
    [field: SerializeField] public float SensorAngle { get; private set; }
    
    [SerializeField] private LayerMask whatIsTarget;
    [SerializeField] private LayerMask whatIsObstacle;

    private void FixedUpdate()
    {   
        SearchTarget();
    }

    public GameObject SearchTarget()
    {
        Collider[] results = Physics.OverlapSphere(transform.position, SensorRadius, whatIsTarget);

        if (results.Length > 0) //He detectado algo
        {
            Vector3 directionToTarget = (results[0].transform.position - transform.position);

            //Está dentro de mi ángulo
            if (Vector3.Angle(transform.forward, directionToTarget) <= SensorAngle / 2)
            {
                //No hay obstáculo entre enemigo y jugador
                if (!Physics.Raycast(transform.position + Vector3.up * 0.3f, directionToTarget, directionToTarget.magnitude, whatIsObstacle))
                {
                    return results[0].gameObject;
                }
            }
        }

        return null;
    }

    public Vector2 DirFromAngle(float angle, bool relativeToFront)
    {
        if (relativeToFront)
        {
            //Empiezo con el angulo de vision que ya tiene el enemigo
            angle += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
