using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _workMenu;
    [SerializeField] private GameObject _shopMenu;
    [SerializeField] private GameObject _mailMenu;

    private void HideAll()
    {
        _workMenu.SetActive(false);
        _shopMenu.SetActive(false);
        _mailMenu.SetActive(false);
    }

    public void ShowWork()
    {
        HideAll();
        _workMenu.SetActive(true);
    }
    
    public void ShowShop()
    {
        HideAll();
        _shopMenu.SetActive(true);
    }

    public void ShowMail()
    {
        HideAll();
        _mailMenu.SetActive(true);
    }
}
