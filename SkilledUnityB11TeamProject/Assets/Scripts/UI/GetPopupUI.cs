using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetPopupUI : MonoBehaviour
{
    public TextMeshProUGUI textName;

    public RectTransform rectTransform;

    private readonly Vector3 START_POSITION = new Vector3(-700, -440, 0);
    private readonly WaitForSeconds delay = new WaitForSeconds(0.01f);

    //private void Start()
    //{
    //    rectTransform = GetComponent<RectTransform>();
    //}

    public void Initial(ItemData item)
    {
        rectTransform.localPosition = START_POSITION;
        textName.text = item.itemName;
        StartCoroutine(CoGoUp());
    }

    private IEnumerator CoGoUp()
    {
        while (rectTransform.localPosition.y < -80f)
        {
            Vector3 pos = rectTransform.localPosition;
            pos.y += 10f;
            rectTransform.localPosition = pos;

            yield return delay;
        }

        gameObject.SetActive(false);
    }
}
