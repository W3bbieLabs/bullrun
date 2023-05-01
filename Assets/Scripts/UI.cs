using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    PlayerController player;
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Button buttonStart = root.Q<Button>("start");
        buttonStart.clicked += () => resetPlayer();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void resetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>();
        player.resetPlayer();
    }
}
