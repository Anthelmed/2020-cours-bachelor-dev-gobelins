public static class GameManager
{
    public static float difficulty = 2f;

    public delegate void SampleEventHandler();
    public static event SampleEventHandler SampleEvent;

    public static void CallEvent()
    {
        //En une seule ligne SampleEvent?.Invoke();

        if (SampleEvent != null)
            SampleEvent();
    } 
}
