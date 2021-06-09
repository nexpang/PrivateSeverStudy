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

    private Vector3 targetPosition;
    private Vector3 targetRotation;
    private Vector3 targetTurretRotation;

    public float lerpSpeed = 4f;

    private InfoUI ui = null;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        move = GetComponent<PlayerMove>();
    }

    public void InitPlayer(Vector3 pos, TankCategory tank, InfoUI ui, bool remote = false)
    {
        bodyRenderer.sprite = bodies[(int)tank];
        turretRenderer.sprite = turrets[(int)tank];
        tankCategory = tank;
        this.ui = ui;

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
            StartCoroutine(SendData());
        }
    }

    IEnumerator SendData()
    {
        int socketId = GameManager.instance.socketId;

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

    public void SetTransform(Vector3 pos, Vector3 rotation, Vector3 turretRotation)
    {
        if(isRemote)
        {
            targetPosition = pos;
            targetRotation = rotation;
            targetTurretRotation = turretRotation;
        }
    }

    private void Update()
    {
        if(isRemote)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0f,0f, Mathf.LerpAngle(transform.eulerAngles.z,targetRotation.z,Time.deltaTime* lerpSpeed));
            turretRenderer.transform.eulerAngles = new Vector3(0f,0f, Mathf.LerpAngle(turretRenderer.transform.eulerAngles.z, targetTurretRotation.z,Time.deltaTime* lerpSpeed));
        }
    }

    public void SetDisable()
    {
        gameObject.SetActive(false);
        ui.gameObject.SetActive(false);

    }
}
