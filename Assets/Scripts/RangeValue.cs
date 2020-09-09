public class RangeValue<T>
{
    public T Minimum { get; set; }
    public T Maximum { get; set; }
    public T Current { get; set; }

    public RangeValue(T minimum, T maximum, T current){
        this.Minimum = minimum;
        this.Maximum = maximum;
        this.Current = current;
    }

    public RangeValue()
    {
    }
}
