using Newtonsoft.Json;

namespace ProektTSPGlaven.Models.Session
{
    public class Session2
    {
        private ISession session;
        private static Session2 instance;
        public Session2(ISession session) {
            this.session = session;
        }

 
        public void SetObject(string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public T GetObject<T>(string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
