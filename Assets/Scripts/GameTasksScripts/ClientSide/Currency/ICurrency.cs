public interface ICurrency
{
    long Count {get;set;}
    void Add(int count);
    void Reduce(int count);
}