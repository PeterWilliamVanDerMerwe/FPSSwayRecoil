using UnityEngine;

public class WeaponHolder : MonoBehaviour, IInteractable
{
    //drop weapons
    //pick up weapons

    [SerializeField] private GameObject[] heldGuns;
    [SerializeField] private GameObject currentGun;

    private void FixedUpdate()
    {
        SwapWeapon();
        Interact();
    }

    private void SwapWeapon()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            currentGun = heldGuns[0];
            DisableWeapons();
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            currentGun = heldGuns[1];
            DisableWeapons();
        }
    }

    private void DisableWeapons()
    {
        foreach (GameObject gun in heldGuns)
        {
            if (gun != currentGun)
                gun.SetActive(false);
            else
                gun.SetActive(true);
        }
    }

    private void WeaponPickUp()
    {

    }

    public void Interact()
    {
        if (RayCastInteractor.Instance.CenterOfScreenRaycast().Equals("WeaponPickUp"))
        {
            print("woohoo");
        }
    }
}
