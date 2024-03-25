using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    [SerializeField] private GameObject messageCloud;
    [SerializeField] private GameObject craftingPanel;
    [SerializeField] private GameObject overlay;
    [SerializeField] private GameObject possibleItemsPanel;
    
    private bool _canCraft;
    
    private void Update()
    {
        if (!_canCraft) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            craftingPanel.SetActive(!craftingPanel.activeSelf);
            overlay.SetActive(!overlay.activeSelf);

            if (!craftingPanel.activeSelf)
            {
                possibleItemsPanel.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.CompareTag("Player")) return;

        messageCloud.SetActive(true);
        _canCraft = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.transform.CompareTag("Player")) return;

        messageCloud.SetActive(false);
        _canCraft = false;
    }
}