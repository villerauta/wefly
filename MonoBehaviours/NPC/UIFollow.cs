using UnityEngine;
using PixelCrushers.DialogueSystem;

public class UIFollow : MonoBehaviour
{
    public Transform followThis;
    [SerializeField] private RectTransform container;
    [SerializeField] private RectTransform ui;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime = 0.2f;

    private Transform currentSpeaker = null;
    private bool needToSnap = true;

    public bool UseSmooth = false;

    private Vector2 m_currentVelocity;

    void Start()
    {
        var dialogueSystemEvents = DialogueManager.instance.GetComponent<DialogueSystemEvents>() ??
                    DialogueManager.instance.gameObject.AddComponent<DialogueSystemEvents>();

        dialogueSystemEvents.conversationEvents.onConversationLine.AddListener(HandleConversationLine);
    }

    void OnDestroy()
    {
        var dialogueSystemEvents = DialogueManager.instance.GetComponent<DialogueSystemEvents>() ??
                    DialogueManager.instance.gameObject.AddComponent<DialogueSystemEvents>();

        dialogueSystemEvents.conversationEvents.onConversationLine.RemoveListener(HandleConversationLine);
    }

    private void HandleConversationLine(Subtitle subtitle)
    {
        if (subtitle.speakerInfo.transform != currentSpeaker)
        {
            needToSnap = true;
            currentSpeaker = subtitle.speakerInfo.transform;
        }
    }

    private void LateUpdate()
    {
        var screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, followThis.position + offset);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(container, screenPoint, Camera.main, out localPoint);
        if (!UseSmooth || needToSnap)
        {
            ui.anchoredPosition = localPoint;
            needToSnap = false;
            m_currentVelocity = Vector2.zero;
        }
        else
        {
            ui.anchoredPosition = Vector2.SmoothDamp(ui.anchoredPosition, localPoint, ref m_currentVelocity, smoothTime);
        }

    }
}