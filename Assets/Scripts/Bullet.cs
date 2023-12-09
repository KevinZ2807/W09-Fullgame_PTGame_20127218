using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletDamage = -1f;
    [SerializeField] private float timeBeforeDestroy = 10f;
    
    
    private float timeStart = 0f;

    public void SetTarget(Transform _target) {
        target = _target;
    }

    private void Update() {

        // Thoi gian dan bay truoc khi bien mat
        timeStart += Time.deltaTime;
        if (timeStart >= timeBeforeDestroy) {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate() {
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;

    }

    private void OnCollisionEnter2D(Collision2D other) {
        // TAKE Health from enemy
        other.gameObject.GetComponent<Player_Info>().ChangeHealth(bulletDamage);
        Destroy(gameObject);
    }
}
