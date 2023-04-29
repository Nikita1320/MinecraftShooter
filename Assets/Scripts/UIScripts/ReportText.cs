using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportText : MonoBehaviour
{
    private static ReportText instance;

    [SerializeField] private Text repotrText;
    [SerializeField] private float timeActiveReportText;
    [SerializeField] private Color colorText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<ReportText>();
        }
        else
        {
            Destroy(instance.gameObject);
        }
    }
    public static void RenderReportMessage(string report)
    {
        instance.repotrText.text = report;
        instance.gameObject.SetActive(true);
        instance.StartCoroutine(instance.ReprtTextAnimationCororutine());
    }
    private IEnumerator ReprtTextAnimationCororutine()
    {
        var time = timeActiveReportText;
        repotrText.color = colorText;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            time -= Time.deltaTime;
            repotrText.color = new Color(repotrText.color.r, repotrText.color.g, repotrText.color.b, time / timeActiveReportText);
            if (time == 0)
            {
                gameObject.SetActive(false);
                break;
            }
        }
    }
}
