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

    private Queue<int> removeSocketQueue = new Queue<int>();

    private List<TransformVo> dataList;
    private bool needRefresh = false;

    public static int PlayerLayer;
    public static int EnemyLayer;

    public static Dictionary<TankCategory, TankDataVO> tankDataDic = new Dictionary<TankCategory, TankDataVO>();

    public static void InitGameData(string payload)
    {
        List<TankDataVO> list = JsonUtility.FromJson<TankDataListVO>(payload).tanks;
        foreach(TankDataVO t in list)
        {
            tankDataDic.Add(t.tank, t);
        }
    }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("�ټ��� ���ӸŴ����� ����ǰ� �ֽ��ϴ�.");
        }
        instance = this;
        PoolManager.CreatePool<PlayerRPC>(tankPrefab, transform, 8);

        PlayerLayer = LayerMask.NameToLayer("Player");
        EnemyLayer = LayerMask.NameToLayer("Enemy");
    }

    void Start()
    {

    }

    public static void DisconnectUser(int socketId)
    {
        lock(instance.lockObj)
        {
            instance.removeSocketQueue.Enqueue(socketId);
        }
    }

    public static void SetRefreshData(List<TransformVo> list)
    {
        lock (instance.lockObj)
        {
            instance.dataList = list;
            instance.needRefresh = true;
        }
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
                InfoUI ui = UIManager.SetInfoUI(rpc.transform, data.name);

                rpc.InitPlayer(data.position, data.tank, ui, false);
                followCam.Follow = rpc.transform;


                gameStart = true;
            }
        }
        if(needRefresh)
        {
            foreach(TransformVo tv in dataList)
            {
                if(tv.socketId != socketId)
                {
                    PlayerRPC p = null;
                    playerList.TryGetValue(tv.socketId, out p);
                    if(p==null)
                    {
                        MakeRemotePlayer(tv);
                    }
                    else
                    {
                        p.SetTransform(tv.position, tv.rotation, tv.turretRotation);
                    }
                }
            }
            needRefresh = false;
        }

        while(removeSocketQueue.Count>0)
        {
            int socket = removeSocketQueue.Dequeue();
            playerList[socket].SetDisable();
            playerList.Remove(socket);
        }
    }

    public PlayerRPC MakeRemotePlayer(TransformVo data)
    {
        PlayerRPC rpc = PoolManager.GetItem<PlayerRPC>();
        InfoUI ui = UIManager.SetInfoUI(rpc.transform, data.name);
        rpc.InitPlayer(data.position, data.tank, ui, true);
        playerList.Add(data.socketId, rpc);
        return rpc;
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
