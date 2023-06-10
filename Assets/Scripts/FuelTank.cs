using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FuelTank : BlockMovementScript
{
    public GameObject popupPrefab;
    public float fuelAmount = 20f;
    public bool active = false;
    public float cooldown = 1f;
    private float t;
    public Renderer glowRenderer;
    public Color glowColor;
    public Material glowMaterial;
    //public int lives = 3;
    // Start is called before the first frame update
    void Start()
    {
        t = cooldown;
        //if(glowRenderer!= null)
        //{
        //    return;
        //}

        //glowMaterial = glowRenderer.gameObject.GetComponent<Material>();
        glowRenderer.material = new Material(glowMaterial);
        glowMaterial = glowRenderer.material;
        if(glowColor == new Color())
        glowColor = glowMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        //Debug.Log("material = " + glowRenderer.gameObject.GetComponent<Material>());
        if (t >= cooldown) active = true;
        glowMaterial.color = glowColor * (Mathf.Clamp(t,0f,cooldown) / cooldown);
        //Debug.Log("Color = " + glowMaterial.color + " tc = " + ((t % cooldown) / cooldown) + " harusnya = " + glowColor);
    }
    public float getFuel(float decaySpeed)
    {
        if(!active) return 0f;
        active= false;
        t = 0f;
        PopupText pt = (PopupText)PopupSpawner.spawnText(popupPrefab, transform);
        pt.setText(Mathf.FloorToInt(fuelAmount/decaySpeed) + "s");
        return fuelAmount;

    }
}
