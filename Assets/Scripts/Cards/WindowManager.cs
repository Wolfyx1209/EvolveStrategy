using EventBusSystem;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardWindow;
    public void OpenCardWindow()
    {
        _cardWindow.SetActive(true);
        Time.timeScale = 0;
        EventBus.RaiseEvent<IWindowOpenHandler>(it => it.WindowOnen());
    }
    public void CloseCardWindow()
    {
        _cardWindow.SetActive(false);
        Time.timeScale = 1;
        EventBus.RaiseEvent<IWindowOpenHandler>(it => it.WindowClosed());
    }
}
