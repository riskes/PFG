using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    // Start is called before the first frame update
    public Text hpText;
    public RectTransform hpBar;

    // Update is called once per frame
    void Update()
    {
        float completionRatio = (float)GameManager.instance.player.hitpoint / (float)GameManager.instance.player.maxHitpoint;
        hpBar.localScale = new Vector3(completionRatio, 1, 1);
        hpText.text = GameManager.instance.player.hitpoint.ToString();
    }
}
