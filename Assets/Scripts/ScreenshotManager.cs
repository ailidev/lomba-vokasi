using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ScreenshotManager : MonoBehaviour
{
    [SerializeField] string directoryName = "Screenshots";
    [SerializeField] string screenshotName = "sertifikat";
    [SerializeField] public GameObject[] m_HideGameObjects;

    public void SaveScreenshotToDocuments()
    {
        if (m_HideGameObjects.Length > 0)
        {
            foreach (GameObject obj in m_HideGameObjects)
            {
                obj.SetActive(false);            
            }
        }

        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string screenshotPath = Path.Combine(documentsPath, Application.productName, directoryName);
        string timeNow = DateTime.Now.ToString("dd-MMMM-yyyy HHmmss");

        DirectoryInfo screenshotDirectory = Directory.CreateDirectory(screenshotPath);
        ScreenCapture.CaptureScreenshot(Path.Combine(screenshotDirectory.FullName, screenshotName + timeNow + ".png"));

        StartCoroutine(Show(2f));
    }

    IEnumerator Show(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (m_HideGameObjects.Length > 0)
        {
            foreach (GameObject obj in m_HideGameObjects)
            {
                obj.SetActive(true);
            }
        }
    }
}
