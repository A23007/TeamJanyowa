using UnityEngine;

public class object_Damage : MonoBehaviour
{
    public int attackPower = 10;

    // プレイヤーから攻撃を受けたときに呼び出される（が、何も起こらない）
    public void TakeDamage(int damage)
    {
       // Debug.Log($"{gameObject.name} は攻撃されたが、反応しないオブジェクトです。");
    }
}
