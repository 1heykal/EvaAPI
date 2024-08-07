namespace EvaAPI.Lazy;

public class ValueHolder<T> : IValueHolder<T>
{
    private readonly Func<object, T> getValue;
    
    public ValueHolder(Func<object, T> getValue)
    {
        this.getValue = getValue;
    }
    
    private T value;
    public T GetValue(object parameter)
    {
        if (value is null)
        {
            value = getValue(parameter);
        }

        return value;
    }
}