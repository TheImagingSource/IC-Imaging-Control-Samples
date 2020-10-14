package ic;

public class PropertyBoolean extends Property
{
    private Boolean _value;
    private Boolean _min;
    private Boolean _max;
    public PropertyBoolean(Grabber g, String Name) {
        super(g, Name);
        _min = false;
        _max = true;
        _type = "Boolean";
    }
    private native void SetPropertyBoolean(Grabber grabber, String Name, Boolean Value);
    private native Boolean GetPropertyBoolean(Grabber grabber, String Name);


    public boolean set(  Boolean Value) {
        SetPropertyBoolean(_grabber,_name, Value);
        return true;
    }

    @SuppressWarnings("unchecked")
   public <T> T get(){
        // Attention : GetPropertyBoolean returns always false. 
       GetPropertyBoolean(_grabber,_name);
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