using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour
{

    public GameObject coin;

    public float _rate;

    public float index;

    public float _maxPosition;
    public float _minPosition;

    void Start()
    {

    }


    void Update()
    {
        if (GameController.Instance._isGamePlaying)
        {
            index += Time.deltaTime;
            if (index > _rate)
            {
                float rand = Random.Range(_minPosition, _maxPosition);
                GenerateCoin(rand);
                index = 0;
            }
        }
        
    }


    private void GenerateCoin(float position)
    {
        GameObject obj = Instantiate(coin, Vector2.zero, Quaternion.identity) as GameObject;
        obj.transform.parent = this.gameObject.transform;
        obj.transform.localPosition = new Vector2(position, 0);
    }
}