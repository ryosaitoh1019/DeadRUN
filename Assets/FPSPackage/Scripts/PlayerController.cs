using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour {

    public Camera camera;

    public AudioSource gunSound;

    int playerHP = 10;

	void Start () {
        Cursor.lockState = CursorLockMode.Locked;

	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Shot();
            gunSound.Play();
        }
    }
    void Shot()
    {
        int distance = 100;
        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = camera.ScreenPointToRay (Input.mousePosition);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, distance)) {
            if (hitInfo.collider.tag == "Enemy")
            {
                Destroy(hitInfo.collider.gameObject);
            }

        }
    }
}
