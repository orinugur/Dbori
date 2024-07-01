using System.Threading.Tasks;
using UnityEngine;

public class SunRIse : MonoBehaviour
{
    public Material skyboxMaterial;
    public float startThickness = 0.1f;
    public float endThickness = 1.3f;
    public float duration = 300f; // 5분 = 300초

    private void Awake()
    {
        skyboxMaterial.SetFloat("_AtmosphereThickness", 0.1f);
    }
    private void Start()
    {

        ChangeAtmosphereThickness();
    }

    private async void ChangeAtmosphereThickness()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // 현재 시간을 업데이트
            elapsedTime += Time.deltaTime;

            // Lerp를 사용하여 0.1에서 1.3 사이의 값을 계산
            float currentThickness = Mathf.Lerp(startThickness, endThickness, elapsedTime / duration);

            // Skybox 마테리얼의 Atmosphere Thickness 값을 설정
            skyboxMaterial.SetFloat("_AtmosphereThickness", currentThickness);

            // 다음 프레임까지 대기
            await Task.Yield();
        }

        // 마지막으로 끝 값으로 설정 (정확성을 위해)
        skyboxMaterial.SetFloat("_AtmosphereThickness", endThickness);
    }
}
