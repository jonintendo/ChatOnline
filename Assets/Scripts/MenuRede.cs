using UnityEngine;
using System.Collections;
using System;




public class MenuRede : MonoBehaviour
{

    public Manager gerente;



    public GameObject PlayerRede;
    public GameObject PlayerRedeInstantiated;
    // PlayerActionOnLine playeractiononline;


    public GameObject mario;
    public GameObject luigi;
    public GameObject wario;
    public GameObject mimico;
    public GameObject fogo;


   


    /*void AdjustPlayer()
    {


        playername = PlayerPrefs.GetString("playerName", gerente.connectplayerName);

        if (playername == null || playername == "")
        {
            playername = "RandomName" + UnityEngine.Random.Range(1, 999);
        }


    }*/


    

    void Awake()
    {



        gerente = gameObject.GetComponentInParent<Manager>();



    }

    















  


    





  



}
