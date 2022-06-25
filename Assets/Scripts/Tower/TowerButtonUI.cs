using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerButtonUI : MonoBehaviour
{
    public TowerData tower;
    public TextMeshProUGUI towerNameText;
    public TextMeshProUGUI towerCostText;
    public Image towerIcon;

    private Button button;

    private void OnEnable()
    {
        GameManager.instance.onMoneyChange.AddListener(OnMoneyChange);
    }

    private void OnDisable()
    {
        GameManager.instance.onMoneyChange.RemoveListener(OnMoneyChange);

    }

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        towerNameText.text = tower.displayName;
        towerCostText.text = $"${tower.cost}";
        towerIcon.sprite = tower.icon;

        OnMoneyChange();
    }

    public void OnClick()
    {
        GameManager.instance.towerPlacement.SelectTowerToPlace(tower);
    }

    public void OnMoneyChange()
    {
        button.interactable = GameManager.instance.money >= tower.cost;
    }


}
