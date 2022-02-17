namespace Connect4
{
    using Newtonsoft.Json;

    public static class JsonHandler
    {
        public static string Serialize<T>(T data)
        {
            var json = JsonConvert.SerializeObject(data);
            return json;
        }

        public static T Deserialize<T>(string json) where T : class, new()
        {
            T output = JsonConvert.DeserializeObject<T>(json) ?? new();
            return output;
        }
    }
}
