using UnityEngine;
using UnityEngine.UI;

public class ResourceCell : MonoBehaviour
{
    [SerializeField] private Image resourceImage;
    [SerializeField] private Text resourceAmmountText;
    private Resource resource;
    public void InitCell(Resource resource)
    {
        this.resource = resource;
        RenderAmmountResource();
        resourceImage.sprite = resource.IconResource;

        resource.resourceChangedAmmount += RenderAmmountResource;
    }
    public void RenderAmmountResource()
    {
        resourceAmmountText.text = resource.Ammount.ToString();
    }
}
