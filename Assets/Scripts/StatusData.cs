using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Status", menuName = "Status Data/Status")]
public class StatusData : ScriptableObject
{
    public string statusName;
    public Sprite statusSprite;
}
