public interface ICurrencyService
{
    int Coins { get; }
    void Add(int amount);
    bool Spend(int amount);
}
