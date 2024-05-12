

using System.Collections;

public  interface HurtableObjects
{
    public void TakeDamage(float amount);
    public IEnumerator Death();
}
