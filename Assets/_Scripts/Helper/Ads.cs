using UnityEngine;
using System.Collections;

public class Ads : MonoBehaviour {

    public static Ads Instance;
    public GoogleMobileAdBanner banner;


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
	
}
