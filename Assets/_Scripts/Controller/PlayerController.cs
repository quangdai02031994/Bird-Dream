using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float _maxPositionX;
    public float _minPositionX;


    public Vector2 fingerStart;
    public Vector2 fingerEnd;

    void Start()
    {

    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    fingerStart = touch.position;
                    fingerEnd = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    //transform.Translate(touch.deltaPosition * Time.deltaTime);
                    transform.Translate(new Vector2(touch.deltaPosition.x, 0) * Time.deltaTime);
                }
            }
        }

        if (transform.position.x > _maxPositionX)
        {
            transform.position = new Vector2(_maxPositionX, transform.position.y);
        }
        else if (transform.position.x < _minPositionX)
        {
            transform.position = new Vector2(_minPositionX, transform.position.y);
        }

      
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == Tags.Coin)
        {
            if (!GameController.Instance._isFevering)
            {
                GameController.Instance._coinGame++;
                Destroy(other.gameObject);
                GameController.Instance.feverTime.localPosition = new Vector2(GameController.Instance.feverTime.localPosition.x + 60, 0);
            }
        }
        else if (other.gameObject.tag == Tags.Bomb)
        {
            int t = GameController.Instance._scoreGame;
            t = t * 95 / 100;
            GameController.Instance._scoreGame = t;
            Destroy(other.gameObject);
        }
    }

    

}
