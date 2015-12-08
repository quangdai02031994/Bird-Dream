using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour {

    /// <summary>
    /// Khai bao cac doi tuong bomb va coin
    /// </summary>
    public GameObject bomb;
    public GameObject coin;

    /// <summary>
    /// toa do max va min cua coin va bomb khi sinh ra
    /// </summary>
    public float _maxPositionX;
    public float _minPositionX;

    /// <summary>
    ///Tong so bomb va coin toi da cung xuat hien, luong bomb+coin se tang dan theo thoi gian
    /// </summary>
    public int _numberBomb;

    public int _maxChild;

    /// <summary>
    /// so luong bomb hoac coin hien co
    /// </summary>
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
                int t = Random.Range(0, 5);

                if (t == 0)
                {
                    GenerateObject(coin, rand);
                }
                else
                {
                    GenerateObject(bomb, rand);
                }
            }
        }
        else
        {
            _numberBomb = 1;
        }

        //if (GameController.Instance._isFevering)
        //{
        //    if (transform.childCount > 0)
        //        Destroy(transform.GetChild(0).gameObject);
        //}
        
    }

    public void GenerateObject(GameObject o,float postionX)
    {
        GameObject obj = Instantiate(o, Vector2.zero, Quaternion.identity) as GameObject;
        obj.transform.parent = this.gameObject.transform;
        obj.transform.localPosition = new Vector2(postionX, 0);
    }

}
