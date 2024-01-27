using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponRange
    {
        melee,
        long_range
    }

    public SpriteRenderer weaponRenderer;
    
    public float rotationSpeed = 45f;
    public float rotationBackSpeed = 30f;
    private Quaternion defaultRotation;
    private bool isRotating = false;

    public float desiredRotationAngle = 90f;
    public float delayBetweenRotations = 1f;
    public Transform PlayerSprite;
    public GameObject AttackArea;
    public GameObject Crosshair;

    [Header("weapon Range")] public WeaponRange _WeaponRange;

    
    [Header("fx")]
    public GameObject AttackFx;
    public GameObject SecondAttackFx;
    public GameObject Bullet;

    // Serialized variable for customizable keycode
    [SerializeField]
    private KeyCode rotationKeyCode = KeyCode.Space;

    void Start()
    {
        defaultRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(rotationKeyCode) && !isRotating)
        {
            StartCoroutine(RotateObjectCoroutine(desiredRotationAngle, rotationSpeed, rotationBackSpeed, delayBetweenRotations));
            Crosshair.GetComponent<MMF_Player>().PlayFeedbacks();

            switch (_WeaponRange)
            {
                case WeaponRange.long_range: 
                    var bullet = Instantiate(Bullet, transform.position, Quaternion.identity);
                    Instantiate(AttackFx, transform.position, Quaternion.identity);
                    bullet.GetComponent<Bullet>().targetPosition = Crosshair.transform.position;
                    bullet.GetComponent<Bullet>().AttackFx = SecondAttackFx;
                    bullet.GetComponent<Bullet>().AttackArea = AttackArea;


                    break;
                
                case WeaponRange.melee:  
                    Instantiate(AttackArea, Crosshair.transform.position, Quaternion.identity);
                    Instantiate(AttackFx, Crosshair.transform.position, Quaternion.identity);
                    break;

            }
        }
    }

    IEnumerator RotateObjectCoroutine(float desiredRotation, float rotateSpeed, float rotateBackSpeed, float delayBetweenRotations)
    {
        isRotating = true;

        float rotationDirection = (PlayerSprite.localScale.x > 0) ? 1f : -1f;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(Vector3.forward * (rotationDirection * desiredRotation));
        float duration = Mathf.Abs(desiredRotation) / rotateSpeed;

        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }

        yield return new WaitForSeconds(delayBetweenRotations);

        t = 0;
        duration = Mathf.Abs(desiredRotation) / rotateBackSpeed;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.rotation = Quaternion.Lerp(targetRotation, defaultRotation, t);
            yield return null;
        }

        isRotating = false;
    }

    public void SetWeaponRenderer(Sprite weaponSprite,int weaponCooldown,int range,int weaponRotation)
    {
        weaponRenderer.sprite = weaponSprite;
        delayBetweenRotations = weaponCooldown;
        desiredRotationAngle = weaponRotation;
        Crosshair.GetComponent<Crosshair>().maxDistance = range;
    }
}