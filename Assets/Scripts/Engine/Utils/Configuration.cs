using Newtonsoft.Json;

namespace Engine.Utils
{
    public static class Configuration
    {
        // ATTRIBUTES

        public static JsonSerializerSettings JsonSettings = new JsonSerializerSettings
            {TypeNameHandling = TypeNameHandling.All};

        // CONSTRUCTORS

        // METHODS
    }
}