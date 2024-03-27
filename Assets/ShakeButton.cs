using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
public class ShakeButton : MonoBehaviour
{
    public float duration = 0.5f;
    public float rotationAngle = 10f;

    public IEnumerator ShakeCoroutine()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DORotate(new Vector3(0, 0, rotationAngle), duration / 2).SetEase(Ease.InOutQuad));
            sequence.Append(transform.DORotate(new Vector3(0, 0, -rotationAngle), duration / 2).SetEase(Ease.InOutQuad));
            sequence.Append(transform.DORotate(new Vector3(0, 0, 0), duration / 2).SetEase(Ease.InOutQuad));
            yield return new WaitForSeconds(duration);
        }
    }
}

