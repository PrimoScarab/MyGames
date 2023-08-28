using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPallet : MonoBehaviour
{
    public GameObject pallet1;
    public GameObject pallet2;
    public GameObject pallet3;
    public GameObject pallet4;
    public GameObject pallet5;
    public GameObject pallet6;

    public GameObject Box1;
    public GameObject Box2;
    public GameObject Box3;
    public GameObject Box4;
    public GameObject Box5;
    public GameObject Box6;

    public GameObject pallet1RespawnPos;
    public GameObject pallet2RespawnPos;
    public GameObject pallet3RespawnPos;
    public GameObject pallet4RespawnPos;
    public GameObject pallet5RespawnPos;
    public GameObject pallet6RespawnPos;

    public GameObject Box1RespawnPos;
    public GameObject Box2RespawnPos;
    public GameObject Box3RespawnPos;
    public GameObject Box4RespawnPos;
    public GameObject Box5RespawnPos;
    public GameObject Box6RespawnPos;


    public void resetPallet1()
    {
        pallet1.transform.position = pallet1RespawnPos.transform.position;
        Box1.transform.position = Box1RespawnPos.transform.position;
    }

    public void resetPallet2()
    {
        pallet2.transform.position = pallet2RespawnPos.transform.position;
        Box2.transform.position = Box2RespawnPos.transform.position;
    }

    public void resetPallet3()
    {
        pallet3.transform.position = pallet3RespawnPos.transform.position;
        Box3.transform.position = Box3RespawnPos.transform.position;
    }

    public void resetPallet4()
    {
        pallet4.transform.position = pallet4RespawnPos.transform.position;
        Box4.transform.position = Box4RespawnPos.transform.position;
    }

    public void resetPallet5()
    {
        pallet5.transform.position = pallet5RespawnPos.transform.position;
        Box5.transform.position = Box5RespawnPos.transform.position;
    }

    public void resetPallet6()
    {
        pallet6.transform.position = pallet6RespawnPos.transform.position;
        Box6.transform.position = Box6RespawnPos.transform.position;
    }
}
