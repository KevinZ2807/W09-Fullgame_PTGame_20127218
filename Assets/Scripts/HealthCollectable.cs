using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    public AudioClip sf;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Info PlayerController = collision.GetComponent<Player_Info>();
        Debug.Log(PlayerController);
        if (PlayerController != null) {
            if(PlayerController.currentHealth < PlayerController.maxHealth) {
                PlayerController.ChangeHealth(1f);
                PlayerController.PlaySound(sf);
                Destroy(gameObject);
            }
        }
    }
}
