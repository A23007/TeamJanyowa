using UnityEngine;

public class MoveOnPlayerTouch : MonoBehaviour
{
    private bool hasMoved = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasMoved && other.CompareTag("Player"))
        {
            hasMoved = true;
            StartCoroutine(MoveSequence());
        }
    }

    private System.Collections.IEnumerator MoveSequence()
    {
        Vector3 startPos = transform.position;
        Vector3 upPos = startPos + new Vector3(0, 5f, 0);
        Vector3 downPos = startPos;

        // è„è∏Ç…1ïb
        yield return StartCoroutine(MoveOverTime(transform, upPos, 1f));

        // 5ïbë“ã@
        yield return new WaitForSeconds(5f);

        // â∫ç~Ç…1ïb
        yield return StartCoroutine(MoveOverTime(transform, downPos, 1f));
    }

    private System.Collections.IEnumerator MoveOverTime(Transform obj, Vector3 targetPos, float duration)
    {
        Vector3 initialPos = obj.position;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            obj.position = Vector3.Lerp(initialPos, targetPos, t);
            yield return null;
        }

        obj.position = targetPos;
    }
}
