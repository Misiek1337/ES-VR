using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class TooSlowCheck : MonoBehaviour
{
    public AudioClip failSound;
    public Session session;

    public void BeginCountdown()
    {
        Debug.Log("TooSlowCheck: BeginCountdown");
        StartCoroutine(Countdown());
    }

    public void StopCountdown()
    {
        Debug.Log("TooSlowCheck: StopCountdown");
        StopAllCoroutines();
    }

    IEnumerator Countdown()
    {
        //this resets after ~25 trials, dont know why
        float timeoutPeriod = session.CurrentTrial.settings.GetFloat("timeout_period");
        //var timeoutPeriod = 2.0f;

        Debug.Log($"TooSlowCheck: Countdown: settings: {string.Join("\n", session.CurrentTrial.settings.Keys)}");
        yield return new WaitForSeconds(timeoutPeriod);

        // if we got to this stage, that means we moved too slow
        Debug.Log($"TooSlowCheck: Countdown: TOO SLOW");
        session.CurrentTrial.result["outcome"] = "tooslow";
        session.EndCurrentTrial();

        // we will play a clip at position above origin, 100% volume
        AudioSource.PlayClipAtPoint(failSound, new Vector3(0, 1.3f, 0), 1.0f);
    }
}