using UnityEngine.UI;
using UnityEngine;

public class RubyHealth : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private Player_Info PlayerRuby;


    // Update is called once per frame
    void Update()
    {
        healthImage.fillAmount = PlayerRuby.currentHealth / 10f;
    }
}
