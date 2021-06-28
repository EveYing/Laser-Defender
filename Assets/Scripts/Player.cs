using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float paddingX;
    float paddingY;
    float coolDownTime = 0.2f;
    float shootingTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Camera gameCamera = Camera.main;
        Renderer renderer = GetComponent<Renderer>();

        paddingX = renderer.bounds.size.x / 2f;
        paddingY = renderer.bounds.size.y / 2f;

        xMin = gameCamera.ViewportToWorldPoint(Vector3.zero).x + paddingX;
        xMax = gameCamera.ViewportToWorldPoint(Vector3.one).x - paddingX;

        yMin = gameCamera.ViewportToWorldPoint(Vector3.zero).y + paddingY;
        yMax = gameCamera.ViewportToWorldPoint(Vector3.one).y - paddingY;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire()
    {
        if (Input.GetButton("Fire1"))
        {
            shootingTime += Time.deltaTime;
            if (shootingTime >= coolDownTime)
            {
                shootingTime = 0f;
                Vector3 firePosition = new Vector3(transform.position.x,
                                   transform.position.y + paddingY,
                                   transform.position.z);
                GameObject laser = Instantiate(laserPrefab, firePosition, Quaternion.identity);
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            }
        }
    }
}
