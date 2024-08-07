namespace EvaAPI.Lazy;

public interface IValueHolder<T>
{
    T GetValue(object parameter);
}