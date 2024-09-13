using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserGrid : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nicknameText;
    [SerializeField] private TextMeshProUGUI recordText;

    public void SetData(string nickname, string record)
    {
        this.nicknameText.text = nickname;
        this.recordText.text = record;
    }
}
