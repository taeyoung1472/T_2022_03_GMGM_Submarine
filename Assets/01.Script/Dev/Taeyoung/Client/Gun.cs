using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunData gunData;
    [SerializeField] private GameObject soundObject;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private Transform gun;
    [SerializeField] private MeshRenderer flashEffect;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private LayerMask cullingLayer;
    private Text magText;
    Queue<GameObject> queue = new Queue<GameObject>();
    int curAmmo;
    bool isReload = false, isCanShoot = true, isZoom;
    PlayerMove playerMove;
    public void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Shoot());
    }
    public void Start()
    {
        magText = ToolManager.instance.MagText;
        curAmmo = gunData.mag;
        playerMove = FindObjectOfType<PlayerMove>();
        for (int i = 0; i < 30; i++)
        {
            GameObject obj = Instantiate(soundObject, transform);
            queue.Enqueue(obj);
            obj.SetActive(false);
        }
    }
    public void OnDisable()
    {
        StopAllCoroutines();
    }
    public void Update()
    {
        if (!PlayerStat.IsCanControll)
            return;
        if (Input.GetKeyDown(KeyCode.R) && !isReload)
        {
            PlayAudio(reloadClip);
            StartCoroutine(Reload());
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isZoom = !isZoom;
            if (isZoom)
            {
                isCanShoot = false;
                Sequence seq = DOTween.Sequence();
                seq.Append(gun.DOLocalMove(gunData.zoomPos, gunData.zoomTime));
                seq.AppendCallback(() => isCanShoot = true);
            }
            else
            {
                isCanShoot = false;
                Sequence seq = DOTween.Sequence();
                seq.Append(gun.DOLocalMove(gunData.defaultPos, gunData.zoomTime));
                seq.AppendCallback(() => isCanShoot = true);
            }
        }
    }
    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitUntil(() => isCanShoot);
            switch (gunData.gunMode)
            {
                case GunMode.Semi:
                case GunMode.Brust:
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) && curAmmo >= 0);
                    break;
                case GunMode.Auto:
                    yield return new WaitUntil(() => Input.GetKey(KeyCode.Mouse0) && curAmmo >= 0);
                    break;
            }
            if (!isCanShoot || !PlayerStat.IsCanControll) continue;
            DisplayAmmo();
            Recoil();
            PlayAudio(shootClip);
            StartCoroutine(Flash());
            ShootRay();
            curAmmo--;
            yield return new WaitForSeconds(gunData.shootDelay);
        }
    }

    private void ShootRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(firePos.position, -firePos.forward, out hit, 100f, cullingLayer))
        {
            Quaternion rot = Quaternion.LookRotation(-hit.normal);
            GameObject obj = Instantiate(hitParticle, hit.point, rot);
            Destroy(obj, 2f);
            if (hit.transform.CompareTag("HitBox"))
            {
                hit.transform.GetComponent<HitBox>().Damaged(gunData.damage);
            }
        }
    }

    private void PlayAudio(AudioClip shootClip)
    {
        GameObject obj;
        if (queue.Count > 0)
        {
            obj = queue.Dequeue();
        }
        else
        {
            obj = Instantiate(soundObject, transform);
        }
        obj.SetActive(true);
        AudioSource source = obj.GetComponent<AudioSource>();
        source.clip = shootClip;
        source.Play();
        StartCoroutine(DQ(obj, source.clip.length > 1 ? 1 : source.clip.length));
    }
    IEnumerator DQ(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
        queue.Enqueue(obj);
    }

    IEnumerator Reload()
    {
        isCanShoot = false;
        isReload = true;
        yield return new WaitForSeconds(gunData.reloadDelay);
        curAmmo = gunData.mag;
        DisplayAmmo();
        isCanShoot = true;
        isReload = false;
    }
    IEnumerator Flash()
    {
        Vector2 offset = new Vector2(Random.Range(0, 2) * 0.5f, Random.Range(0, 2) * 0.5f);
        flashEffect.material.mainTextureOffset = offset;
        float angle = Random.Range(0, 360);
        float size = Random.Range(0.35f, 0.5f);
        flashEffect.transform.localEulerAngles = new Vector3(0, 90, angle);
        flashEffect.transform.localScale = new Vector3(size, size, size);
        flashEffect.enabled = true;
        yield return new WaitForSeconds(0.05f);
        flashEffect.enabled = false;
    }
    void DisplayAmmo()
    {
        magText.text = $"{curAmmo}";
    }
    void Recoil()
    {
        Vector2 recoil = new Vector2(gunData.recoil.x * Random.Range(-0.25f, 1f), gunData.recoil.y * Random.Range(0.75f, 1.25f));
        playerMove.Recoil(-recoil, gunData.shootDelay);
    }
}
