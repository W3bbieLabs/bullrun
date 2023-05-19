using UnityEngine;
using Thirdweb;
using UnityEngine.UI;
using Mirror;

public class UI : MonoBehaviour
{
    PlayerController player;

    [SerializeField] GameObject connectButton;

    [SerializeField] GameObject mainMenu;

    [SerializeField] GameObject controlsCanvas;

    [SerializeField] GameObject countCanvas;

    [SerializeField] NetworkManager networkManager;

    [SerializeField] string networkAddress;

    private void Start()
    {
        //hideCanvas(countCanvas);
        connectButton.GetComponent<Button>().onClick.AddListener(() => OnClick());
        //mainMenu.active = false;
    }

    public void OnClick()
    {
        ConnectClient();
        Debug.Log("Clicked");
        hideCanvas(mainMenu);
        showCanvas(controlsCanvas);
        //showCanvas(countCanvas);
    }

    public void hideCanvas(GameObject canvas)
    {
        Renderer renderer = canvas.GetComponent<Renderer>();
        canvas.SetActive(false);
        if (renderer != null)
        {
            renderer.enabled = false;
        }
    }

    public void showCanvas(GameObject canvas)
    {
        Renderer renderer = canvas.GetComponent<Renderer>();
        canvas.SetActive(true);
        if (renderer != null)
        {
            renderer.enabled = true;
        }
    }

    public void ConnectClient()
    {
        networkManager.networkAddress = networkAddress;
        networkManager.StartClient();
        //mainMusic.Stop();
    }
}
