public interface IInteractable
{
    public bool IsInteractable { get; set; }
    void Interact();
}

public interface IDamagable
{
    public int Health { get; set; }

    public void TakeDamage(int damage);

}