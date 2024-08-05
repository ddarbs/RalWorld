using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static Enums;

public class IntroTutorial : MonoBehaviour
{
    [SerializeField] private GameObject MaterialInventoryBlocker;
    [SerializeField] private GameObject SkillInventoryBlocker;
    [SerializeField] private GameObject GatherButtonsBlocker;
    [SerializeField] private GameObject ActionBarBlocker;
    
    [SerializeField] private GameObject StructureButtonsBlocker;
    [SerializeField] private GameObject ToolsButtonsBlocker;
    [SerializeField] private GameObject ForgeButtonsBlocker;

    [SerializeField] private SkillInventory SkillInventory;
    
    
    public delegate void IntroEnd();
    public static event IntroEnd OnIntroEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Intro());
    }

    private IEnumerator Intro()
    {
        yield return new WaitForSeconds(1f);
        
        NotificationHandler.QueueNotification("hello I am Ral Mondl");
        NotificationHandler.QueueNotification("let me show you around");
        yield return new WaitForSeconds(5f);
        
        NotificationHandler.QueueNotification("these are your materials");
        yield return new WaitForSeconds(1.5f);
        MaterialInventoryBlocker.SetActive(false);
        yield return new WaitForSeconds(2f);
        
        NotificationHandler.QueueNotification("these are your skills");
        yield return new WaitForSeconds(1.5f);
        SkillInventoryBlocker.SetActive(false);
        yield return new WaitForSeconds(1.75f);
        
        NotificationHandler.QueueNotification("this is how you get materials");
        yield return new WaitForSeconds(1.5f);
        GatherButtonsBlocker.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        
        NotificationHandler.QueueNotification("now take my gloves and try gathering");
        yield return new WaitForSeconds(3f);
        SkillInventory.IncreaseLevel(SkillList.Gathering);
        ActionBarBlocker.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        
        NotificationHandler.QueueNotification("and feel free to rearrange things");
        yield return new WaitForSeconds(3f);
        StructureButtonsBlocker.SetActive(false);
        ToolsButtonsBlocker.SetActive(false);
        ForgeButtonsBlocker.SetActive(false);
        
        OnIntroEnd?.Invoke();
    }
}
