using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Connection
{
    public class RoomsMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_InputField createRoom;
        [SerializeField] private TMP_InputField joinRoom;
    
        public void CreateRoom()
        {
            if (createRoom.text.Length <= 3) return;

            var roomOptions = new RoomOptions
            {
                MaxPlayers = 2
            };
            PhotonNetwork.CreateRoom(createRoom.text.ToLower(), roomOptions);
        }

        public void JoinRoom()
        {
            if (joinRoom.text.Length <= 3) return;

            PhotonNetwork.JoinRoom(joinRoom.text.ToLower());
        }

        public override void OnJoinedRoom()
        {
            var roomName= "Room: " + PhotonNetwork.CurrentRoom.Name;
            Debug.Log(roomName);
            PhotonNetwork.LoadLevel("Game");
        }
    }
}