using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonController : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        Debug.Log("Start");
        // プレイヤー自身の名前を"Player"に設定する
        PhotonNetwork.NickName = "Player";
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        Debug.Log("In Masterserver");

        // ランダムなルームに参加する
        PhotonNetwork.JoinRandomRoom();

        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        //PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
        
        //PhotonNetwork.JoinLobby();    
    }

    // ランダムで参加できるルームが存在しないなら、新規でルームを作成する
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // ルームの参加人数を2人に設定する
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;

        PhotonNetwork.CreateRoom(null, roomOptions);
    }


    public override void OnJoinedLobby()
    {
        Debug.Log("In lobby");
        
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック 
    public override void OnJoinedRoom()
    {
        Debug.Log("In Gameserver");
        //何番目の入室者か？　１から順番通り採番される。退室者がいた場合の法則は未確認
        //今回使わないけど、今後何かで必要そうなので備忘録としてここに残してます。
        int num = PhotonNetwork.CurrentRoom.PlayerCount;

        //自身のアバターはランダムで決定
        //今回のアバター３体で、名をAvatar1,Avatar2,Awatar3とし、Resources フォルダに入れておく

        string avatarName="";
        avatarName = "Avatar";

        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        var position = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        GameObject avatar = PhotonNetwork.Instantiate(avatarName, position, Quaternion.identity);

        
        var camera = GameObject.Find("MainCamera");

        camera.transform.parent = avatar.transform;
        camera.transform.position = avatar.transform.position;

        var pos = avatar.transform.position;

        camera.transform.position = new Vector3(pos.x , pos.y+3.0f, pos.z-2.0f);
        

    }

    public override void OnDisconnected(DisconnectCause cause){
        Debug.Log($"だめぽ: {cause.ToString()}");
    }
}

