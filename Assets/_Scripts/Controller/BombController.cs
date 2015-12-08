using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour {

   
    public GameObject bomb;
    public GameObject bomb2;
    public GameObject bomb3;

    public float _maxPositionX;
    public float _minPositionX;

    public int _numberBomb;
    public int _maxChild;

    public int _currentChild;

    public float timeCount;

    public int _rate;

    private float rand;

    void Start()
    {
        _numberBomb = 1;
        
    }

    void Update()
    {

        if (GameController.Instance._isGamePlaying)
        {
            _currentChild = transform.childCount;
            timeCount += Time.deltaTime;

            if (timeCount > _rate && _numberBomb < _maxChild)
            {
                _numberBomb++;
                timeCount = 0;
            }

            if (_currentChild < _numberBomb)
            {
                rand = Random.Range(_minPositionX, _maxPositionX);
                int x = Random.Range(0, 3);
                if (x == 0)
                {
                    GenerateObject(bomb, rand);
                }
                else if (x == 1)
                {
                    GenerateObject(bomb2, rand);
                }
                else
                {
                    GenerateObject(bomb3, rand);
                }
            }
        }
        else
        {
            _numberBomb = 1;
        }
    }

    public void GenerateObject(GameObject o,float postionX)
    {
        GameObject obj = Instantiate(o, Vector2.zero, Quaternion.identity) as GameObject;
        obj.transform.parent = this.gameObject.transform;
        obj.transform.localPosition = new Vector2(postionX, 0);
    }

}
