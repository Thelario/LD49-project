using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CanvasSwitcher : MonoBehaviour
{
    public CanvasType desiredCanvasType;

    CanvasManager canvasManager;
    Button menuButton;

    private void Start()
    {
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(OnButtonClicked);
        canvasManager = CanvasManager.Instance;
    }

    void OnButtonClicked()
    {
        switch (desiredCanvasType)
        {
            case CanvasType.InGameMenu:
                Time.timeScale = 1f;
                canvasManager.SwitchCanvas(desiredCanvasType);
                break;
            case CanvasType.PauseGameMenu:
                Time.timeScale = 0f;
                canvasManager.SwitchCanvas(desiredCanvasType);
                break;
            default:
                canvasManager.SwitchCanvas(desiredCanvasType);
                break;
        }
    }
}
