using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    [SerializeField] private Image targetImgae;
    void Update()
    {
        target.Rotate(-Vector3.forward, curve.Evaluate(Time.time % 1) * Time.deltaTime * speed);
        targetImgae.fillAmount = 0.17f * curve.Evaluate(Time.time % 1);
    }
}
