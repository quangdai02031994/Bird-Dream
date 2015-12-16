using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

    public float _maxPositionX;
    public float _minPositionX;

    public float _delay;

    public float _animBirdSpeed;

    public Tween _tweenPositon;
    
    private Animator _animBird;
    private Vector2 _endPosition;

    void Start()
    {
        _animBird = GetComponent<Animator>();
        //_animBird.speed = 0.5f;
        _animBird.speed = _animBirdSpeed;
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
                    _delay += Time.deltaTime;
                    _endPosition = Vector2.zero;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    _delay += Time.deltaTime;
                    if (_delay > 0.3f)
                        transform.Translate(new Vector2(touch.deltaPosition.x, 0) * Time.deltaTime);
                    _animBird.speed += Time.deltaTime * 5;
                    _endPosition = Input.mousePosition;
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
                    _delay = 0;
                    //transform.DOMoveX(_endPosition.x / 100, 0.2f);
                }
                
            }
        }
        else
        {
            //_animBird.speed = _animBirdSpeed;
            if (GameController.Instance._isFevering)
            {
                _animBird.speed = 5;
            }
            else
            {
                _animBird.speed -= Time.deltaTime * 5;
                if (_animBird.speed < 0.5f)
                {
                    _animBird.speed = 0.5f;
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

        if (transform.position.y > 0.2f)
        {
            transform.position = new Vector3(transform.position.x, 0.2f, 0);
        }
        else if (transform.position.y < -0.2f)
        {
            transform.position = new Vector3(transform.position.x, -0.2f, 0);
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
                transform.DOPunchRotation(new Vector3(0, 0, 270), 0.2f, 10, 1);
                Destroy(other.gameObject);
            }
            else
            {
                other.attachedRigidbody.AddForce(new Vector2(Random.RandomRange(-2, 2), Random.RandomRange(2, 3)) * 20, ForceMode2D.Impulse);
                StartCoroutine(DestroyBomb(other.gameObject));
            }
        }
    }

    void RotateUp()
    {
        transform.DOMoveY(0.2f, 0.5f);
        transform.DORotate(new Vector3(0, 0, 10), 0.5f).OnComplete(RotateDown);
    }

    void RotateDown()
    {
        transform.DOMoveY(-0.2f, 0.5f);
        transform.DORotate(new Vector3(0, 0, 350), 0.5f).OnComplete(RotateUp);
    }

    IEnumerator DestroyBomb(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        Destroy(obj);
        Debug.Log("Done");
    }
}

