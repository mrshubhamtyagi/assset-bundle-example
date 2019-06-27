using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadingAssetBundleFromURL : MonoBehaviour
{
    public string URL;
    private GameObject loadedObject;

    void Start()
    {
        URL = "https://uc2220704f0ecf89bd440a72aefa.dl.dropboxusercontent.com/cd/0/get/AjhJC5CmLA7Hk3a1__IDkzMrQ8L2R8Wo_hHipSgbCN5CgA8T6LwrapKjYn47PuYAPfxX_wFh4-4PDIx1SOXMu-zqZkMQYFYiVNk65Qx9Glgh6w/file?dl=1";
    }


    public void LoadCube()
    {
        if (loadedObject != null)
            if (loadedObject.name.Equals("Cube"))
                return;

        LoadAsset("Cube");
    }

    public void LoadCapsule()
    {
        if (loadedObject != null)
            if (loadedObject.name.Equals("Capsule"))
                return;

        LoadAsset("Capsule");
    }

    public void LoadText()
    {
        if (loadedObject != null)
            if (loadedObject.name.Equals("Text"))
                return;

        LoadAsset("Text");
    }

    private void LoadAsset(string _name)
    {
        if (loadedObject)
            Destroy(loadedObject);

        StartCoroutine(Co_LoadAssetUsingWWW(_name));
        //StartCoroutine(Co_LoadAssetUsingUnityWebRequest(_name));
    }

    private IEnumerator Co_LoadAssetUsingWWW(string _name)
    {
        while (!Caching.ready)
        {
            yield return null;
        }

        // Begin Download
        WWW w = WWW.LoadFromCacheOrDownload(URL, 0);
        yield return w;

        // Get Asset Bundle
        AssetBundle assetBundle = w.assetBundle;

        // Get the Asset from the Bundle by its Name
        AssetBundleRequest assetBundleRequest = assetBundle.LoadAssetAsync(_name, typeof(GameObject));
        yield return assetBundleRequest;

        // Convert into asset into a GameObject
        GameObject go = assetBundleRequest.asset as GameObject;

        // Instantiate the GameObject into the Scene
        loadedObject = Instantiate(go);
        loadedObject.name = go.name;

        // Finally Unload the asset bundle to free the memory and Dispose the web request
        assetBundle.Unload(false);
        w.Dispose();

    }
    private IEnumerator Co_LoadAssetUsingUnityWebRequest(string _name)
    {
        // Make the request
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(URL);
        yield return request.SendWebRequest();

        // Get the Asset Bundle
        AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(request);

        // Get the Asset
        AssetBundleRequest bundleRequest = assetBundle.LoadAssetAsync(_name, typeof(GameObject));
        yield return bundleRequest;

        // Get the Gameobject
        GameObject go = bundleRequest.asset as GameObject;

        // Instantiate the GameObject into the Scene
        loadedObject = Instantiate(go);
        loadedObject.name = go.name;

        // Finally Unload the asset bundle to free the memory and Dispose the web request
        assetBundle.Unload(false);
        request.Dispose();
    }

}
