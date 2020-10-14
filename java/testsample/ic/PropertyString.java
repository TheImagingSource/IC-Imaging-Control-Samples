package ic;

public class PropertyString extends Property
{
    private String _value;
    private String _min;
    private String _max;

    public PropertyString(Grabber g, String Name) {
        super(g,Name);
        _type="String";
    }

    public boolean set(  int Value) {
        return true;
    }

    @SuppressWarnings("unchecked")
   public <T> T get(){
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