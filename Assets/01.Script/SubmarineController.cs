using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    [SerializeField] private TextMesh speedText, underText, fuelText;
    [SerializeField] private GameObject chair;
    [SerializeField] private GameObject virtualSubmarine;
    [SerializeField] private GameObject underMarking;
    [SerializeField] private GameObject fuelBar;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] Vector3 moveDir;
    [SerializeField] private float maxFuel;
    float fuel;
    Vector3 moveDir_Lerp;
    bool isUsing;
    private void Start()
    {
        fuel = maxFuel;
    }
    void Update()
    {
        if (isUsing)
        {
            if(fuel >= 0)
            {
                moveDir = new Vector3(0, Input.GetKey(KeyCode.Space) ? 1 : Input.GetKey(KeyCode.LeftControl) ? -1 : 0, Input.GetAxisRaw("Vertical"));
                transform.Rotate(Vector3.up * Input.GetAxisRaw("Horizontal") * Time.deltaTime * 10);
                virtualSubmarine.transform.eulerAngles = new Vector3(virtualSubmarine.transform.eulerAngles.x, virtualSubmarine.transform.eulerAngles.y, -transform.eulerAngles.y);
                fuel -= (1 + rb.velocity.magnitude + Mathf.Sqrt(rb.velocity.magnitude)) * Time.deltaTime;
                fuelText.text = string.Format("-{0:0.0}", 1 + rb.velocity.magnitude + Mathf.Sqrt(rb.velocity.magnitude));
                fuelBar.transform.localScale = new Vector3(fuel / maxFuel, 1, 1);
            }
            else
            {
                fuelText.text = "NO FUEL!";
                moveDir = Vector3.zero;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                SitUp();
            }
        }
        else{
            moveDir = Vector3.zero;
        }
        moveDir = transform.TransformDirection(moveDir);
        moveDir.Normalize();    
        moveDir_Lerp = Vector3.Lerp(moveDir_Lerp, moveDir, Time.deltaTime * 0.5f);
        rb.velocity = moveDir_Lerp * speed;
        speedText.text = string.Format("{0:0.0}Note", rb.velocity.magnitude);
        underText.text = string.Format("{0:0.0}M", transform.position.y);
        underMarking.transform.localPosition = new Vector3(0, transform.position.y * 0.01f, -0.01f);
    }
    public void SitDown()
    {
        isUsing = true;
        GameObject obj = GameManager.Instance.Player;
        obj.transform.SetParent(chair.transform);
        obj.transform.position = chair.transform.position;
        obj.GetComponent<PlayerMove>().DisableMove(false);
    }
    public void SitUp()
    {
        isUsing = false;
        GameObject obj = GameManager.Instance.Player;
        obj.transform.SetParent(GameManager.Instance.SubMarine.transform);
        obj.GetComponent<PlayerMove>().IsCanMove = true;
        obj.GetComponent<PlayerMove>().DisableMove(true);
    }
}
