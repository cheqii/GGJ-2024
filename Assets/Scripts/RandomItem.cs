using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomItem : MonoBehaviour
{
    [Header("Weapon Data")]
    [SerializeField] private List<WeaponData> weapons;
    [SerializeField] private List<Sprite> imageList;
    [SerializeField] private TMP_Text weaponNameText;
    [SerializeField] private Image selectedWeaponUI;

    [Header("Random Speed")]
    [SerializeField] private float initialSpeed = 1000.0f;
    [SerializeField] private float acceleration = 10.0f;
    [SerializeField] private float easeInOutDuration = 1.0f; // Duration for ease-in-out curve

    [Header("Round")]
    [SerializeField] private int totalRounds = 20;
    [SerializeField] private int currentRound = 0;

    private RectTransform rectTransform;
    private bool isMoving = false; // Flag to check if the movement should start

    public bool IsMoving
    {
        get => isMoving;
        set => isMoving = value;
    }

    private WeaponData selectedWeapon;

    [SerializeField] public WeaponManager _weaponManager;
    
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        weaponNameText.text = "";
    }

    private void Update()
    {
        // Press F to start random!
        if (Input.GetKeyDown(KeyCode.F) && currentRound < totalRounds && !isMoving)
        {
            StartMoving();
        }

        if (isMoving && currentRound < totalRounds)
        {
            MoveImage();
        }
    }

    // Use this method to start random!
    public void StartMoving()
    {
        isMoving = true;
        StartCoroutine(MoveRoutine());
        weaponNameText.text = "";
    }

    private void MoveImage()
    {
        if (currentRound == totalRounds - 1) // Check if it's the last lap
        {
            float t = Mathf.SmoothStep(0, 1, Mathf.PingPong(Time.time / easeInOutDuration, 1));
            float easedSpeed = Mathf.Lerp(initialSpeed, 0, t);
            float step = easedSpeed * Time.deltaTime;
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(rectTransform.anchoredPosition.x, 0), step);

            if (rectTransform.anchoredPosition.y == 0)
            {
                currentRound++;
                isMoving = false; // Stop movement after the last lap

                // Do something when the randomization is done here!
                Image imageComponent = GetComponent<Image>();
                string spriteName = imageComponent ? imageComponent.sprite?.name : "No Sprite";

                Debug.Log("Source image name: " + spriteName);

                // Check if spriteName matches any WeaponData.weaponIcon.name
                selectedWeapon = weapons.Find(weapon => weapon.weaponIcon.name == spriteName);
                if (selectedWeapon != null)
                {
                    // Get data from WeaponData
                    Debug.Log("Weapon name: " + selectedWeapon.weaponName);
                    weaponNameText.text = selectedWeapon.weaponName;
                    selectedWeaponUI.sprite = selectedWeapon.weaponIcon;
                    
                    _weaponManager.SetWeapon(selectedWeapon);
                }

                currentRound = 0; // Reset currentRound for the next iteration
            }
        }
        else
        {
            float t;

            if (currentRound <= 5) // First 5 laps: Random slot machine-like speed changes
            {
                t = Mathf.SmoothStep(0, 1, Random.Range(0.0f, 1.0f));
            }
            else if (currentRound > 4 && currentRound < totalRounds - 4) // Laps 6-15: Constant speed
            {
                t = 1.0f;
            }
            else
            {
                t = Mathf.SmoothStep(0, 1, Random.Range(0.0f, 1.0f));
            }

            float easedSpeed = Mathf.Lerp(initialSpeed / 2, initialSpeed, t);
            float step = easedSpeed * Time.deltaTime;
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(rectTransform.anchoredPosition.x, -120), step);

            if (rectTransform.anchoredPosition.y == -120)
            {
                ChangeImage();
            }
        }

        // if (currentRound == 0)
        // {
        //     print("already random false = random again");
        //     alreadyRandom = false;
        // }
    }

    private void ChangeImage()
    {
        int randomIndex = Random.Range(0, imageList.Count);
        GetComponent<Image>().sprite = imageList[randomIndex];

        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 120);
        currentRound++;
    }

    IEnumerator MoveRoutine()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 1.0f)); // Random delay before starting the next round
    }
}