using UnityEngine;

public class Boid : MonoBehaviour  //fnc OnAttack() to update foreach boid subclass 
{
    public Vector3 velocity;
	//comportement normal
    private float cohesionRadius = 10;
    private float separationDistance = 5;
    private Collider[] boids;
    private Vector3 cohesion;
    private Vector3 separation;
    private int separationCount;
    private Vector3 alignment;
    private float maxSpeed = 15;

    private void Start()
    {
        InvokeRepeating("CalculateVelocity", 0, 0.1f);
    }

    void CalculateVelocity()
    {
        //valeur à changer selon la map?
	velocity = Vector3.zero;
        cohesion = Vector3.zero;
        separation = Vector3.zero;
        separationCount = 0;
        alignment = Vector3.zero;
	//cohesion et allignement des boids 
        boids = Physics.OverlapSphere(transform.position, cohesionRadius);
        foreach (var boid in boids)
        {
            cohesion += boid.transform.position;
            alignment += boid.GetComponent<Boid>().velocity;

            if (boid != collider && (transform.position - boid.transform.position).magnitude < separationDistance)
            {
                separation += (transform.position - boid.transform.position) / (transform.position - boid.transform.position).magnitude;
                separationCount++;
            }
        }

        cohesion = cohesion / boids.Length;
        cohesion = cohesion - transform.position;
        cohesion = Vector3.ClampMagnitude(cohesion, maxSpeed);
        if (separationCount > 0)
        {
            separation = separation / separationCount;
            separation = Vector3.ClampMagnitude(separation, maxSpeed);
        }
        alignment = alignment / boids.Length;
        alignment = Vector3.ClampMagnitude(alignment, maxSpeed);

        velocity += cohesion + separation * 10 + alignment * 1.5f;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
    }

    void Update()
    {
        //if(OnAttack())
	//{OnAttackBehavior()}
	//else
	//{
		if (transform.position.magnitude > 25)
        	{
            		velocity += -transform.position.normalized;
        	}

        transform.position += velocity * Time.deltaTime;
    	//}
    }

    void OnAttackBheavior()
    { /*maxspeed_a changer
    cohesion et allignement a changer 
    et finalment pour le beta et l'alpha une tragectoire special?*/}

    bool OnAttack()
    {/*Test avec un autre collider  */ }

}
