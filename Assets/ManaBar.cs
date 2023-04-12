//using UnityEngine;
//using UnityEngine.UI;

//public class HPBar : MonoBehaviour
//{
//    public int maxMana;
//    public int currentMana;
//    public NPCStats npcStats;
//    public RectTransform manaBarTransform;
//    public TargetStats targetStats;

//    public void Start()
//    {
//        manaBarTransform = GetComponent<RectTransform>();
//    }

//    public void Update()
//    {
//        if (targetStats.selectedNPC != null)
//        {
//            NPCStats selectedStats = targetStats.selectedNPC.GetComponentInChildren<NPCStats>();
//            currentMana = selectedStats.currentMana;
//            maxMana = selectedStats.maxMana;
//            Debug.Log("Current Mana: " + currentMana);
//            float manaPercent = (float)currentMana / maxMana;
//            manaBarTransform.localScale = new Vector3(manaPercent, 1, 1);

//            Vector2 size = manaBarTransform.sizeDelta;
//            size.x = manaPercent * 200; // adjust the width of the mana bar by changing the value 200
//            manaBarTransform.sizeDelta = size;
//        }
//    }
//}