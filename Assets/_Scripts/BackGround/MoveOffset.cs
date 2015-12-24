using UnityEngine;
using System.Collections;

public class MoveOffset : MonoBehaviour {

    private Renderer rend;

    public float offset;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        offset = Time.time * GameController.Instance._speedBackGround;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));

    }



}
