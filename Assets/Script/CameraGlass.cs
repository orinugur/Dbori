using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class CameraGlass : MonoBehaviour
{
    public GameObject Camera;
    public Volume volume;

    void Start()
    {
        Camera.GetComponent<Animator>().enabled = false;
        Camera.AddComponent<Rigidbody>();
        Camera.GetComponent<Rigidbody>().AddForce( new Vector3(1f,1f,1f));

        // TryGet를 사용하여 LiftGammaGain 설정을 가져옵니다.

        StartCoroutine(startScene());

      
   
    }
    IEnumerator startScene()
    {
        yield return new WaitForSecondsRealtime(1f);
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
            float duration = 1f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                vignette.intensity.value = Mathf.Lerp(0f, 3f, elapsedTime / duration);
                yield return null;
            }

            vignette.intensity.value = 1f; // 최종 값 설정
        }
        else
        {
            Debug.LogError("Vignette 효과를 찾을 수 없습니다.");
            yield break;
        }
        yield return null;
        RoomManager.Instance.LoadScene("InGame");
    }

}
