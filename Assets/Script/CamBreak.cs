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
        // TryGet를 사용하여 LiftGammaGain 설정을 가져옵니다.
        if (volume2.profile.TryGet<LiftGammaGain>(out liftGammaGain))
        {
            // 기본 Lift 값을 저장합니다.
            defaultLift = liftGammaGain.lift.value;
        }
        else
        {
            Debug.LogError("LiftGammaGain 설정을 찾을 수 없습니다!");
        }

        // 테스트를 위해 시작 시 코루틴을 호출합니다.
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

        // 서서히 증가
        while (timer < duration)
        {
            timer += Time.deltaTime;
            liftGammaGain.lift.value = Vector4.Lerp(initialLift, targetLift, timer / duration);
            yield return null;
        }

        // 일정 시간 유지
        yield return new WaitForSeconds(holdTime);

        timer = 0f;
        // 서서히 감소
        while (timer < duration)
        {
            timer += Time.deltaTime;
            liftGammaGain.lift.value = Vector4.Lerp(targetLift, initialLift, timer / duration);
            yield return null;
        }

        // Lift 값을 원래대로 설정
        liftGammaGain.lift.value = defaultLift;
    }
}