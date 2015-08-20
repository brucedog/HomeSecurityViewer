namespace HomeSecurity.Services
{
    public abstract class PollingServiceBase
    {
        private int pollingInterval = 100;

        public int PollingInterval
        {
            get { return pollingInterval; }
            set
            {
                if (value >= 100)
                    pollingInterval = value;
            }
        }
    }
}