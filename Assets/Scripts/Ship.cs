using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    public static Ship Instance { get; private set; }
    [SerializeField]
    private float moveSpeed;
    private Rigidbody2D rb2d;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private bool canShoot = true;
    [SerializeField]
    private float timeToShoot = 0.75f;
    private Button btnShoot;
    private bool isMovingUp, isMovingDown, isShooting;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || isMovingUp)
        {
            MoveUp();
        }
        else if (Input.GetKey(KeyCode.DownArrow) || isMovingDown)
        {
            MoveDown();
        }
        else
        {
            Stop();
        }

        if ((Input.GetKey(KeyCode.Space) || isShooting) && canShoot)
        {
            canShoot = false;
            Shoot();
            StartCoroutine(CO_Shoot());
        }
    }

    private IEnumerator CO_Shoot()
    {
        yield return new WaitForSeconds(timeToShoot);
        canShoot = true;
    }

    public void StartMovingUp()
    {
        isMovingUp = true;
    }

    public void StopMovingUp()
    {
        isMovingUp = false;
    }

    public void StartMovingDown()
    {
        isMovingDown = true;
    }

    public void StopMovingDown()
    {
        isMovingDown = false;
    }

    public void StartShooting()
    {
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
    }

    private void MoveUp()
    {
        Move(moveSpeed);
    }

    private void MoveDown()
    {
        Move(-moveSpeed);
    }

    private void Stop()
    {
        Move(0f);
    }

    private void Move(float speed)
    {
        rb2d.velocity = new Vector2(0, speed);
    }

    private void Shoot()
    {
        var laserPoint = GameObject.Find("LaserPoint").transform;
        Instantiate(laserPrefab, new Vector3(laserPoint.position.x, laserPoint.position.y, 0f), Quaternion.identity);
    }
}
