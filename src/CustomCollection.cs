public interface CustomCollection<T>{
void add(T item);
bool remove (T item);
T get(int index);
int count { get; }
IEnumerable<T> getAll();
}