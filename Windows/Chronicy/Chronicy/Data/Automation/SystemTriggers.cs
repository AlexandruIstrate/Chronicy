namespace Chronicy.Data.Automation
{
    public static class SystemTriggers
    {
        public static ITrigger CardAdded = new CardAddedTrigger();
        public static ITrigger CardRemoved = new CardRemovedTrigger();
    }

    public class CardAddedTrigger : Trigger
    {
  
    }

    public class CardRemovedTrigger : Trigger
    {
        
    }
}
