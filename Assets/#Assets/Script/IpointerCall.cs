using UnityEngine;
using UnityEngine.EventSystems;

public class IpointerCall : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.instance.fightingPanel.SetActive(true);
        InGameTimer.instance.canRunTimer = true;
        CameraFollow.instance.initialCameraRotation = new Vector3(3f, 0, 0);
        CameraFollow.instance.offsetFromPlayer = new Vector3(0, 10, -25);
        PlayerPrefs.SetString("KnowFighting", "1");
        gameObject.SetActive(false);
        
    }
}
