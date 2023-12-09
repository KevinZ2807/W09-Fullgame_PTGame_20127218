using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animatorController;
    public float horizontal;
    public float vertical;

    void Update() {
        if (Input.GetKeyDown(KeyCode.X)) {
        Vector2 raycastDir = gameObject.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(GetComponent<Rigidbody2D>().position + Vector2.up * 2f,
        raycastDir, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null) {
            NPC character = hit.collider.GetComponent<NPC>();
            if (character != null) {
                character.DisplayDialog();
            }
        }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 || vertical != 0) {
            animatorController.SetBool("IsMoving", true);
            if (!GetComponent<Player_Info>().audioS.isPlaying) GetComponent<Player_Info>().PlaySound(FindAnyObjectByType<Player_Info>().movingSound);
        } else {
            animatorController.SetBool("IsMoving", false);
            GetComponent<Player_Info>().audioS.Stop();
        }
        Vector2 position = transform.position;
        position.x += 5f * horizontal * Time.deltaTime;
        animatorController.SetFloat("Horizontal", horizontal);
        position.y += 5f * vertical * Time.deltaTime;
        animatorController.SetFloat("Vertical", vertical);
        transform.position = position;
    }
}
