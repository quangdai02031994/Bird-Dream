using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class IntroGameController : MonoBehaviour {
    
    public Camera _mainCamera;

    public GameObject btnShop;
    public GameObject btnExit;

    public Transform Background;
    public Transform Background2;
    
    public Transform BirdCage1;
    public Transform Bird1;

    public Transform Line;

    public Transform START;
    public Transform S;
    public Transform T1;
    public Transform A;
    public Transform R;
    public Transform T2;

    public float timeDuration;
    public float timeNextGame;


    public RuntimeAnimatorController anmBird2;
    public RuntimeAnimatorController anmBird3;

    public Sprite spBird2;
    public Sprite spBird3;

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    void Awake()
    {
        string t = PlayerPrefs.GetString(Configs.PlayName);
        if (t == ItemsShop.FlappyBird2)
        {
            Bird1.GetComponent<SpriteRenderer>().sprite = spBird2;
            Bird1.GetComponent<Animator>().runtimeAnimatorController = anmBird2;
        }
        else if (t == ItemsShop.FlappyBird3)
        {
            Bird1.GetComponent<SpriteRenderer>().sprite = spBird3;
            Bird1.GetComponent<Animator>().runtimeAnimatorController = anmBird3;
        }
    }

    void Start()
    {
        RotateLeft();
        Move_S();
    }

    void Update()
    {
        #region Chạm màn hình mobile
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    _startPosition = touch.position;
                    _endPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    _endPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    _endPosition = touch.position;
                    if (Mathf.Abs(_startPosition.x - _endPosition.x) > Mathf.Abs(_startPosition.y - _endPosition.y))
                    {
                        //do some things
                        if (Vector3.Distance(_startPosition, _endPosition) >= Screen.width / 2)
                        {
                            BirdCage1.DORotate(Vector3.zero, 0.3f).OnComplete(BirdCageStopTween);
                            StartCoroutine(BirdFlyOut());
                        }
                    }
                }
            }
        }
        #endregion
        //if (Input.GetMouseButtonDown(0))
        //{
        //    BirdCage1.DORotate(Vector3.zero, 0.3f).OnComplete(BirdCageStopTween);
        //    StartCoroutine(BirdFlyOut());
        //}
      
    }


    void RotateRight()
    {
        BirdCage1.DORotate(new Vector3(0, 0, 5), 1).OnComplete(RotateLeft);
    }

    void RotateLeft()
    {
        BirdCage1.DORotate(new Vector3(0, 0, -5), 1).OnComplete(RotateRight);
    }

    void BirdCageStopTween()
    {
        BirdCage1.DOKill();
    }

    IEnumerator BirdFlyOut()
    {
        btnShop.SetActive(false);
        btnExit.SetActive(false);
        Kill_START();
        yield return new WaitForSeconds(1);
        Bird1.DOMoveX(Bird1.localPosition.x + 4, timeNextGame).OnComplete(NextGame);
        Bird1.GetComponent<Animator>().speed = 2;
    }

    void NextGame()
    {
        Line.DOMoveY(Line.position.y + 10, timeNextGame);
        BirdCage1.DOMoveY(BirdCage1.position.y + 10, timeNextGame);
        Background.DOMoveY(12, timeNextGame);
        Background2.DOMoveY(0, timeNextGame).OnComplete(LoadGamePlay);
    }

    void LoadGamePlay()
    {
        Application.LoadLevel(SceneName.GamePlay);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadShopGame()
    {
        Application.LoadLevel(SceneName.ShopGame);
    }

    private void Move_S()
    {
        S.DOMoveY(-3, timeDuration).OnComplete(Resert_S);
    }
    private void Resert_S()
    {
        S.DOMoveY(-3.5f, timeDuration).OnComplete(Move_T1);
    }
    private void Move_T1()
    {
        T1.DOMoveY(-3, timeDuration).OnComplete(Resert_T1);
    }
    private void Resert_T1()
    {
        T1.DOMoveY(-3.5f, timeDuration).OnComplete(Move_A);
    }
    private void Move_A()
    {
        A.DOMoveY(-3, timeDuration).OnComplete(Resert_A);
    }
    private void Resert_A()
    {
        A.DOMoveY(-3.5f, timeDuration).OnComplete(Move_R);
    }
    private void Move_R()
    {
        R.DOMoveY(-3, timeDuration).OnComplete(Resert_R);
    }
    private void Resert_R()
    {
        R.DOMoveY(-3.5f, timeDuration).OnComplete(Move_T2);
    }
    private void Move_T2()
    {
        T2.DOMoveY(-3, timeDuration).OnComplete(Resert_T2);
    }
    private void Resert_T2()
    {
        T2.DOMoveY(-3.5f, timeDuration).OnComplete(Move_S);
    }

    private void Kill_START()
    {
        S.DOKill();
        T1.DOKill();
        A.DOKill();
        R.DOKill();
        T2.DOKill();
        START.DOScale(0, timeDuration * 5).OnComplete(ActiveOff);

    }

    private void ActiveOff()
    {
        START.gameObject.SetActive(false);
    }

}
