package ic;

public abstract class GrabberListener{
    {
        this._InternalListener = createListener();
    }
    public void finalize() {
        deleteListener(this._InternalListener);
    }


    protected long _InternalListener;

    //public MemBuffer[] _MemBufferCollection;


    private native void deleteListener(long InternalListener); 
    private native long createListener(); 

    public abstract void frameReady( ic.Grabber caller, ic.MemBuffer Buffer, int FrameNumber );
    public abstract void deviceLost(ic.Grabber caller);
}