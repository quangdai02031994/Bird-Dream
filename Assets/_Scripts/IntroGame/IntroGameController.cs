using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class IntroGameController : MonoBehaviour {
    
    public Camera _mainCamera;

    public Transform Background;
    public Transform Background2;
    
    public Transform BirdCage;
    public Transform Bird;

    public Transform Line;

    public Transform START;
    public Transform S;
    public Transform T1;
    public Transform A;
    public Transform R;
    public Transform T2;

    public float timeDuration;
    public float timeNextGame;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
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
                            BirdCage.DORotate(Vector3.zero, 0.3f).OnComplete(BirdCageStopTween);
                            StartCoroutine(BirdFlyOut());
                        }
                    }
                }
            }
        }
        #endregion
        //if (Input.GetMouseButtonDown(0))
        //{
        //    BirdCage.DORotate(Vector3.zero, 0.3f).OnComplete(BirdCageStopTween);
        //    StartCoroutine(BirdFlyOut());
        //}
      
    }


    void RotateRight()
    {
        BirdCage.DORotate(new Vector3(0, 0, 5), 1).OnComplete(RotateLeft);
    }

    void RotateLeft()
    {
        BirdCage.DORotate(new Vector3(0, 0, -5), 1).OnComplete(RotateRight);
    }

    void BirdCageStopTween()
    {
        BirdCage.DOKill();
    }

    IEnumerator BirdFlyOut()
    {
        Kill_START();
        yield return new WaitForSeconds(1);
        Bird.DOMoveX(Bird.localPosition.x + 4, timeNextGame).OnComplete(NextGame);
        Bird.GetComponent<Animator>().speed = 2;
    }

    void NextGame()
    {
        Line.DOMoveY(Line.position.y + 10, timeNextGame);
        BirdCage.DOMoveY(BirdCage.position.y + 10, timeNextGame);
        Background.DOMoveY(12, timeNextGame);
        Background2.DOMoveY(0, timeNextGame).OnComplete(LoadGamePlay);
    }

    void LoadGamePlay()
    {
        Application.LoadLevel(SceneName.GamePlay);
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
