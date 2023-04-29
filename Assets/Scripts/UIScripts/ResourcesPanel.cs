using System.Collections.Generic;
using UnityEngine;

public class ResourcesPanel : MonoBehaviour
{
    [SerializeField] private Bank bank;
    [SerializeField] private TypeResource[] renderedResources;
    [SerializeField] private ResourceCell resourceCellPrefab;
    [SerializeField] private GameObject contentPanel;
    private List<ResourceCell> resourceCells = new();

    private void Start()
    {
        bank = Bank.Instance;
        InstantiateCells();
    }
    public void InstantiateCells()
    {
        foreach (var item in renderedResources)
        {
            var cell = Instantiate(resourceCellPrefab, contentPanel.transform);
            resourceCells.Add(cell);
            cell.InitCell(bank.GetResource(item));
        }
    }
}
