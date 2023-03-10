using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardWindow;
    public void OpenCardWindow() 
    {
        _cardWindow.SetActive(true);
        Time.timeScale = 0;
    }
    public void CloseCardWindow()
    {
        _cardWindow.SetActive(false);
        Time.timeScale = 1;
    }
}
