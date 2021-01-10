using UnityEngine;
using UnityEngine.UI;
using System.Collections; //引用 系統.集合API(包含協同程序)

public class NPC : MonoBehaviour
{
    [Header("NPC資料")]
    public NPCDate date;
    [Header("對話框")]
    public GameObject dialog;
    [Header("對話內容")]
    public Text textContent;
    [Header("對話者名稱")]
    public Text textName;
    [Header("間隔對話")]
    public float interval = 0.2f;

    //玩家是否進入感應區
    public bool playerInArea;
    //對話
    public enum NPCState
    {
        FirstDialog, Missioning, Finish
    }
    public NPCState state = NPCState.FirstDialog;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "女孩")
        {
            playerInArea = true;
            StartCoroutine(Dialog());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "女孩")
        {
            playerInArea = false;
            stopDialog();
        }

    }
    /// <summary>
    /// 停止對話
    /// </summary>
    private void stopDialog()
    {
        dialog.SetActive(false);//關閉所有對話框
        StopAllCoroutines();//停止所有協同
    }
    /// <summary>
    /// 開始對話
    /// </summary>
    private IEnumerator Dialog()
    {
       //for迴圈-重複處理相同程式
        //for (int i = 0; i < 6; i++)
        //{
        //    print("我是迴圈:" + i);
        //}

        dialog.SetActive(true);//顯示對話框
        textContent.text = ""; //清空文字
        textName.text = name;//對話者名稱 指定為 此物件的名稱

        string dialogString = date.dialougB;//要說的對話

        switch (state)
        {
            case NPCState.FirstDialog:
                dialogString = date.dialougA;
                break;
            case NPCState.Missioning:
                dialogString = date.dialougB;
                break;
            case NPCState.Finish:
                dialogString = date.dialougC;
                break;

        }

        for (int i = 0; i < dialogString.Length; i++)//Length包含所有文字
        {
            textContent.text += dialogString[i] + "";
            yield return new WaitForSeconds(interval);
        }
    }
}
