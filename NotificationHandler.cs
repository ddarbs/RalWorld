using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class NotificationHandler : MonoBehaviour
{
    private const float DELAY_CHARACTER = 0.075f;
    private const float DELAY_AFTERTYPED = 0.5f;
    private const float DELAY_AFTERCLEARED = 0.5f;
    
    [SerializeField] private TextMeshProUGUI NotificationText;
    
    private static NotificationHandler Instance;
    private static Queue<string> Notifications = new Queue<string>();

    private static bool Available;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Available = true;
        }
        else
        {
            Debug.LogError("There should not be more than one NotifcationHandler", this);
        }
    }

    public static void QueueNotification(string _notification)
    {
        Notifications.Enqueue(_notification);

        if (Available)
        {
            Instance.StartCoroutine(ShowNotificationThread());
        }
    }

    private static IEnumerator ShowNotificationThread()
    {
        Available = false;
        
        while (Notifications.Count > 0)
        {
            string _FullNotification = Notifications.Dequeue();

            foreach (char _c in _FullNotification)
            {
                Instance.NotificationText.text += _c;
                DialogueAudio.RequestDialogueSound(_c);
                yield return new WaitForSeconds(DELAY_CHARACTER);
            }

            yield return new WaitForSeconds(DELAY_AFTERTYPED);
            
            Instance.NotificationText.text = "";
            
            yield return new WaitForSeconds(DELAY_AFTERCLEARED);
        }
        
        Available = true;
    }
}
