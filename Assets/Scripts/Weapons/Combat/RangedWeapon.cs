using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.DebugUI.Table;

public class RangedWeapon : MonoBehaviour
{
    public WeaponStatistics WeaponStatitics;
    public RecoilProfile RecoilProfile;

    //Reference Points for Recoil
    [SerializeField] private Transform recoilPosition;
    [SerializeField] private Transform rotationPoint;

    [SerializeField] private Transform weponBarrel;
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private TMP_Text Clip;

    private bool canFire = true;
    private float timeBetweenLastShot;

    private Vector3 directionWithoutSpread;
    private Vector3 directionWithSpread;

    private int currentMagazine;
    private bool needReload;

    private Vector3 rotationalRecoil;
    private Vector3 positionalRecoil;
    private Vector3 rotation;

    private void Start()
    {
        currentMagazine = WeaponStatitics.ClipSize;
        recoilPosition = GameObject.Find("WeaponPosition").transform;
        rotationPoint = GameObject.Find("RotationPoint").transform;
    }

    void FixedUpdate()
    {
        FireRateController();
        RecoilController();
        MagazineController(WeaponStatitics.ClipSize);
    }

    private void FireRateController()
    {
        timeBetweenLastShot += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0) && canFire && !needReload)
            Fire();

        if (timeBetweenLastShot > WeaponStatitics.FireRate)
            canFire = true;
    }

    public void Fire()
    {
        canFire = false;
        timeBetweenLastShot = 0;

        CenterOfScreen();

        GameObject projectile = Instantiate(WeaponStatitics.PrimaryProjectile, weponBarrel.position, Quaternion.identity);
        projectile.transform.forward = directionWithSpread.normalized;//Rotate bullet to shoot direction
        projectile.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * WeaponStatitics.ProjectileSpeed, ForceMode.Impulse);//Add forces to bullet
        AddRecoil();

        currentMagazine = MagazineController(WeaponStatitics.ClipSize, currentMagazine);
        MuzzleFlash();
    }

    private void CenterOfScreen()
    {
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); //Just a point far away from the player

        //Calculate direction from attackPoint to targetPoint
        directionWithoutSpread = targetPoint - weponBarrel.position;

        //Calculate spread
        float x = Random.Range(-WeaponStatitics.BulletSpread, WeaponStatitics.BulletSpread);
        float y = Random.Range(-WeaponStatitics.BulletSpread, WeaponStatitics.BulletSpread);

        //Calculate new direction with spread
        directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction
    }

    private void MuzzleFlash()
    {
        GameObject muzzleFlash = Instantiate(WeaponStatitics.MuzzleFlash, weponBarrel.position, Quaternion.identity);//MuzzleFlash
        muzzleFlash.transform.LookAt(directionWithoutSpread);
        Destroy(muzzleFlash, 1.5f);
    }

    private int MagazineController(int maxClipSize, int currentClipSize)
    {
        currentClipSize--;

        if (currentClipSize <= 0)
            needReload = true;
        return currentClipSize;
    }

    private void MagazineController(int maxClipSize)
    {
        Clip.text = currentMagazine.ToString();

        if (Input.GetKey(KeyCode.R))
        {
            currentMagazine = maxClipSize;
            needReload = false;
        }
    }

    private void RecoilController()
    {
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, 
            Vector3.zero, 
            RecoilProfile.rotationalReturnSpeed * Time.deltaTime);

        positionalRecoil = Vector3.Lerp(positionalRecoil, 
            Vector3.zero, 
            RecoilProfile.positionalReturnSpeed * Time.deltaTime);

        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, 
            positionalRecoil, 
            RecoilProfile.positionalRecoilSpeed * Time.deltaTime);

        rotation = Vector3.Slerp(rotation, 
            rotationalRecoil, 
            RecoilProfile.rotationalRecoilSpeed * Time.deltaTime);

        rotationPoint.localRotation = Quaternion.Euler(rotation);
    }

    private void AddRecoil()
    {
        rotationalRecoil += new Vector3(-RecoilProfile.RecoilRotation.x, 
            Random.Range(-RecoilProfile.RecoilRotation.y, 
            RecoilProfile.RecoilRotation.y), 
            Random.Range(-RecoilProfile.RecoilRotation.z, 
            RecoilProfile.RecoilRotation.z));

        rotationalRecoil += new Vector3(Random.Range(-RecoilProfile.RecoilKickBack.x, 
            RecoilProfile.RecoilKickBack.x), 
            Random.Range(-RecoilProfile.RecoilKickBack.y, 
            RecoilProfile.RecoilKickBack.y),
            RecoilProfile.RecoilKickBack.z);
    }

}
