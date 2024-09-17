using Microsoft.AspNetCore.Mvc;

namespace ApiRestNetforemost.DTO
{
    public class TaskDTO
    {
        public string IdTask { get; set; } = null!;
        public string? IdUser { get; set; }
        public string Title { get; set; } = null!;
        public string? DescriptionTask { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Finished { get; set; }
        public bool Deleted { get; set; }
        public string? Tags { get; set; }
        public int? IdPriority { get; set; }

        public string ValidateTask()
        {
            if (string.IsNullOrWhiteSpace(Title))            
                return "Title is required.";
            if (ExpirationDate == null)
                return "Expiration Date is required";

            return string.Empty; 
        }
    }
}
