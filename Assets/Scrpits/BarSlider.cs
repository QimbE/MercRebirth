using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarSlider : MonoBehaviour
{
    public Slider sliderHP;
    public Text textHP;

    public Slider sliderArmor;
    public Text textArmor;

    public Slider sliderEnergy;
    public Text textEnergy;

    public Stats playerStats;

    public void SetMaxValue(ref Slider slider, ref Text textValue, int startValue)
    {
        slider.maxValue = startValue;
        slider.value= startValue;
        textValue.text = $"{startValue}/{startValue}";
    }
    public void SetValue(ref Slider slider, ref Text textValue, int newValue)
    {
        slider.value = newValue;
        string[] temp = textValue.text.Split('/');
        textValue.text = $"{newValue}/{temp[1]}";
    }
    // Start is called before the first frame update
    void Start()
    {
        SetMaxValue(ref sliderHP, ref textHP, playerStats.maxHealth);
        SetMaxValue(ref sliderArmor, ref textArmor, playerStats.maxArmor);
        SetMaxValue(ref sliderEnergy, ref textEnergy, playerStats.maxEnergy);
    }

    // Update is called once per frame
    void Update()
    {
        SetValue(ref sliderHP, ref textHP, playerStats.health);
        SetValue(ref sliderArmor, ref textArmor, playerStats.armor);
        SetValue(ref sliderEnergy, ref textEnergy, playerStats.energy);
    }

    public void StopEnergyAnimation()
    {
        this.GetComponent<Animator>().SetBool("isEnergyCharged",false);
    }
    public void StopArmorAnimation()
    {
        this.GetComponent<Animator>().SetBool("isArmorCharged", false);
    }
    public void StopHpAnimation()
    {
        this.GetComponent<Animator>().SetBool("isHpCharged", false);
    }
}
