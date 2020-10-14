package ic;

public class PropertyInteger extends Property
{
    private Integer _value;
    private Integer _min;
    private Integer _max;
    public PropertyInteger(Grabber g, String Name) {
        super(g, Name);
        GetRangePropertyRange(_grabber, _name);
        _type ="Integer";
    }
    private native void GetRangePropertyRange(Grabber grabber, String Name);
    private native void SetPropertyInteger(Grabber grabber, String Name, int Value);
    private native int GetPropertyInteger(Grabber grabber, String Name);


    public boolean set(  int Value) {
        SetPropertyInteger(_grabber,_name, Value);
        return true;
    }

    @SuppressWarnings("unchecked")
   public <T> T get(){
       _value = GetPropertyInteger(_grabber,_name);
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