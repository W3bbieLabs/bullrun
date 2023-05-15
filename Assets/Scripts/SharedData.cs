using Mirror;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class SharedData : NetworkBehaviour
{

    VisualElement root;

    GameObject UIObject;

    Label playerCount;


    PlayerController player;

    MyNetworkManager _nm;


    [SyncVar(hook = nameof(onPlayerCountChange))]
    private int _playerCount = 0;

    public int countDownLength = 11;

    [SyncVar(hook = nameof(onCountDownChange))]
    private int countDown = 0;

    [SyncVar(hook = nameof(onRaceStateChange))]
    private bool isRacing = false;


    [SyncVar]
    public bool isCounting = false;


    public override void OnStartClient()
    {

        UIObject = GameObject.FindGameObjectWithTag("UIDoc");
        root = UIObject.GetComponent<UIDocument>().rootVisualElement;
        VisualElement container = root.Q<VisualElement>("container");
        playerCount = container.Q<Label>("players");
        //player = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>();
    }

    public void SetPlayerCount(int newValue)
    {
        _playerCount = newValue;
    }

    public int GetPlayerCount()
    {
        return _playerCount;
    }

    public void SetCountDown(int val)
    {
        countDown = val;
    }

    public int GetCountDown()
    {
        return countDown;
    }

    public void setRaceState(bool state)
    {
        isRacing = state;
    }

    public bool getIsRacing()
    {
        return isRacing;
    }

    public void SetIsCounting(bool state)
    {
        isCounting = state;
    }

    public bool GetIsCounting()
    {
        return isCounting;
    }

    [Command]
    public void startCounter()
    {
        StartCoroutine(updateCountdown(countDownLength));
    }

    public void onCountDownChange(int o, int n)
    {
        //Debug.Log($"o: {o} - n: {n}");
    }
    public void onPlayerCountChange(int oldCount, int newCount)
    {
        //Debug.Log($"Old: {oldCount} New: {newCount}");
    }

    public void onRaceStateChange(bool oldState, bool newState)
    {
        //Debug.Log($"old racing state {oldState} - new racing state {newState}");
    }


    public IEnumerator updateCountdown(int count)
    {
        //Debug.Log($"Starting new countdown p: {_playerCount} count: {countDown} isRacing: {isRacing}");
        for (int i = count; i >= 0; i--)
        {
            yield return new WaitForSeconds(1);
            countDown = i;
            SetCountDown(i);
        }
        setRaceState(true);
        SetIsCounting(false);
    }

    /********************** Server **************************************/

    /********************** Server **************************************/

}