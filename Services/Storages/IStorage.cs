namespace Services.Storages;
public interface IStorage<in T>
{
    void Add(T item);
    void Delete(T item);
    void Update(T item);
}