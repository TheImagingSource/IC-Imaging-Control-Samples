package ic;

public class Property{ 
    String _name;
    String _type;
    Grabber _grabber;
    public  Property(Grabber g,String Name)
    {
        _name = Name;
        _type = "unknown";
        _grabber = g;
    }

    public boolean set( int Value){
        return false;
    }
    public boolean set(double Value){
        return false;
    }
    public boolean set( String Value){
        return false;
    }
    public boolean set( Boolean Value) {
        return false;
    }
    
    public <T> T get(){
        return null;
    }
    public <T> T getMinimum(){
        return null;
    }
    public <T> T getMaximum(){
        return null;
    }
}