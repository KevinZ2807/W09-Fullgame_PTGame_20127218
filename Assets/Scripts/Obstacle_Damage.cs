using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Damage : MonoBehaviour
{

    void OnTriggerStay2D(Collider2D other) {
        Player_Info controller = other.GetComponent<Player_Info>();
            if (controller != null) {
                controller.ChangeHealth(-1f);
                //controller.KnockedBack();
            }
    }
    // void OnTriggerEnter2D(Collider2D other) {
    // Player_Info controller = other.GetComponent<Player_Info>();
    //     if (controller != null) {
    //         controller.ChangeHealth(-1);
    //         //controller.KnockedBack();
    //     }
    // }
}
