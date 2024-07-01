using System.Threading.Tasks;
using UnityEngine;

public class SunRIse : MonoBehaviour
{
    public Material skyboxMaterial;
    public float startThickness = 0.1f;
    public float endThickness = 1.3f;
    public float duration = 300f; // 5�� = 300��

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
            // ���� �ð��� ������Ʈ
            elapsedTime += Time.deltaTime;

            // Lerp�� ����Ͽ� 0.1���� 1.3 ������ ���� ���
            float currentThickness = Mathf.Lerp(startThickness, endThickness, elapsedTime / duration);

            // Skybox ���׸����� Atmosphere Thickness ���� ����
            skyboxMaterial.SetFloat("_AtmosphereThickness", currentThickness);

            // ���� �����ӱ��� ���
            await Task.Yield();
        }

        // ���������� �� ������ ���� (��Ȯ���� ����)
        skyboxMaterial.SetFloat("_AtmosphereThickness", endThickness);
    }
}
