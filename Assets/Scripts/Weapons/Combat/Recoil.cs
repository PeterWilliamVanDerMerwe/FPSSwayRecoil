using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    [Header("Reference Points")]
    public Transform recoilPosition;
    public Transform rotationPoint;
    [Space(10)]

    [Header("Speed Settings")]
    public float positionalRecoilSpeed;
    public float rotationalRecoilSpeed;
    [Space(10)]

    public float positionalReturnSpeed;
    public float rotationalReturnSpeed;
    [Space(10)]

    [Header("Recoil Settings:")]
    public Vector3 RecoilRotation = new Vector3(0f, 0f, 0f);
    public Vector3 RecoilKickBack = new Vector3(0f, 0f, 0f);

    Vector3 rotationalRecoil;
    Vector3 positionalRecoil;
    Vector3 Rot;
    [Header("State:")]
    public bool aiming;

    private void FixedUpdate()
    {
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, rotationalReturnSpeed * Time.deltaTime);
        positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, positionalReturnSpeed * Time.deltaTime);

        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil, positionalRecoilSpeed * Time.deltaTime);
        Rot = Vector3.Slerp(Rot, rotationalRecoil, rotationalRecoilSpeed * Time.deltaTime);
        rotationPoint.localRotation = Quaternion.Euler(Rot);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Fire();
        }
    }

    public void Fire()
    {
            rotationalRecoil += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
            rotationalRecoil += new Vector3(Random.Range(-RecoilKickBack.x, RecoilKickBack.x), Random.Range(-RecoilKickBack.y, RecoilKickBack.y), RecoilKickBack.z);
    }
}

