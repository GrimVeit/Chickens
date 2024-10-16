using UnityEngine;

public class NormalEgg : Egg
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Basket>())
        {
            OnEggWin_EggValues?.Invoke(eggValues);
            OnEggDown_Position?.Invoke(transform.position);
            Dispose();
        }

        if (other.GetComponent<Earth>())
        {
            OnEggDown?.Invoke();
            OnEggDown_Position?.Invoke(transform.position);
            Dispose();
        }
    }
}
