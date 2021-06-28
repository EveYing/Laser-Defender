using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float padding = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(Vector3.zero).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(Vector3.one).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(Vector3.zero).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(Vector3.one).y - padding;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }
}
