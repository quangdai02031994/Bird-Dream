using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class GameTutorial : MonoBehaviour {

    public GameObject coin;
    public GameObject bomb;

    public GameObject Player;

    public Transform Touch;
    public Transform One;
    public Transform Two;
    public Transform Three;

    public Camera _mainCamera;

    public Text txtScore;
    public Text txtCoin;

    public int _score;
    public int _playCoin;

    public bool _pauseGame;


    private GameObject _coin;
    private GameObject _bomb;


    public int _step = -1;
    public float t;


    void Start()
    {
        StartCoroutine(CreateBomb());
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (_pauseGame)
            {
                if (_step == 0)
                {
                    PlayerController.Instance.PlayTween();
                    Player.transform.DOMoveX(_mainCamera.ScreenToWorldPoint(Input.mousePosition).x, 1);
                    if (_bomb != null)
                        _bomb.GetComponent<Rigidbody2D>().isKinematic = false;
                    if (_coin != null)
                        _coin.GetComponent<Rigidbody2D>().isKinematic = false;
                    GameController.Instance._speedBackGround = 0.5f;
                    Touch.gameObject.SetActive(false);
                    Player.GetComponent<Animator>().enabled = true;
                    _pauseGame = false;
                    StartCoroutine(StepCreateCoin());
                }
                else if (_step == 1)
                {
                    PlayerController.Instance.PlayTween();
                    Player.transform.DOMoveX(_mainCamera.ScreenToWorldPoint(Input.mousePosition).x, 1);
                    if (_bomb != null)
                        _bomb.GetComponent<Rigidbody2D>().isKinematic = false;
                    if (_coin != null)
                        _coin.GetComponent<Rigidbody2D>().isKinematic = false;
                    GameController.Instance._speedBackGround = 0.5f;
                    Touch.gameObject.SetActive(false);
                    Player.GetComponent<Animator>().enabled = true;
                    InvokeRepeating("CreateRandomCoin", 1, 1.5f);
                    _step = 2;
                }
                else if (_step == 2)
                {
                    Player.transform.DOMoveX(_mainCamera.ScreenToWorldPoint(Input.mousePosition).x, 1);
                    Touch.gameObject.SetActive(false);
                }
                
            }
        }

        if (GameController.Instance._isFevering)
        {
            if (_step == 3)
            {
                Touch.gameObject.SetActive(false);
                InvokeRepeating("CreateRandomCoin", 1, 1.5f);
                StartCoroutine(PlayThree());
                _step = 4;
            }
        }

        txtScore.text = _score.ToString();
        _playCoin = GameController.Instance._coinGame;
        txtCoin.text = _playCoin.ToString();

        if (_playCoin >= 10)
        {
            if (_step == 2)
            {
                Touch.gameObject.SetActive(true);
                Touch.localPosition = new Vector3(1, -4.4f, 0);
                _step = 3;
                CancelInvoke();
            }
        }


        if (!_pauseGame || (_step > 1 && _step != 3))
        {
            t += Time.deltaTime;
            if (t > 1)
            {
                _score++;
                t = 0;
            }
        }
    }

    private void CreateRandomCoin()
    {
        _coin = Instantiate(coin, new Vector3(Random.Range(-2, 2), 6, 0), Quaternion.identity) as GameObject;
        _bomb = Instantiate(bomb, new Vector3(Random.Range(-2, 2), 6, 0), Quaternion.identity) as GameObject;
    }
    IEnumerator CreateBomb()
    {
        _bomb = Instantiate(bomb, new Vector3(0, 7, 0), Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(2f);
        if (_bomb != null)
            _bomb.GetComponent<Rigidbody2D>().isKinematic = true;
        GameController.Instance._speedBackGround = 0;
        Player.GetComponent<Animator>().enabled = false;
        PlayerController.Instance.PauseTween();
        StartCoroutine(ScaleTouch(new Vector3(2, 1.5f, 0)));
        _pauseGame = true;
        _step = 0;
    }

    IEnumerator ScaleTouch(Vector3 pos)
    {
        Touch.localPosition = pos;
        Touch.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Touch.DOScale(3, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    IEnumerator StepCreateCoin()
    {
        _coin = Instantiate(coin, new Vector3(-1, 7, 0), Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(3);
        if (_coin != null)
            _coin.GetComponent<Rigidbody2D>().isKinematic = true;
        GameController.Instance._speedBackGround = 0;
        Player.GetComponent<Animator>().enabled = false;
        PlayerController.Instance.PauseTween();
        Touch.localPosition = new Vector3(-1, 1.5f, 0);
        Touch.gameObject.SetActive(true);
        _step = 1;
        _pauseGame = true;
    }

    IEnumerator PlayThree()
    {
        _step = 5;
        Three.gameObject.SetActive(true);
        Tween tween1 = Three.DOScale(0, 3);
        yield return tween1.WaitForCompletion();
        CancelInvoke();
        StartCoroutine(PlayTwo());
    }

    IEnumerator PlayTwo()
    {
        Three.gameObject.SetActive(false);
        Two.gameObject.SetActive(true);
        Tween tween2 = Two.DOScale(0, 3);
        yield return tween2.WaitForCompletion();
        StartCoroutine(PlayOne());
    }

    IEnumerator PlayOne()
    {
        Two.gameObject.SetActive(false);
        One.gameObject.SetActive(true);
        Tween tween3 = One.DOScale(0, 3);
        yield return tween3.WaitForCompletion();
        PlayerPrefs.SetInt(Configs.Turn, 1);
        PlayerPrefs.Save();
        GameController.Instance._isTutorial = false;
        transform.gameObject.SetActive(false);
    }


}
