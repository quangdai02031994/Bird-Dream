using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance;
    
    public Camera _mainCamera;
    public GameObject Tutorial;

    public float _delay;
    public float _maxPositionX;
    public float _minPositionX;
    
    public Animator _animBird;
    
    public Vector3 _positionCamera;
    public Vector3 _endTouchPosition;

    public RuntimeAnimatorController anmBird2;
    public RuntimeAnimatorController anmBird3;

    public Sprite spBird2;
    public Sprite spBird3;

    void Awake()
    {
        Instance = this;
        string t = PlayerPrefs.GetString(Configs.PlayName);
        if (t == ItemsShop.FlappyBird2)
        {
            transform.GetComponent<SpriteRenderer>().sprite = spBird2;
            transform.GetComponent<Animator>().runtimeAnimatorController = anmBird2;
        }
        else if (t == ItemsShop.FlappyBird3)
        {
            transform.GetComponent<SpriteRenderer>().sprite = spBird3;
            transform.GetComponent<Animator>().runtimeAnimatorController = anmBird3;
        }
    }

    void Start()
    {
        _animBird = GetComponent<Animator>();
        RotateUp();
        _positionCamera = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        _maxPositionX = _positionCamera.x - 0.5f;
        _minPositionX = -_positionCamera.x + 0.5f;
    }

    void Update()
    {
        #region Điều khiển Player

        if (GameController.Instance._isGamePlaying)
        {
            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        _delay += Time.deltaTime;
                        _endTouchPosition = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        _delay += Time.deltaTime;
                        if (_delay >= 0.3f)
                        {
                            transform.Translate(new Vector2(touch.deltaPosition.x, 0) * Time.deltaTime);
                        }
                        _animBird.speed += Time.deltaTime * 5;
                        _endTouchPosition = touch.position;
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

                        if (_delay < 0.3f)
                        {
                            transform.DOMoveX(_mainCamera.ScreenToWorldPoint(_endTouchPosition).x, 1);
                            DOTween.To(() => transform.GetComponent<Animator>().speed, x => transform.GetComponent<Animator>().speed = x, 5, 0.3f);
                        }
                        _delay = 0;
                        _endTouchPosition = Vector3.zero;
                    }
                }
            }
            else
            {
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
        }
        
        #endregion

        #region Giới hạn toạ độ của Player trong Scene
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
        #endregion
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == Tags.Coin)
        {
            if (!GameController.Instance._isFevering)
            {
                GameController.Instance._coinGame++;
                GameController.Instance._countFever++;
                Destroy(other.gameObject);
                GameController.Instance._feverTimeChild.localPosition = new Vector2(GameController.Instance._feverTimeChild.localPosition.x + 80, 0);
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
                t = t * 70 / 100;
                GameController.Instance._scoreGame = t;
                transform.DOPunchRotation(new Vector3(0, 0, 270), 0.2f, 10, 1);
                Destroy(other.gameObject);
                if (Tutorial.activeSelf)
                {

                    Tutorial.GetComponent<GameTutorial>()._score = Tutorial.GetComponent<GameTutorial>()._score * 70 / 100;
                    if (Tutorial.GetComponent<GameTutorial>()._score < 0)
                    {
                        Tutorial.GetComponent<GameTutorial>()._score = 0;
                    }
                }
                
            }
            else
            {
                other.attachedRigidbody.AddForce(new Vector2(Random.Range(-2, 2), Random.Range(2, 3)) * 20, ForceMode2D.Impulse);
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
    }

    public void PauseTween()
    {
        transform.DOPause();
    }

    public void PlayTween()
    {
        transform.DOPlay();
    }

}

