using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class RoomManager : Singleton<RoomManager>
{
    public Volume volume;

    // �� ��ȯ �޼���
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // �� �ε� �� �ʱ�ȭ �۾�
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeOutVignette());
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void FailExit()
    {
        StartCoroutine(ExitFail());
    }

    public IEnumerator ExitFail()
    {
        // Volume ������Ʈ�� �����ɴϴ�.
        if (volume == null)
        {
            volume = GetComponent<Volume>();
            if (volume == null)
            {
                Debug.LogError("Volume ������Ʈ�� ã�� �� �����ϴ�.");
                yield break;
            }
        }

        // Vignette ȿ���� ã���ϴ�.
        if (volume.profile == null)
        {
            Debug.LogError("Volume Profile�� �������� �ʾҽ��ϴ�.");
            yield break;
        }

        if (volume.profile.TryGet<Vignette>(out Vignette vignette))
        {
            vignette.intensity.value = 0f; // �ʱ�ȭ
            float elapsedTime = 0f;
            float duration = 5f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                vignette.intensity.value = Mathf.Lerp(0f, 1f, elapsedTime / duration);
                yield return null;
            }

            vignette.intensity.value = 1f; // ���� �� ����
        }
        else
        {
            Debug.LogError("Vignette ȿ���� ã�� �� �����ϴ�.");
            yield break;
        }

        yield return new WaitForSecondsRealtime(1f);
        LoadScene("Result");
    }

    public IEnumerator FadeOutVignette()
    {
        // Volume ������Ʈ�� �����ɴϴ�.
        if (volume == null)
        {
            volume = GetComponent<Volume>();
            if (volume == null)
            {
                Debug.LogError("Volume ������Ʈ�� ã�� �� �����ϴ�.");
                yield break;
            }
        }

        // Vignette ȿ���� ã���ϴ�.
        if (volume.profile == null)
        {
            Debug.LogError("Volume Profile�� �������� �ʾҽ��ϴ�.");
            yield break;
        }

        if (volume.profile.TryGet<Vignette>(out Vignette vignette))
        {
            vignette.intensity.value = 1f; // �ʱ�ȭ
            float elapsedTime = 0f;
            float duration = 1f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                vignette.intensity.value = Mathf.Lerp(1f, 0f, elapsedTime / duration);
                yield return null;
            }

            vignette.intensity.value = 0f; // ���� �� ����
        }
        else
        {
            Debug.LogError("Vignette ȿ���� ã�� �� �����ϴ�.");
            yield break;
        }
    }
}
