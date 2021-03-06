﻿using UnityEngine;
using System.Collections;

public class Ads : MonoBehaviour {

    public static Ads Instance;
    public GoogleMobileAdBanner banner;
    public int BannerHeigh
    {
        get
        {
            return banner.height;
        }
    }

    public bool BannerOnScreen
    {
        get
        {
            return banner.IsOnScreen;
        }
    }


    void Awake()
    {
        Instance = this;
        GoogleMobileAd.Init();
    }

    void Start()
    {
        banner = GoogleMobileAd.CreateAdBanner(TextAnchor.LowerCenter, GADBannerSize.SMART_BANNER);
        banner.ShowOnLoad = false;
    }

    public void ShowBanner()
    {
        banner.ShowOnLoad = true;
        if (banner.IsLoaded && banner.IsOnScreen == false)
            banner.Show();
    }

    public void HideBanner()
    {
        if (banner.IsOnScreen)
            banner.Hide();
    }

}
