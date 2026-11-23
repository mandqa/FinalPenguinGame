using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBarManager : MonoBehaviour
{
    public TMP_Text IceText;
    public Image iceBar;

    public TMP_Text SugarText;
    public Image SugarBar;
    
    public TMP_Text fishyText;
    public Image fishyBar;

    float ice, maxIce = 100;

    float sugar, maxSugar = 100;
    
    float fishiness, maxFishy = 100;
    
    float lerpSpeed;

    private void Start()
    {
        IceText.text = "Ice Content: " + ice + "%";
        SugarText.text = "Sugar Content: " + sugar + "%";
        fishyText.text = "Fishiness: " + fishiness+"%";
        
        if (ice > maxIce) ice = maxIce;

        if (sugar > maxSugar) sugar = maxSugar;
        
        if(fishiness > maxFishy) fishiness = maxFishy;

        lerpSpeed = 3f * Time.deltaTime;
        
        BarFiller();
        ColorChanger();
    }

    void BarFiller()
    {
        iceBar.fillAmount = Mathf.Lerp(iceBar.fillAmount, ice/maxIce, lerpSpeed);
        SugarBar.fillAmount = Mathf.Lerp(SugarBar.fillAmount, sugar/maxSugar, lerpSpeed);
        fishyBar.fillAmount = Mathf.Lerp(fishyBar.fillAmount, fishiness/maxFishy, lerpSpeed);
    }

    void ColorChanger()
    {
        Color iceColor = Color.Lerp(Color.dodgerBlue, Color.softBlue, (ice/maxIce));
        Color sugarColor = Color.Lerp(Color.powderBlue, Color.antiqueWhite, (sugar/maxSugar));
        Color fishyColor = Color.Lerp(Color.lightSeaGreen, Color.slateBlue, (fishiness/maxFishy));  
        
        fishyBar.color = fishyColor;
        iceBar.color = iceColor;
        SugarBar.color = sugarColor;
    }

    public void IceDamage(float damagePoints)
    {
        if (ice > 0) ice -= damagePoints;
        
    }

    public void SugarDamage(float damagePoints)
    {
        if (sugar > 0) sugar-=damagePoints;
    }

    public void FishyDamage(float damagePoints)
    {
        if(fishiness > 0) fishiness -= damagePoints;
    }

    public void HealIce(float healingPoints)
    {
        if(ice<maxIce) ice += healingPoints;
    }

    public void HealSugar(float healingPoints)
    {
        if (sugar < maxSugar) sugar += healingPoints;
    }

    public void HealFishy(float healingPoints)
    {
        if (fishiness > maxFishy) fishiness += healingPoints;
    }
}
