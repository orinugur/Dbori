using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CamBreak : MonoBehaviour
{
    public Volume volume2;
    private LiftGammaGain liftGammaGain;
    private Vector4 defaultLift;

    void Start()
    {
        // TryGet�� ����Ͽ� LiftGammaGain ������ �����ɴϴ�.
        if (volume2.profile.TryGet<LiftGammaGain>(out liftGammaGain))
        {
            // �⺻ Lift ���� �����մϴ�.
            defaultLift = liftGammaGain.lift.value;
        }
        else
        {
            Debug.LogError("LiftGammaGain ������ ã�� �� �����ϴ�!");
        }

        // �׽�Ʈ�� ���� ���� �� �ڷ�ƾ�� ȣ���մϴ�.
        StartCoroutine(IncreaseLiftRoutine(new Vector4(2.0f, 2.0f, 2.0f, 1.0f), 0.2f, 0.2f));
    }

    public void IncreaseLiftTemporarily(Vector4 targetLift, float duration, float holdTime)
    {
        if (liftGammaGain != null)
        {
            StartCoroutine(IncreaseLiftRoutine(targetLift, duration, holdTime));
        }
    }

    private IEnumerator IncreaseLiftRoutine(Vector4 targetLift, float duration, float holdTime)
    {
        Vector4 initialLift = liftGammaGain.lift.value;
        float timer = 0f;

        // ������ ����
        while (timer < duration)
        {
            timer += Time.deltaTime;
            liftGammaGain.lift.value = Vector4.Lerp(initialLift, targetLift, timer / duration);
            yield return null;
        }

        // ���� �ð� ����
        yield return new WaitForSeconds(holdTime);

        timer = 0f;
        // ������ ����
        while (timer < duration)
        {
            timer += Time.deltaTime;
            liftGammaGain.lift.value = Vector4.Lerp(targetLift, initialLift, timer / duration);
            yield return null;
        }

        // Lift ���� ������� ����
        liftGammaGain.lift.value = defaultLift;
    }
}