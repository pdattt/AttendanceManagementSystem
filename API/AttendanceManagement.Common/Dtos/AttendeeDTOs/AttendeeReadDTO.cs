namespace AttendanceManagement.Common.Dtos.AttendeeDTOs
{
    public class AttendeeReadDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CardId { get; set; }
        public string Role { get; set; }
    }
}