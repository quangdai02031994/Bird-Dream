using UnityEngine;
using System.Collections;

public class MoveOffset : MonoBehaviour {

    public Renderer rend;

    public float scrollSpeed = 0.5f;

    public float offset;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }



}
