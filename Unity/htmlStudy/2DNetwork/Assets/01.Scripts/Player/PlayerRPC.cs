using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRPC : MonoBehaviour
{
    public Sprite[] bodies;
    public Sprite[] turrets;

    public SpriteRenderer bodyRenderer;
    public SpriteRenderer turretRenderer;
    public bool isRemote = false;

    private PlayerInput input;
    private PlayerMove move;

    private TankCategory tankCategory;

    private WaitForSeconds ws = new WaitForSeconds(1 / 5);

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        move = GetComponent<PlayerMove>();
    }

    public void InitPlayer(Vector3 pos, TankCategory tank, bool remote = false)
    {
        bodyRenderer.sprite = bodies[(int)tank];
        turretRenderer.sprite = turrets[(int)tank];
        tankCategory = tank;

        isRemote = remote;
        if(isRemote)
        {
            input.enabled = false;
            move.enabled = false;
        }
        else
        {
            input.enabled = true;
            move.enabled = true;
            //StartCoroutine(SendData());
        }
    }

    IEnumerator SendData()
    {
        int socketId = 0;

        while(true)
        {
            yield return ws;
            TransformVo vo = new TransformVo(
                transform.position,
                transform.rotation.eulerAngles,
                turretRenderer.transform.rotation.eulerAngles,
                socketId,
                tankCategory
            );
            string playload = JsonUtility.ToJson(vo);

            DataVO dataVO = new DataVO();
            dataVO.type = "TRANSFORM";
            dataVO.payload = playload;
            SocketClient.SendDataToSocket(JsonUtility.ToJson(dataVO));
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
