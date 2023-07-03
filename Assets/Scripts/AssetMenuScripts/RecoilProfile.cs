using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Weapon Recoil", order = 2)]
public class RecoilProfile : ScriptableObject
{
    //Speed Settings
    public float positionalRecoilSpeed;
    public float rotationalRecoilSpeed;

    //Return Speeds
    public float positionalReturnSpeed;
    public float rotationalReturnSpeed;

    //Recoil Settings
    public Vector3 RecoilRotation;
    public Vector3 RecoilKickBack;
}
