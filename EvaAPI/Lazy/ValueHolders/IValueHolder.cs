namespace EvaAPI.Lazy.ValueHolders;

public interface IValueHolder<T>
{
    T GetValue(object parameter);
}