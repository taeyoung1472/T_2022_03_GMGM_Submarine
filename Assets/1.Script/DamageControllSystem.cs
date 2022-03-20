using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControllSystem : MonoBehaviour
{
    [SerializeField] private float[] partHp;
    [SerializeField] private SpriteRenderer[] spriteRenderer;
    [SerializeField] private Color[] colorPerHp;
    [SerializeField] private Light alarmLight;
    void Start()
    {
        int i = 0;
        foreach (SpriteRenderer item in spriteRenderer)
        {
            i++;
            item.color = colorPerHp[Random.Range(0,colorPerHp.Length)];
        }
        //StartCoroutine(Alarm());
    }
    void Update()
    {
    }
    IEnumerator Alarm()
    {
        while (true)
        {
            alarmLight.color = Color.white;
            yield return new WaitForSeconds(0.2f);
            alarmLight.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            /*int i = 0;
            foreach (Light item in alarmLight)
            {
                item.enabled = true;
                item.color = spriteRenderer[i].color;
                i++;
            }
            yield return new WaitForSeconds(0.2f);
            foreach (Light item in alarmLight)
            {
                item.enabled = false;
            }*/
        }
    }
}
public enum Part
{
    LeftUp,
    LeftDown,
    Middle,
    RightUp,
    RightDown
}
public enum PartState
{

}