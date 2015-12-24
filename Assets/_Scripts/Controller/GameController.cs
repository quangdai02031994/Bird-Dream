using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class GameController : MonoBehaviour {

    public static GameController Instance;
    
    public GameObject player;
    public GameObject Tutorials;

    public GameObject bobm;
    public GameObject coin;

    public Text txt_Score;
    public Text txt_Coin;
    public RectTransform feverTime;
    public Button btnFever;
    public GameObject btnExit;


    public Transform Touch;

    public int _scoreGame;
    public int _coinGame;
    public int _countFever;

    public bool _isGamePlaying;
    public bool _isFevering;
    public bool _isTutorial;
    public bool _tutoPause;
    
    private float timeCount;
    private GameObject _bomb;


    public float _speedFevering = 10;
    public float _speedBackGround = 0.5f;

    void Awake()
    {
        Instance = this;
    }

	void Start () {

        RestartGame();
        player.transform.DOMove(Vector3.zero, 0.5f).OnComplete(EablePlayerScript);


	}

	void Update ()
    {
       
        if (_isTutorial)
        {
           
        }
        else
        {
            #region Game Playing

            if (Input.GetMouseButtonDown(0))
            {
                if (!_isGamePlaying)
                {
                    Touch.gameObject.SetActive(false);
                    _isGamePlaying = true;
                    btnExit.SetActive(true);
                }
            }
            if (_isGamePlaying)
            {
               
                timeCount += Time.deltaTime;
                txt_Score.text = _scoreGame.ToString();
                txt_Coin.text = _coinGame.ToString();


                if (timeCount > 1)
                {
                    _scoreGame++;
                    timeCount = 0;
                }

                CheckFeverTime();

                if (_countFever >= 10 && _countFever != 0 && _isFevering == false)
                {
                    btnFever.enabled = true;
                }
            }
            else
            {
                timeCount = 0;
                _scoreGame = 0;
                _coinGame = 0;
                txt_Score.text = "0";
                txt_Coin.text = "0";
                feverTime.localPosition = new Vector2(-600, 0);
            }
            #endregion
        }


    }

    private void CheckFeverTime()
    {
        
        if (_isFevering == false)
        {
            Time.timeScale = 1;
            if (feverTime.localPosition.x > 0)
            {
                feverTime.localPosition = new Vector2(0, 0);
            }
        }
        else if(_isFevering)
        {
            Time.timeScale = 2;
            feverTime.localPosition = new Vector2(feverTime.localPosition.x - Time.deltaTime * _speedFevering, 0);
        }
        if (feverTime.localPosition.x < -600)
        {
            feverTime.localPosition = new Vector2(-600, 0);
            _isFevering = false;
            _countFever = 0;
        }
        
    }

    private void RestartGame()
    {
        _isGamePlaying = false;
        _isFevering = false;
        timeCount = 0;
        _scoreGame = 0;
        _countFever = 0;
        _coinGame = 0;
        txt_Score.text = "0";
        txt_Coin.text = "0";
        //Touch.DOScale(1, 0.5f).SetLoops(-1, LoopType.Yoyo);
        //Ads.Instance.ShowBanner();
    }
   
    public void StartFevering()
    {
        _isFevering = true;
        btnFever.enabled = !btnFever.enabled;
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    private void EablePlayerScript()
    {
        player.transform.GetComponent<PlayerController>().enabled = true;
    }



}
