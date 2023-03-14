using Mirror;

public interface IDamageTaker
{
    [Command(requiresAuthority = false)]
    void TakeDamage(float damage);
}