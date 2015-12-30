using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class GameController : MonoBehaviour {

    public static GameController Instance;
    
    public GameObject Player;
    public GameObject Tutorials;
    public GameObject BombParent;
    public GameObject CoinParent;
    
    public Transform Touch;

    public Text txt_Score;
    public Text txt_Coin;
    
    public Button btnFever;

    public RectTransform FeverTimeParent;
    public RectTransform _feverTimeChild;

    public int _scoreGame;
    public int _coinGame;
    public int _countFever;

    public bool _isGamePlaying;
    public bool _isFevering;
    public bool _isTutorial;
    public bool _startGame;
    
    private float timeCount;


    public float _speedFevering;
    public float _speedBackGround = 0.5f;

    void Awake()
    {
        Instance = this;
        if (PlayerPrefs.GetInt(Configs.Turn) == 0)
        {
            _isTutorial = true;
        }
        else
        {
            _isTutorial = false;
            Tutorials.gameObject.SetActive(false);
        }

    }

	void Start () {

        Player.transform.DOMove(Vector3.zero, 0.5f);
        if (!_isTutorial)
        {
            RestartGame();
        }
        else
        {
            Time.timeScale = 1;
            _isGamePlaying = false;
            _isFevering = false;
            timeCount = 0;
            _scoreGame = 0;
            _countFever = 0;
            _coinGame = 0;
            txt_Score.text = "0";
            txt_Coin.text = "0";
        }
	}

	void Update ()
    {
        if (Ads.Instance.BannerOnScreen)
        {
            FeverTimeParent.anchoredPosition = new Vector3(0, FeverTimeParent.sizeDelta.y / 2 + Ads.Instance.BannerHeigh, 0);
        }
        else
        {
            FeverTimeParent.anchoredPosition = new Vector3(0, FeverTimeParent.sizeDelta.y / 2, 0);
        }
        if (_isTutorial)
        {
            CheckFeverTime();
            if (_countFever >= 10 && _countFever != 0 && _isFevering == false)
            {
                btnFever.enabled = true;
            }
        }
        else
        {
            #region Game Playing
            if (_startGame)
            {
                BombParent.SetActive(true);
                CoinParent.SetActive(true);
                Touch.gameObject.SetActive(true);
                RestartGame();
                Player.transform.DOMove(Vector3.zero, 0.5f);
                _startGame = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!_isGamePlaying)
                {
                    Touch.gameObject.SetActive(false);
                    Player.GetComponent<PlayerController>().enabled = true;
                    _isGamePlaying = true;
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
                _feverTimeChild.localPosition = new Vector2(-800, 0);
            }
            #endregion
        }
    }

    private void CheckFeverTime()
    {
        
        if (!_isFevering)
        {
            Time.timeScale = 1;
            if (_feverTimeChild.localPosition.x > 0)
            {
                _feverTimeChild.localPosition = new Vector2(0, 0);
            }
        }
        else if(_isFevering)
        {
            Time.timeScale = 2;
            _feverTimeChild.localPosition = new Vector2(_feverTimeChild.localPosition.x - Time.deltaTime * _speedFevering, 0);
        }
        if (_feverTimeChild.localPosition.x < -800)
        {
            _feverTimeChild.localPosition = new Vector2(-800, 0);
            _isFevering = false;
            _countFever = 0;
        }
        
    }

    private void RestartGame()
    {
        Time.timeScale = 1;
        _isGamePlaying = false;
        _isFevering = false;
        timeCount = 0;
        _scoreGame = 0;
        _countFever = 0;
        _coinGame = 0;
        txt_Score.text = "0";
        txt_Coin.text = "0";
        Touch.DOScale(1.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        Ads.Instance.ShowBanner();
    }
   
    public void StartFevering()
    {
        _isFevering = true;
        btnFever.enabled = !btnFever.enabled;
    }

    public void BackIntroGame()
    {
        int i = PlayerPrefs.GetInt(Configs.Coin);
        i += _coinGame;
        PlayerPrefs.SetInt(Configs.Coin, i);
        PlayerPrefs.Save();
        Ads.Instance.HideBanner();
        Application.LoadLevel(SceneName.IntroName);
    }


}
