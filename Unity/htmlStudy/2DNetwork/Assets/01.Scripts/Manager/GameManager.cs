using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("탱크 프리팹")]
    public GameObject tankPrefab;
    public bool gameStart = false;
    private TransformVo data = null;

    [Header("시네머신 카메라")]
    public CinemachineVirtualCamera followCam;

    [HideInInspector]
    public int socketId;

    public object lockObj = new object(); // 데이터 락킹을 위한 오브젝트

    private Dictionary<int, PlayerRPC> playerList = new Dictionary<int, PlayerRPC>();

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("다수의 게임매니져가 실행되고 있습니다.");
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
            if(!gameStart && data != null) //로그인된것
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
