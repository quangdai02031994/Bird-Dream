using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {
    
    public GameObject Items;

    public GameObject Items1;
    public GameObject Items2;
    public GameObject Items3;

    public GameObject btnKey;
    public Text txtKey;
    public Text txtCoin;


    public int _coinFlappyBird2;
    public int _coinFlappyBird3;
    public int _coin;

    public Sprite spKeyNotActive;
    public Sprite spKeyActive;

    public Vector3 currentPosition;
    public Vector3 startPosition;
    public Vector3 endPosition;

    public string chooseItem;
    public int currentItem;
    public int allItems;



    void Start()
    {
        allItems = Items.transform.childCount - 1;
        chooseItem = ItemsShop.FlappyBird1;
        currentItem = 0;
        
    }

    void Update()
    {
        txtCoin.text = PlayerPrefs.GetInt(Configs.Coin).ToString();
       
        #region Dieu khien touch

            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        startPosition = touch.position;
                        endPosition = touch.position;
                        currentPosition = Items.transform.position;
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        endPosition = touch.position;
                        Vector3 touchDeltaPosition = touch.deltaPosition;
                        Items.transform.position = new Vector3(Items.transform.position.x + touchDeltaPosition.x * Time.deltaTime, Items.transform.position.y, Items.transform.position.z);
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        if (Items.transform.position.x > 0)
                        {
                            Items.transform.DOMoveX(0, 0.3f);
                        }
                        else if (Items.transform.position.x < -allItems * 7)
                        {
                            Items.transform.DOMoveX(-allItems * 7, 0.3f);
                        }
                        else
                        {
                            if (Mathf.Abs(endPosition.x - startPosition.x) > Mathf.Abs(endPosition.y - startPosition.y))
                            {
                                // swipe left
                                if (endPosition.x < startPosition.x)
                                {
                                    // do something
                                    Items.transform.DOMoveX(currentPosition.x - 7, 0.3f);
                                    currentItem--;
                                }
                                // swipe right
                                else if (endPosition.x > startPosition.x)
                                {
                                    // do something
                                    Items.transform.DOMoveX(currentPosition.x + 7, 0.3f);
                                    currentItem++;
                                }
                                currentPosition = Vector3.zero;
                            }
                            startPosition = Vector3.zero;
                            endPosition = Vector3.zero;
                        }
                    }
                }
            }
        
        #endregion

        #region Kiem tra item

        switch (currentItem)
        {
            case -1:
                {
                    if (PlayerPrefs.GetString(ItemsShop.FlappyBird2) == StateItems.Active)
                    {
                        txtKey.text = "";
                        btnKey.SetActive(false);
                        Items2.GetComponent<SpriteRenderer>().enabled = false;
                        Items2.GetComponentInChildren<Animator>().enabled = true;
                    }
                    else
                    {
                        txtKey.text = _coinFlappyBird2.ToString();
                        btnKey.SetActive(true);
                        CompareCoin(_coinFlappyBird2);
                    }
                }
                break;
            case -2:
                {
                    if (PlayerPrefs.GetString(ItemsShop.FlappyBird3) == StateItems.Active)
                    {
                        txtKey.text = "";
                        btnKey.SetActive(false);
                        Items3.GetComponent<SpriteRenderer>().enabled = false;
                        Items3.GetComponentInChildren<Animator>().enabled = true;
                    }
                    else
                    {
                        txtKey.text = _coinFlappyBird3.ToString();
                        btnKey.SetActive(true);
                        CompareCoin(_coinFlappyBird3);
                    }
                }
                break;

            default:
                {
                    txtKey.text = null;
                    btnKey.SetActive(false);
                    
                }
                break;
        }




        #endregion
    }


    private void CompareCoin(int coin)
    {
        if (PlayerPrefs.GetInt(Configs.Coin) > coin)
        {
            btnKey.GetComponent<Button>().image.overrideSprite = spKeyActive;
            btnKey.GetComponent<Button>().enabled = true;
        }
        else
        {
            btnKey.GetComponent<Button>().image.overrideSprite = spKeyNotActive;
            btnKey.GetComponent<Button>().enabled = false;
        }
    }


    private void ChooseItems()
    {
        switch (currentItem)
        {
            case -1:
                {
                    if (PlayerPrefs.GetString(ItemsShop.FlappyBird2) == StateItems.Active)
                        chooseItem = ItemsShop.FlappyBird2;
                }
                break;
            case -2:
                {
                    if (PlayerPrefs.GetString(ItemsShop.FlappyBird3) == StateItems.Active)
                        chooseItem = ItemsShop.FlappyBird3;
                }
                break;
            default:
                {
                    chooseItem = ItemsShop.FlappyBird1;
                }
                break;
        }
        PlayerPrefs.SetString(Configs.PlayName, chooseItem);
    }


    public void ActiveItems()
    {
        switch (currentItem)
        {
            case -1:
                {
                    PlayerPrefs.SetString(ItemsShop.FlappyBird2, StateItems.Active);
                    int t = PlayerPrefs.GetInt(Configs.Coin);
                    t -= _coinFlappyBird2;
                    PlayerPrefs.SetInt(Configs.Coin, t);
                }
                break;
            case -2:
                {
                    PlayerPrefs.SetString(ItemsShop.FlappyBird3, StateItems.Active);
                    int t = PlayerPrefs.GetInt(Configs.Coin);
                    t -= _coinFlappyBird3;
                    PlayerPrefs.SetInt(Configs.Coin, t);
                }
                break;
        }
    }


    public void LoadIntroGame()
    {
        ChooseItems();
        Application.LoadLevel(SceneName.IntroName);
    }
}
