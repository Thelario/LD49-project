using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    private Button buttonRef;

    private void Awake()
    {
        buttonRef = GetComponent<Button>();

        buttonRef.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        SoundManager.Instance.PlaySound(SoundType.ButtonClick, 1f);
    }

    private void OnMouseEnter()
    {
        SoundManager.Instance.PlaySound(SoundType.MouseOverButton, 1f);
    }
}
