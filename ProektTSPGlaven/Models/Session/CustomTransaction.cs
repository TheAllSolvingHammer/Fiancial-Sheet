namespace ProektTSPGlaven.Models.Session
{
    public class CustomTransaction
    {
        private static CustomTransaction instance;
        public int accountId { get; set; }

        public static CustomTransaction getInstance()
        {
            if (instance == null)
            {
                instance = new CustomTransaction();
            }
            return instance;
        }
    }
}
