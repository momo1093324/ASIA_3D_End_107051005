using UnityEngine;

[CreateAssetMenu(fileName = "NPC 資料", menuName = "a1070501005/NPC 資料")]
public class NPCDate : ScriptableObject
{
    [Header("第一段對話"), TextArea(1, 5)]
    public string dialougA;
    [Header("第二段對話"), TextArea(1, 5)]
    public string dialougB;
    [Header("第三段對話"), TextArea(1, 5)]
    public string dialougC;
    [Header("任務項目需求數量")]
    public int count;
    [Header("已經取得項目數量")]
    public int countCurrent;
}
