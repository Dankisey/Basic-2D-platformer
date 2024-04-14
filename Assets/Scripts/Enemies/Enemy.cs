public class Enemy : Entity
{
    protected override void TryDie()
    {
        Destroy(gameObject);
    }
}