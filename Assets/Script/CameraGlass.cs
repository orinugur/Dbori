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

        // TryGet�� ����Ͽ� LiftGammaGain ������ �����ɴϴ�.

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
            float duration = 1f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                vignette.intensity.value = Mathf.Lerp(0f, 3f, elapsedTime / duration);
                yield return null;
            }

            vignette.intensity.value = 1f; // ���� �� ����
        }
        else
        {
            Debug.LogError("Vignette ȿ���� ã�� �� �����ϴ�.");
            yield break;
        }
        yield return null;
        RoomManager.Instance.LoadScene("InGame");
    }

}
