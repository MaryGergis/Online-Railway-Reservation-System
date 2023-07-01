using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace New_Train_Reservation.Models
{
    public enum MessageType
    {
        Suggestions, Complaints
    }
    public class Suggestions_Complaints
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public MessageType? Message_Type { get; set; }
        [Required]
        public string First_Name { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Phone]
        [Required]
        public string Phone_Number { get; set; }
        [Required]
        [MaxLength(100)] 
        
        public string Subject { get; set; }
        public string Details { get; set; }
        [ForeignKey("UsersID")]
        public int UsersID { get; set; }
        public virtual Users? Users { get; set; }

    }
}
