namespace SQS.Listener.Lambda
{
    public static class ContentType
    {
        public static string GetContentType(this string payload)
        {
            if (!string.IsNullOrEmpty(payload))
            {
                if (payload.StartsWith("<"))
                {
                    return "application/xml";
                }

                if (payload.StartsWith("{") || payload.StartsWith("["))
                {
                    return "applciation/json";
                }
            }

            return "text/plain";
        }
    }
}
