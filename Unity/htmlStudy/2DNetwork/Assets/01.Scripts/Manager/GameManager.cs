using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("��ũ ������")]
    public GameObject tankPrefab;
    public bool gameStart = false;
    private TransformVo data = null;

    [Header("�ó׸ӽ� ī�޶�")]
    public CinemachineVirtualCamera followCam;

    [HideInInspector]
    public int socketId;

    public object lockObj = new object(); // ������ ��ŷ�� ���� ������Ʈ

    private Dictionary<int, PlayerRPC> playerList = new Dictionary<int, PlayerRPC>();

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("�ټ��� ���ӸŴ����� ����ǰ� �ֽ��ϴ�.");
        }
        instance = this;
        PoolManager.CreatePool<PlayerRPC>(tankPrefab, transform, 8);
    }

    void Start()
    {
        
    }

    void Update()
    {
        lock(lockObj)
        {
            if(!gameStart && data != null) //�α��εȰ�
            {
                UIManager.CloseLoginPanel();

                PlayerRPC rpc = PoolManager.GetItem<PlayerRPC>();
                socketId = data.socketId;
                rpc.InitPlayer(data.position, data.tank, false);
                followCam.Follow = rpc.transform;

                //UIManager.SetInfoUI()

                gameStart = true;
            }
        }
    }

    public static void GameStart(TransformVo data)
    {
        //Instantiate(instance.tankPrefab, data.position, Quaternion.identity);
        //instance.socketId = data.socketId;
        lock(instance.lockObj)
        {
            instance.data = data;
        }
    }
}
