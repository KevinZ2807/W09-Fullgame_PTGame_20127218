using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Info : MonoBehaviour
{
    
    public AudioSource audioS;
    public AudioClip movingSound;
    public AudioClip hitSound;  
    public Rigidbody2D rb2D;
    public float maxHealth = 10f;
    public float currentHealth;

    public float timeInvincible = 2.0f;
    public bool isInvincible = false;  
    float invincibleTimer;

    public float GetCurrentHealth() {
        return currentHealth;
    }
    public void ChangeHealth(float amount) {
        if (amount < 0) {
            if (isInvincible) return;
            GetComponent<Animator>().SetTrigger("GetDamage");
            KnockedBack();
            PlaySound(hitSound);
            isInvincible = true;
            invincibleTimer = timeInvincible;
            GetComponent<PlayerMovement>().enabled = false;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    void KnockedBack() {
        Vector2 knock = new Vector2(FindAnyObjectByType<PlayerMovement>().horizontal * 50, FindAnyObjectByType<PlayerMovement>().vertical * 50);
        rb2D.AddForce(-knock * Time.deltaTime, ForceMode2D.Force);
        
    }

    public void PlaySound(AudioClip clip) {
        audioS.PlayOneShot(clip);
    }
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        audioS = GetComponent<AudioSource>();
        currentHealth = 10f;
    }

    void Update() {
        if (isInvincible) {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 1) 
                GetComponent<PlayerMovement>().enabled = true;
            if (invincibleTimer < 0) {
                isInvincible = false;
            }
        }
    }
}
