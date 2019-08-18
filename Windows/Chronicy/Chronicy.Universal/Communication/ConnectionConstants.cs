namespace Chronicy.Communication
{
    public static class ConnectionConstants
    {
        public const string EndpointUri = "net.pipe://localhost";
        public const string EndpointName = "ChronicyEndpoint";

        public const string EndpointFullAddress = EndpointUri + "/" + EndpointName;
    }
}
