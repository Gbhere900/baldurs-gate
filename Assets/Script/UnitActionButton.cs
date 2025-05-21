using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Button button;

    public void Initialize(BaseUnitAction baseUnitAction)
    {
        button.onClick.AddListener(() =>
        {
            UnitActionManager.Instance().SetSelectedUniAction(baseUnitAction);
        });
        ChangeText(baseUnitAction.GetUnitAcionName());
    }

    private  void ChangeText(string tsxt)
    {
        textMeshProUGUI.text = tsxt.ToUpper();
    }
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
