using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeleType : MonoBehaviour
{
    // Start is called before the first frame update

private TextMeshPro textMeshPro;

    IEnumerator Start()
    {
        textMeshPro = gameObject.GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();

        int totalVisibleCharacters = textMeshPro.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);

            textMeshPro.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
                yield return new WaitForSeconds(1.0f);
            
            counter += 1;

            yield return new WaitForSeconds(0.05f);
        }        
    }
}
