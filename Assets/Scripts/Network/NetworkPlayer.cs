using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;


public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public TextMeshProUGUI playerNickNameTM;
    public static NetworkPlayer Local {get; set;}

    public Transform playerModel;
    //�г����� host�� ���� ����
    //clinet�� �ڱ� �̸��� ������ �˷������ -> rpc
    //��� ����ȭ �Ǵ� ���� - ���� ������ ���ؼ��� ���� ����
    [Networked(OnChanged = nameof(OnNickNameChanged))]
    public NetworkString<_16> nickName {get;set;}

    [Networked] public int token {get; set;}
    public LocalCameraHandler localCameraHandler;
    void Start()
    {
        
    }
    public override void Spawned()
    {
        // player spawn
        if(Object.HasInputAuthority){
            Local = this;
            //local player layer set
            Utils.SetRenderLayerInChildren(playerModel, LayerMask.NameToLayer("LocalPlayerModel"));

            //main camera ��Ȱ��ȭ 
            if(Camera.main != null)
                Camera.main.gameObject.SetActive(false);

            //listener 1���� 
            AudioListener audioListener = GetComponentInChildren<AudioListener>(true);
            audioListener.enabled = false;

            localCameraHandler.localCamera.enabled = true;

            localCameraHandler.transform.parent = null;

            RPC_SentNickName(GameManager.instance.playerNickName);

            Debug.Log("Spawned local Player");
        }else{
            //Local camera�� �ƴϸ� ��Ȱ��ȭ
            localCameraHandler.localCamera.enabled = false;
            
            //listener 1���� 
            AudioListener audioListener = GetComponentInChildren<AudioListener>();
            audioListener.enabled = false;

            Debug.Log("Spanwed remote player");
        }
        //�̸� ����
        transform.name = $"P_{Object.Id}";
    }

    public void PlayerLeft(PlayerRef player){
        if(player == Object.InputAuthority){
            Runner.Despawn(Object);
        }
    }

    static void OnNickNameChanged(Changed<NetworkPlayer> changed){
        Debug.Log($"{Time.time} OnHPChanged value {changed.Behaviour.nickName}");
        changed.Behaviour.OnNickNameChanged();
    }

    public void OnNickNameChanged(){
        Debug.Log($"Nickname changed for player to {nickName} for player {gameObject.name}");
        playerNickNameTM.text = nickName.ToString();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SentNickName(string nickName, RpcInfo info = default){
        //RPC�� ���� �̸� ����
        Debug.Log($"[RPC] SetNickName {nickName}");
        this.nickName = nickName; //NetworkPlayer ��ũ��Ʈ�� nickName variable ����
    }

    void OnDestroy(){
        if(localCameraHandler!=null)
            Destroy(localCameraHandler.gameObject);

    }
}
