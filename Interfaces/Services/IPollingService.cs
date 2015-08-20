namespace HomeSecurity.Interfaces.Services
{
    public interface IPollingService
    {
        /// <summary>
        /// Gets or sets the polling interval (milliseconds).
        /// </summary>
        /// <value>
        /// The polling interval.
        /// </value>
        int PollingInterval { get; set; }
    }
}