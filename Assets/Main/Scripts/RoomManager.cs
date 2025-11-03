using System.Collections;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }

    [Header("Debug Info")]
    [SerializeField] private string currentZoneName;

    [Header("Zones")] 
    [SerializeField] private GameObject ROOM;
    [SerializeField] private GameObject PC;
    //[SerializeField] private GameObject Time;
    //[SerializeField] private GameObject Clicker;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void EnterZone(RoomZone zone)
    {
        currentZoneName = zone.zoneName;
        Debug.Log($"🧭 Player entered zone: {zone.zoneName}");

        switch (currentZoneName)
        {
            case "PC":
                EnterPC();
                break;
        }
    }

    private IEnumerator WaitFade()
    {
        yield return null;
        yield return new WaitUntil(() => G.ScreenFader.IsFading == false);
        
        G.ScreenFader.FadeIn();
        
        ROOM.SetActive(false);
        
        PC.SetActive(true);
        //Time.SetActive(true);
        //Clicker.SetActive(true);
    }

    private void EnterPC()
    {
        G.ScreenFader.FadeOut();

        StartCoroutine(WaitFade());
    }

    public void BackToRoom()
    {
        PC.SetActive(false);
        //Time.SetActive(false);
        //Clicker.SetActive(false);
        
        ROOM.SetActive(true);
    }
}