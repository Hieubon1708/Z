using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isStart;
    public Camera cam;

    private void Awake()
    {
        instance = this;
        DOTween.SetTweensCapacity(200, 1000);
        Resize();
    }

    void Resize()
    {
        float defaultSize = cam.orthographicSize;
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = 10.8f / 19.2f;
        if (screenRatio < targetRatio)
        {
            float changeSize = targetRatio / screenRatio;
            cam.orthographicSize = defaultSize * changeSize;
        }
    }
}
