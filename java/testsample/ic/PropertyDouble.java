package ic;

public class PropertyDouble extends Property
{
    private Double _value;
    private Double _min;
    private Double _max;

    public PropertyDouble(Grabber g, String Name) {
        super(g, Name);
        GetRangePropertyDouble(_grabber , _name);
        _type = "Double";
    }

    private native void GetRangePropertyDouble(Grabber grabber, String Name);
    private native void SetPropertyDouble(Grabber grabber, String Name, double Value);
    private native double GetPropertyDouble(Grabber grabber, String Name);

    public boolean set(  double Value) {
        SetPropertyDouble( _grabber, _name, Value);
        return true;
    }

    @SuppressWarnings("unchecked")
    public <T> T get(){
        GetPropertyDouble( _grabber, _name);
        return (T)_value;
    }
    @SuppressWarnings("unchecked")
    public <T> T getMinimum(){
        return (T)_min;
    }
    @SuppressWarnings("unchecked")
    public <T> T getMaximum(){
        return (T)_max;
    }

}