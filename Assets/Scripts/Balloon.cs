using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField]
    private BalloonType balloonType;
    [SerializeField]
    private float baseSpeed;
    private Rigidbody2D rb2d;
    [SerializeField]
    private List<Sprite> sprites;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        int randomType = Random.Range(0, 3);

        balloonType = (BalloonType)randomType;

        GetComponent<SpriteRenderer>().sprite = sprites[randomType];

        float moveSpeed = baseSpeed;

        if (balloonType == BalloonType.Blue)
        {
            moveSpeed *= 2;
        }

        rb2d.velocity = new Vector2(0.0f, moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Laser>() != null)
        {
            if (balloonType == BalloonType.Blue)
            {
                GameManager.Instance.points += 10;
            }
            else
            {
                GameManager.Instance.points++;
            }

            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}

public enum BalloonType
{
    Red = 0, Green = 1, Blue = 2
}
