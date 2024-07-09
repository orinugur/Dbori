using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class RoomManager : Singleton<RoomManager>
{
    public Volume volume;

    // 씬 전환 메서드
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // 씬 로드 후 초기화 작업
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
        // Volume 컴포넌트를 가져옵니다.
        if (volume == null)
        {
            volume = GetComponent<Volume>();
            if (volume == null)
            {
                Debug.LogError("Volume 컴포넌트를 찾을 수 없습니다.");
                yield break;
            }
        }

        // Vignette 효과를 찾습니다.
        if (volume.profile == null)
        {
            Debug.LogError("Volume Profile이 설정되지 않았습니다.");
            yield break;
        }

        if (volume.profile.TryGet<Vignette>(out Vignette vignette))
        {
            vignette.intensity.value = 0f; // 초기화
            float elapsedTime = 0f;
            float duration = 5f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                vignette.intensity.value = Mathf.Lerp(0f, 1f, elapsedTime / duration);
                yield return null;
            }

            vignette.intensity.value = 1f; // 최종 값 설정
        }
        else
        {
            Debug.LogError("Vignette 효과를 찾을 수 없습니다.");
            yield break;
        }

        yield return new WaitForSecondsRealtime(1f);
        LoadScene("Result");
    }

    public IEnumerator FadeOutVignette()
    {
        // Volume 컴포넌트를 가져옵니다.
        if (volume == null)
        {
            volume = GetComponent<Volume>();
            if (volume == null)
            {
                Debug.LogError("Volume 컴포넌트를 찾을 수 없습니다.");
                yield break;
            }
        }

        // Vignette 효과를 찾습니다.
        if (volume.profile == null)
        {
            Debug.LogError("Volume Profile이 설정되지 않았습니다.");
            yield break;
        }

        if (volume.profile.TryGet<Vignette>(out Vignette vignette))
        {
            vignette.intensity.value = 1f; // 초기화
            float elapsedTime = 0f;
            float duration = 1f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                vignette.intensity.value = Mathf.Lerp(1f, 0f, elapsedTime / duration);
                yield return null;
            }

            vignette.intensity.value = 0f; // 최종 값 설정
        }
        else
        {
            Debug.LogError("Vignette 효과를 찾을 수 없습니다.");
            yield break;
        }
    }
}
