using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class GameTutorial : MonoBehaviour {

    public GameObject coin;
    public GameObject bomb;

    public GameObject Player;

    public Transform Touch;

    public Camera _mainCamera;

    public Text txtScore;
    public Text txtCoin;

    public int _score;
    public int _playCoin;

    public bool _pauseGame;


    private GameObject _coin;
    private GameObject _bomb;


    //private int index = 0;
    public int _step = 0;
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
                    GameController.Instance._speedBackGround = 0.5f;
                    Touch.gameObject.SetActive(false);
                    Player.GetComponent<Animator>().enabled = true;
                    _pauseGame = false;
                }
            }
        }

        txtScore.text = _score.ToString();
        _playCoin = GameController.Instance._coinGame;
        txtCoin.text = _playCoin.ToString();
        if (!_pauseGame)
        {
            t += Time.deltaTime;
            if (t > 1)
            {
                _score++;
                t = 0;
            }
        }
    }

    private void CreateCoin()
    {
        _coin = Instantiate(coin, new Vector3(Random.Range(-2, 2), 6, 0), Quaternion.identity) as GameObject;
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
        StartCoroutine(MoveTouch());
        _pauseGame = true;
    }

    IEnumerator MoveTouch()
    {
        Touch.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Touch.DOScale(3, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

}
