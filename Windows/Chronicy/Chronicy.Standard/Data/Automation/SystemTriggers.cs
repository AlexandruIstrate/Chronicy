namespace Chronicy.Standard.Data.Automation
{
    public static class SystemTriggers
    {
        public static ITrigger CardAdded = new CardAddedTrigger();
    }

    public class CardAddedTrigger : Trigger
    {
        public CardAddedTrigger()
        {
            SetupCardWatch();
        }

        private void SetupCardWatch()
        {
            // TODO: Implement
        }
    }
}
