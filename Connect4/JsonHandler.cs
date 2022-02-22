namespace Connect4
{
    using Newtonsoft.Json;

    /// <summary>
    /// Simple class for serializing / deserializing to and from json.
    /// </summary>
    public static class JsonHandler
    {
        /// <summary>
        /// Serializes the specified data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static string Serialize<T>(T data) where T : class, new()
        {
            var json = JsonConvert.SerializeObject(data) ?? "";
            return json;
        }

        /// <summary>
        /// Deserializes the specified json string into the type specified in T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The json string.</param>
        /// <returns></returns>
        public static T Deserialize<T>(string json) where T : class, new()
        {
            T output = JsonConvert.DeserializeObject<T>(json) ?? new();
            return output;
        }
    }
}
