using UnityEngine;

public class Attacker : MonoBehaviour
{
    private float _lastTime;

    public void Action(Health target, int damage, float cooldown)
    {
        if (Time.time >= _lastTime + cooldown)
        {
            target.TakeDamage(damage);
            _lastTime = Time.time;
        }
    }
}
