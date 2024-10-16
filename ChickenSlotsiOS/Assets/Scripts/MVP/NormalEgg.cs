using UnityEngine;

public class NormalEgg : Egg
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Basket>())
        {
            OnEggWin?.Invoke(eggValues);
            Dispose();
        }

        if (other.GetComponent<Earth>())
        {
            OnEggDown?.Invoke(eggValues, transform.position);
            Dispose();
        }
    }
}
