using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

    public float _maxPositionX;
    public float _minPositionX;

    //public float _speed;

    private Animator _animBird;

    private Vector2 _endPosition;

    void Start()
    {
        _animBird = GetComponent<Animator>();
        _animBird.speed = 0.5f;
        RotateUp();
        
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    _endPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    _endPosition = touch.position;
                    transform.Translate(new Vector2(touch.deltaTime, 0) * Time.deltaTime);
                    _animBird.speed += Time.deltaTime * 5;
                }
                else if (touch.phase == TouchPhase.Stationary)
                {
                    _animBird.speed += Time.deltaTime * 5;
                    if (_animBird.speed > 3)
                    {
                        _animBird.speed = 3;
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    //transform.DOMoveX(_endPosition.x, 0.3f);
                    _endPosition = Vector2.zero;
                }
                
            }
        }
        else
        {
            _animBird.speed -= Time.deltaTime * 5;
            if (_animBird.speed < 0.5f)
            {
                _animBird.speed = 0.5f;
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


    /// <summary>
    /// Doan code nay de bat va cham voi coin hoac bomb
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == Tags.Coin)
        {
            if (!GameController.Instance._isFevering)
            {
                GameController.Instance._coinGame++;
                GameController.Instance._countFever++;
                Destroy(other.gameObject);
                GameController.Instance.feverTime.localPosition = new Vector2(GameController.Instance.feverTime.localPosition.x + 60, 0);
            }
            else
            {
                GameController.Instance._coinGame++;
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.tag == Tags.Bomb)
        {
            if (!GameController.Instance._isFevering)
            {
                int t = GameController.Instance._scoreGame;
                t = t * 95 / 100;
                GameController.Instance._scoreGame = t;
                Destroy(other.gameObject);
            }
            else
            {
                other.attachedRigidbody.AddForce(new Vector2(Random.value,Random.value) * 20, ForceMode2D.Impulse);
            }
        }
    }

    void RotateUp()
    {
        transform.DORotate(new Vector3(0, 0, 10), 0.5f).OnComplete(RotateDown);
    }

    void RotateDown()
    {
        transform.DORotate(new Vector3(0, 0, 350), 0.5f).OnComplete(RotateUp);
    }

}

