namespace CoinPurse.Data.Decorators;

public interface IIdentifiable<TKey>
{
    TKey GetId();

    void SetId(TKey id);
}