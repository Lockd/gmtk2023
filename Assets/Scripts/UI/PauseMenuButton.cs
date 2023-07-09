using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button thisButton;
    public GameObject select;
    public string sceneToLoad;

    private void Start()
    {
        thisButton.onClick.AddListener(LoadScene);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        select.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        select.SetActive(false);
    }

    void LoadScene()
    {
        if(!sceneToLoad.Equals("NONE"))
            SceneManager.LoadScene(sceneToLoad);
    }
}
