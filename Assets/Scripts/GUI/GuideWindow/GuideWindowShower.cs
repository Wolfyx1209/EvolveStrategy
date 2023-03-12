using EventBusSystem;
using System.Collections.Generic;
using UnityEngine;

public class GuideWindowShower : MonoBehaviour
{
    [SerializeField] private GameObject _windowPrefab;
    [SerializeField] private GameObject _darckerPrefab;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private bool _isWindowShowes;

    private Queue<GuideWindowData> _guideWindows = new();

    private void Start()
    {
        AddDataToQueue("TestGuideWindow");
    }
    private void StartShowWindows()
    {
        _isWindowShowes = true;
        Time.timeScale = 0;
        _darckerPrefab.SetActive(true);
        EventBusSystem.EventBus.RaiseEvent<IWindowOpenHandler>(it => it.WindowOnen());
        ShowWindow(_guideWindows.Dequeue());
    }

    private void StopShowWindows()
    {
        _isWindowShowes = false;
        Time.timeScale = 1;
        _darckerPrefab.SetActive(false);
        EventBusSystem.EventBus.RaiseEvent<IWindowOpenHandler>(it => it.WindowClosed());
    }

    private void ShowWindow(GuideWindowData name)
    {
        _isWindowShowes = true;
        GuideWindow window = Instantiate(_windowPrefab).GetComponent<GuideWindow>();
        window.transform.SetParent(_canvas.transform, false);
        window.transform.SetAsLastSibling();
        window.FillWindow(name);
        window.OnPlayerPressNext += ShowNextWindow;
    }

    private void ShowNextWindow(GuideWindow window)
    {
        Destroy(window.gameObject);
        if (_guideWindows.Count > 0)
        {
            ShowWindow(_guideWindows.Dequeue());
        }
        else
        {
            StopShowWindows();
        }
    }

    private void AddDataToQueue(string name)
    {
        _guideWindows.Enqueue(Resources.Load<GuideWindowData>("GuideWindows/" + name));
        if (!_isWindowShowes)
        {
            StartShowWindows();
        }
    }
}
