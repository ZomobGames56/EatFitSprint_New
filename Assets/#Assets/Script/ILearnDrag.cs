using UnityEngine;
using UnityEngine.EventSystems;

public class ILearnDrag : MonoBehaviour,IDragHandler
{
    [SerializeField]
    GameObject timerBG_GO;
    public void OnDrag(PointerEventData eventData)
    {
        GameManager.instance.start = true;
        GameManager.instance.collectingPanel.SetActive(true);
        PlayerPrefs.SetString("FirstTimeEnter", "1");
        PlayerPrefs.SetString("LearnedPlay", "1");
        gameObject.SetActive(false);
        timerBG_GO.SetActive(true);
        n_Timer.StartTimerBool(true);

    }
}
