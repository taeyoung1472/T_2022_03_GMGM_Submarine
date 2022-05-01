using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairTool : MonoBehaviour
{
    [SerializeField] private float repairSpeed;
    [SerializeField] private RepairUI repairUI;
    [SerializeField] private Transform firePos;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float range = 5f;
    [SerializeField] private ParticleSystem particle;
    RaycastHit hit;
    [SerializeField] SubmarinePart part;
    float startTime, startHp;
    public void OnEnable()
    {
        particle.Stop();
    }
    public void Update()
    {
        CheckRay();
        Repair();
        DisplayUI();
    }
    void DisplayUI()
    {
        if(part != null)
        {
            if (!repairUI.gameObject.activeSelf)
            {
                repairUI.gameObject.SetActive(true);
            }
            repairUI.Display(part.PartData, part.HP, repairSpeed);
        }
        else
        {
            if (repairUI.gameObject.activeSelf)
            {
                repairUI.gameObject.SetActive(false);
            }
        }
    }
    void CheckRay()
    {
        if (Physics.Raycast(firePos.position, firePos.forward, out hit, range, layer))
        {
            if (part == null)
                part = hit.transform.GetComponent<SubmarinePart>();
        }
        else
        {
            if (part != null)
                part = null;
        }
    }
    void Repair()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            particle.Play();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            particle.Stop();
        }
        if(part != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                startTime = Time.time;
                startHp = part.HP;
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (part.HP < part.PartData.maxPartHp)
                {
                    part.HP = startHp + (Time.time - startTime) * repairSpeed;
                }
                else
                {
                    part.HP = part.PartData.maxPartHp;
                }
            }
        }
        /*if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            particle.Play();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            print("사용중");
            if (Physics.Raycast(firePos.position, firePos.forward, out hit, range, layer))
            {
                if (part == null)
                    part = hit.transform.GetComponent<SubmarinePart>();
            }
            else
            {
                if (part != null)
                    part = null;
            }
        }
        else
        {
            particle.Stop();
            if (part != null)
                part = null;
            print("사용중이 아님");
        }*/
    }
}
