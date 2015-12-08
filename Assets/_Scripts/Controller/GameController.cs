using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController Instance;
    
    public Text txt_Score;
    public Text txt_Coin;
    public RectTransform feverTime;
    public Button btnFever;
    public Button btnPlayer;

    public Sprite spPlay;
    public Sprite spPause;

    public int _scoreGame;
    public int _coinGame;

    public bool _isGamePlaying;
    public bool _isFevering;
    
    private float index;

    public float speed = 10;
    
	void Start () {
        Instance = this;
        _isGamePlaying = false;
        _isFevering = false;
        index = 0;
        _scoreGame = 0;
        _coinGame = 0;
        txt_Score.text = "0";
        txt_Coin.text = "0";
	}
	
	void Update () {

        if (_isGamePlaying)
        {
            txt_Score.text = _scoreGame.ToString();
            txt_Coin.text = _coinGame.ToString();
            index += Time.deltaTime;

            if (index > 1)
            {
                _scoreGame++;
                index = 0;
            }

            CheckFeverTime();

            if (_coinGame % 10 == 0 && _coinGame != 0)
            {
                btnFever.enabled = true;
            }

        }
        else
        {
            index = 0;
            _scoreGame = 0;
            _coinGame = 0;
            txt_Score.text = "0";
            txt_Coin.text = "0";
            feverTime.localPosition = new Vector2(-600, 0);
        }
       
	}

    public void CheckFeverTime()
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
            feverTime.localPosition = new Vector2(feverTime.localPosition.x - Time.deltaTime * speed, 0);
        }
        if (feverTime.localPosition.x < -600)
        {
            feverTime.localPosition = new Vector2(-600, 0);
            _isFevering = false;
        }
        
    }

    public void SpeedTang()
    {
        _isFevering = true;
        btnFever.enabled = !btnFever.enabled;
        //_isGamePlaying = false;
    }

    public void PauseAndPlay()
    {
        _isGamePlaying = !_isGamePlaying;
        if (_isGamePlaying)
        {
            btnPlayer.image.overrideSprite = spPause;
        }
        else
        {
            btnPlayer.image.overrideSprite = spPlay;
        }
    }
}
