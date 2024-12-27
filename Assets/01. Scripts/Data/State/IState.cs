public abstract class IState
{
    protected DataManager dataManager;

    public abstract void SendData();
    
    public abstract void LoadData();
    
    public abstract void ProcessData();
}