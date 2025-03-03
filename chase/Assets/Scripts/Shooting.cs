using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bullet; // Bullet prefab
    private Camera mainCam;
    private Vector3 mousePos;

    private void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        Aim();
    }

    void Shoot()
    {
        Instantiate(bullet, shootingPoint.position, shootingPoint.rotation); // Spawn the bullet
    }

    void Aim()
    {
        // Vector3 mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        // mousePosition.z = 0f;
        // mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(mainCam.transform.position.z);

        mousePos = mainCam.ScreenToWorldPoint(mousePosition);

        // Vector2 direction = (mousePosition - shootingPoint.position).normalized;
        // Vector3 rotation = mousePos - transform.position;

        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(mousePosition);
        Vector2 direction = (mouseWorldPos - (Vector2)transform.position).normalized;

        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        float rotz = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // shootingPoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.Euler(0, 0, rotz);
    }
}
