using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SERVER : MonoBehaviour
{

  
    public ArrayList playerList = new ArrayList();

   
    GUIOnLine guiOnLine;
    Manager gerente;

    float timer;

    class PlayerChat
    {
        public String name = "";
        public NetworkPlayer networkPlayer;
    }

    void OnServerInitialized()
    {

    
        guiOnLine.HitEnter("server ativo", "");



    }


    void OnPlayerDisconnected(NetworkPlayer player)
    {
        guiOnLine.HitEnter("Player disconnected from: " + player.ipAddress + ":" + player.port, "");

       


        string hh = RemovePlayer(player);

        if (hh != "")
        {
            guiOnLine.HitEnter(hh + " left the chat", "");
        }

        Network.DestroyPlayerObjects(player);
        Network.RemoveRPCs(player);



    }




    void OnPlayerConnected(NetworkPlayer player)
    {

        guiOnLine.HitEnter("Player connected from: " + player.ipAddress + ":" + player.port, "");


    }


    void OnDisconnectedFromServer()
    {

        playerList = new ArrayList();

        Debug.Log("server desconectou");
    }


    void Awake()
    {
        guiOnLine = GetComponent<GUIOnLine>();
        gerente = gameObject.GetComponentInParent<Manager>();

    }

    void Start()
    {
        //currentMenu = ChatWindow;
        //showMenu = false;

    }











    [RPC]
    bool TellServerOurName(String name, NetworkMessageInfo info)
    {
        if (Network.isServer)
        {
            if (AddNewPlayer(name, info.sender))
            {
                guiOnLine.HitEnter(name + " joined the chat", "");

            }

        }
        return true;

    }


    public bool AddNewPlayer(String name, NetworkPlayer player)
    {
        Debug.Log(name);
        if (CheckNomeRepedido(name))
        {
            //Network.CloseConnection (info.sender, true);
            guiOnLine.HitEnter("Nome Repetido Jogador mais recente será desconectado", name);

            Network.CloseConnection(player, true);
            return false;
        }
        else
        {
            PlayerChat newPlayer = new PlayerChat();
            newPlayer.name = name;

            newPlayer.networkPlayer = player;

            playerList.Add(newPlayer);

           
            return true;
        }

    }


    public bool CheckNomeRepedido(string name)
    {
        foreach (PlayerChat playerPlaying in playerList)
        {
            if (playerPlaying.name == name)
            {

                //Network.CloseConnection(info.sender,true);
                //networkView.RPC ("NameInUse", RPCMode.Others,info.sender);
                //NameInUse();
                //nameinuse = true;
                //break;

                return true;
            }
        }

        return false;
    }



    public string RemovePlayer(NetworkPlayer player)
    {

        //Remove playerstatus from the server list

        var playerRemovedfromGame = GetPlayer(player);

        if (!object.ReferenceEquals(playerRemovedfromGame, null))
        {
            string playerremovefromgamename = playerRemovedfromGame.name;
            //Remove player from the server list
            playerList.Remove(playerRemovedfromGame);
            //Debug.Log (playerRemovedfromGame.playerName);
            return playerremovefromgamename;
        }
        else
            return "";

    }


    PlayerChat GetPlayer(NetworkPlayer networkPlayer)
    {

        foreach (PlayerChat entry in playerList)
        {
            Debug.Log(entry.name);
            if (entry.networkPlayer == networkPlayer)
                return entry;

        }

        return null;

    }









}


