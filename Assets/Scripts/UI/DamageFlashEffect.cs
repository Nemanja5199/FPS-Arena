using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageFlashEffect : MonoBehaviour
{
    [SerializeField] private Image damageImage;
    [SerializeField] private Image healImage;
    [SerializeField] private Image armorImage;
    [SerializeField] private float flashDuration = 0.3f;

    private Coroutine flashRoutine;

    public enum FlashType { Damage, Heal, Arrmor }

    void Awake()
    {
        if (damageImage != null)
            damageImage.color = new Color(1, 0, 0, 0);

        if (healImage != null)
            healImage.color = new Color(0, 1, 0, 0);
        if (healImage != null)
            healImage.color = new Color(135, 206, 235, 0);
    }

    public void Flash(FlashType type)
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        if (type == FlashType.Damage)
            flashRoutine = StartCoroutine(FlashRoutineDamage());
        else if (type == FlashType.Heal)
            flashRoutine = StartCoroutine(FlashRoutineHeal());
        else if (type == FlashType.Arrmor)
            flashRoutine = StartCoroutine(FlashRoutineArmor());
    }

    private IEnumerator FlashRoutineDamage()
    {
        float timer = 0f;

        while (timer < flashDuration / 2)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 0.5f, timer / (flashDuration / 2));
            damageImage.color = new Color(1, 0, 0, alpha);
            yield return null;
        }

        timer = 0f;

        while (timer < flashDuration / 2)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0.5f, 0, timer / (flashDuration / 2));
            damageImage.color = new Color(1, 0, 0, alpha);
            yield return null;
        }

        damageImage.color = new Color(1, 0, 0, 0);
    }

    private IEnumerator FlashRoutineHeal()
    {
        float timer = 0f;

        while (timer < flashDuration / 2)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 0.5f, timer / (flashDuration / 2));
            healImage.color = new Color(0, 1, 0, alpha);
            yield return null;
        }

        timer = 0f;

        while (timer < flashDuration / 2)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0.5f, 0, timer / (flashDuration / 2));
            healImage.color = new Color(0, 1, 0, alpha);
            yield return null;
        }

        healImage.color = new Color(0, 1, 0, 0);
    }


    private IEnumerator FlashRoutineArmor()
    {
        float timer = 0f;

        while (timer < flashDuration / 2)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 0.5f, timer / (flashDuration / 2));
            healImage.color = new Color(135, 206, 235, alpha);
            yield return null;
        }

        timer = 0f;

        while (timer < flashDuration / 2)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0.5f, 0, timer / (flashDuration / 2));
            healImage.color = new Color(135, 206, 235, alpha);
            yield return null;
        }

        healImage.color = new Color(135, 206, 235, 0);
    }
}
