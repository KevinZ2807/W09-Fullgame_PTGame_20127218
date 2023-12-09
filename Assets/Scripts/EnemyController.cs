using System.Collections;
using UnityEngine;
using UnityEditor;


public class EnemyController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    public ParticleSystem smokeEffect;
    Rigidbody2D rb2D_;
    private Transform target;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    public float speed = 1.5f;
    public bool vertical;
    public float changeTime = 3.0f;
    float timer;
    int direction = -1;
    private bool m_FacingRight = true;
    private float timeUntilFire;
    private float bps = 1.0f;


    void Start() {
        rb2D_ = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    void Update() {
        smokeEffect.transform.position = gameObject.transform.position;
        timer -= Time.deltaTime;
        if (timer <0) {
            direction = -direction;
            timer = changeTime;
        }
        if (target == null) {
            FindTarget();
            return;

        }

        if (!CheckTargetIsInRange()) {
            target = null;
        } else {
            timeUntilFire += Time.deltaTime; // firing cooldown

            if (timeUntilFire >= 1f / bps) {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }
    void FixedUpdate() {
        Vector2 position = rb2D_.position;
        if (vertical) {
            position.y = position.y + Time.deltaTime * speed * direction;
        } else {
            position.x += Time.deltaTime * speed * direction;
        }

        rb2D_.MovePosition(position);
    }
    
    #if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
        
    }
    #endif
    private bool CheckTargetIsInRange() {
        return Vector2.Distance(target.position, transform.position) <= targetingRange; 
    }

    private void FindTarget() {

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, 
        (Vector2) transform.position, 0f, playerMask);

        if (hits.Length > 0) { // When we hit something
            target = hits[0].transform; // First enemy we hit
        }
    }
    private void Shoot() {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
        Debug.Log("Shoot");
    }

    private void FlipHorizontal()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		//transform.Rotate (0f, 180f, 0f);
	}

    // private void FlipVertical()
	// {
	// 	// Switch the way the player is labelled as facing.
	// 	m_FacingUp = !m_FacingUp;

	// 	// Multiply the player's x local scale by -1.
	// 	Vector3 theScale = transform.localScale;
	// 	theScale.y *= -1;
	// 	transform.localScale = theScale;
	// 	//transform.Rotate (0f, 180f, 0f);
	// }
}
