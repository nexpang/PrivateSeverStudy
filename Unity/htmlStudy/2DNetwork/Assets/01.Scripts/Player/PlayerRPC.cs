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
    private PlayerFire playerFire;
    private PlayerHealth health;

    private TankCategory tankCategory;

    private WaitForSeconds ws = new WaitForSeconds(1f / 10f);

    private Vector3 targetPosition;
    private Vector3 targetRotation;
    private Vector3 targetTurretRotation;

    public float lerpSpeed = 4f;

    private InfoUI ui = null;

    public bool isDead = false;

    private BoxCollider2D boxCollider;
    private SpriteRenderer[] renderers;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        move = GetComponent<PlayerMove>();
        playerFire = GetComponent<PlayerFire>();
        health = GetComponent<PlayerHealth>();
        boxCollider = GetComponent<BoxCollider2D>();
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void InitPlayer(Vector3 pos, TankCategory tank, InfoUI ui, bool remote = false)
    {
        bodyRenderer.sprite = bodies[(int)tank];
        turretRenderer.sprite = turrets[(int)tank];
        tankCategory = tank;
        this.ui = ui;

        gameObject.transform.position = pos;

        isRemote = remote;
        if(isRemote)
        {
            input.enabled = false;
            move.enabled = false;
            gameObject.layer = GameManager.EnemyLayer;
        }
        else
        {
            input.enabled = true;
            move.enabled = true;
            gameObject.layer = GameManager.PlayerLayer;
            move.SetMoveScript( GameManager.tankDataDic[tank] );
            StartCoroutine(SendData());
        }
        health.SetHealthScript(GameManager.tankDataDic[tank], remote, ui);
        playerFire.SetFireScript(GameManager.tankDataDic[tank], remote);
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

    public void SetHp(int hp)
    {
        health.currentHP = hp;
        health.UpdateUI();
    }

    public void Die()
    {
        isDead = true;
        MassiveExplosion mExp = EffectManager.GetMassiveExplosion();
        mExp.ResetPos(transform.position);

        //gameObject.SetActive(false);
        //ui.gameObject.SetActive(false);

        SetScript(false);
    }

    public void SetScript(bool on)
    {
        input.enabled = on && !isRemote;
        boxCollider.enabled = on;
        foreach(SpriteRenderer s in renderers)
        {
            s.enabled = on;
        }
        ui.SetVisible(on);
    }

    public void Respawn()
    {
        SetScript(true);
        health.currentHP = health.maxHP;
        health.UpdateUI();
        isDead = false;

        if(!isRemote)
        {
            DataVO vo = new DataVO();
            vo.type = "RESPAWN";
            vo.payload = GameManager.instance.socketId + "";

            SocketClient.SendDataToSocket(JsonUtility.ToJson(vo));
        }
    }
}
