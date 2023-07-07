using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TEMP_UITest : MonoBehaviour
{
    [SerializeField] Transform text;

    public void OnClick() {
        Vector3 v = new (Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0);
        text.DOMove(v, 0.5f).SetEase(Ease.OutQuad);
    }
}
