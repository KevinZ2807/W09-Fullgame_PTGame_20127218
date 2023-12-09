
using UnityEngine;

public class CameraMan : MonoBehaviour
{
    public Transform player; // Tạo them 1 transform cua vat the camera muon di theo
    //public Vector2 offset = new Vector2(0, 0);
    public Transform forCamera;
    private Vector3 offset = new Vector3(0, 1, -10);  

    void Update()
    {
        forCamera.position = player.position + offset; 
    }
}
