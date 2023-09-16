namespace pet911_backend.Models.Dto
{
    public class ServiceDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public TimeOnly? OpeningTime { get; set; }
        public TimeOnly? ClosingTime { get; set; }
        public string ContactNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Catalogue { get; set; }
        public Boolean? Sponsored { get; set; }
        public string? IdUser { get; set; }
    }
}
