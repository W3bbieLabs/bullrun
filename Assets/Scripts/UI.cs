using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    PlayerController player;

    /*
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Button buttonStart = root.Q<Button>("start");
        Button buttonGo = root.Q<Button>("go");
        buttonStart.clicked += () => resetPlayer();
        buttonGo.clicked += () => { takeOff(); };
    }

    void takeOff()
    {
        player = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>();
        player.go();
    }

    void resetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>();
        player.resetPlayer();
    }
    */

}
