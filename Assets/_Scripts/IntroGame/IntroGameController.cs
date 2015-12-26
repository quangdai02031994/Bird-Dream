using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class IntroGameController : MonoBehaviour {

    public Transform BirdCage;
    public Transform Bird;
    public Transform Background;
    public Transform Background2;
    public Transform Line;


    public Text SlideToPlay;


    public Vector3 _startPosition;
    public Vector3 _endPosition;

    public Camera _mainCamera;

    void Start()
    {
        RotateLeft();
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
                        if (Vector3.Distance(_startPosition, _endPosition) >= 80)
                        {
                            BirdCage.DORotate(Vector3.zero, 0.3f).OnComplete(KillALL);
                            SlideToPlay.text = null;
                            StartCoroutine(BirdFlyOut());
                        }
                    }
                }
            }
        }
        #endregion
        if (Input.GetMouseButtonDown(0))
        {
            BirdCage.DORotate(Vector3.zero, 0.3f).OnComplete(KillALL);
            SlideToPlay.text = null;
            StartCoroutine(BirdFlyOut());
        }
      
    }


    void RotateRight()
    {
        BirdCage.DORotate(new Vector3(0, 0, 5), 1).OnComplete(RotateLeft);
    }

    void RotateLeft()
    {
        BirdCage.DORotate(new Vector3(0, 0, -5), 1).OnComplete(RotateRight);
    }

    void KillALL()
    {
        BirdCage.DOKill();
    }

    IEnumerator BirdFlyOut()
    {
        yield return new WaitForSeconds(1);
        Bird.DOMoveX(Bird.localPosition.x + 4, 5).OnComplete(NextGame);
        Bird.GetComponent<Animator>().speed = 2;
    }

    void NextGame()
    {
        Line.DOMoveY(Line.position.y + 10, 3);
        Background.DOMoveY(12, 3);
        Background2.DOMoveY(0, 3).OnComplete(LoadGamePlay);
    }

    void LoadGamePlay()
    {
        Application.LoadLevel(SceneName.GamePlay);
    }

}
