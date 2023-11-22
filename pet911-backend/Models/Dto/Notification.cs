namespace pet911_backend.Models.Dto
{
    public class Notification
    {
        public int? Id { get; set; }
        public string Email_tx { get; set; }
        public string Email_rx { get; set; }
        public string Message { get; set; }

    }
}
