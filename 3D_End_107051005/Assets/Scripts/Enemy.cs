using UnityEngine;
using UnityEngine.AI;   //引用人工智慧API

public class Enemy : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent nav;
    private Animator ani;
    private float timer;        //計時器

    [Header("移動速度"), Range(0, 50)]
    public float speed = 3;
    [Header("停止距離"), Range(0, 50)]
    public float stopDistance = 2.5f;
    [Header("攻擊冷卻時間"), Range(0, 50)]
    public float cd = 2f;
   [Header("攻擊中心點")]
    public Transform atkPoint;
    [Header("攻擊長度"), Range(0f, 5f)]
    public float atkLength;
    [Header("攻擊力"), Range(0, 500)]
    public float atk=30;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();     //取得身上的元件(代理器-怪物)
        ani = GetComponent<Animator>();

        player = GameObject.Find("女孩").transform;       //尋找其他遊戲物件.變形物件

        //代理器的速度與停止距離
        nav.speed = speed;
        nav.stoppingDistance = stopDistance;
    }
    private void Update()
    {
        Track();
        Attack();
    }

    /// <summary>
    /// 繪製圖示事件，僅在unity內顯示
    /// </summary>

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(atkPoint.position, atkPoint.forward * atkLength);
    }

    /// <summary>
    /// 射線擊中的物件
    /// </summary>
    private RaycastHit hit;

    //攻擊
    private void Attack()
    {
        if (nav.remainingDistance < stopDistance)
        {
            timer += Time.deltaTime;

            Vector3 pos = player.position;
            pos.y = transform.position.y;

            transform.LookAt(pos);

            if (timer >= cd)
            {
                ani.SetTrigger("攻擊觸發");
                timer = 0;

                //物理 射線碰撞(中心點座標，中心點前方，射線擊中的物件out，攻擊長度，圖層)
                //圖層 1<<8     8為玩家新設圖層
                //如果射線大道物歉疚執行{}
                if (Physics.Raycast(atkPoint.position, atkPoint.forward, out hit, atkLength, 1 << 8))
                {
                    //碰撞物件.取得元件<玩家>(),受傷()
                    hit.collider.GetComponent<Player>().Damage(atk);
                }
            }
        }
    }

    private float hp = 60;

    /// <summary>
    /// 受傷
    /// </summary>
    /// damage為接收傷害值
    public void Damage(float damage)
    {
        hp -= damage;
        ani.SetTrigger("受傷觸發");

        if (hp <= 0) Dead();
    }
    /// <summary>
    /// 死亡觸發
    /// </summary>
    private void Dead()
    {
        nav.isStopped = true;//關閉導覽器
            enabled = false;//關閉腳本
        ani.SetBool("死亡開關",true);//死亡動畫

    }
    //追蹤
    private void Track()
    {
        nav.SetDestination(player.position);        //代理器.設定目的地(玩家的座標)

        ani.SetBool("跑步開關", nav.remainingDistance > stopDistance);
    }

}
