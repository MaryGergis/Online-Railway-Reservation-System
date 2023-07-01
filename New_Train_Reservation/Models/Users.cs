using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.OracleClient;

namespace New_Train_Reservation.Models
{
    public enum Gender
    {
        Male, Female
    }
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [Required]
        public string User_Fname { get; set; }
        [Required]
        public string User_Lname { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"[A-Za-z0-9.-]+@gmail.com")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Phone]
        [Required]
        public string User_phone_number { get; set; }
        [Required]
        [MaxLength(14)]
        [MinLength(14)]
        public string National_Pass_Number { get; set; }
        public int? Number_of_purchased_tickets { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public virtual ICollection<User_Tickets>? TicketId { get; set; }
    }
}
    
