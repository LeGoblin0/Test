using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("폭탄 프리팹")]
    public GameObject boomPre;
    [Tooltip("폭탄 터질때 나오는 잔상")]
    public GameObject BoomDie;
    [Tooltip("폭탄 던지는 스피드")]
    public float BoomSpeed = 10;
    [Tooltip("폭탄 터지는 파워")]
    public float BoomPower = 10;

    GameObject Boom;//폭탄 생성하면 들어갈 변수
    Camera cam;
    Vector2 MousePosition;
    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Boom==null)
            {
                Boom = Instantiate(boomPre);
                Boom.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
                MousePosition = Input.mousePosition;
                MousePosition = cam.ScreenToWorldPoint(MousePosition);

                Boom.GetComponent<Rigidbody2D>().velocity = (new Vector3(MousePosition.x, MousePosition.y, transform.position.z) - transform.position).normalized * BoomSpeed;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = (transform.position - Boom.transform.position).normalized / (((transform.position - Boom.transform.position).sqrMagnitude < 1) ? 1 : (transform.position - Boom.transform.position).sqrMagnitude) * BoomPower;
                GameObject BoomDies = Instantiate(BoomDie);
                Destroy(BoomDies, 0.3f);
                BoomDies.transform.position = new Vector3(Boom.transform.position.x, Boom.transform.position.y, -1);
                Destroy(Boom.gameObject);
            }
        }
    }
}
