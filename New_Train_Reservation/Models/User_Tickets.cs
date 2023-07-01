using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace New_Train_Reservation.Models
{
    public class User_Tickets
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Time_Pickup { get; set; }
        [Required]
        public string Pickup_Station { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public int Seat_Number { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Ticket_Money { get; set; }
        [Required]
        public string Ticket_Class { get; set; }
        [Required]
        public int Train_Coach_Number { get; set; }
        
        public int Train_Number { get; set; }
        [Required]
        [ForeignKey("usersid")]
        public int usersid { get; set; }
        public Users? users { get; set; }

    }
}
