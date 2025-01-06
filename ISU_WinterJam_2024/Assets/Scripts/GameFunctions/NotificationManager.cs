using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public GameObject notifTextObj;
    private bool showing = false;
    public AudioClip notifSound;

    public void ShowNotif(string notifText)
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(notifSound);

        if (showing == true)
        {
            CancelInvoke();
            ResetNotif();
        }
        showing = true;
        notifTextObj.GetComponent<TextMeshProUGUI>().text = notifText;
        notifTextObj.SetActive(true);
        Invoke("ResetNotif", 6f);
    }

    private void ResetNotif()
    {
        showing = false;
        notifTextObj.SetActive(false);
    }
}
