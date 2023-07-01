using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace New_Train_Reservation.Models
{
    public enum Ticket_Classes
    {
        AC1, AC2
    }
    public class Admin_Tickets
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
        public Ticket_Classes Ticket_Classes { get; set; }

        [Required]
        public int Train_Coach_Number { get; set; }
        [Required]
        [ForeignKey("TrainID")]
        public int TrainID { get; set; }
        public Trains? Trains { get; set; }
        [Required]
        [ForeignKey("AdminID")]
        public int AdminID { get; set; }
        public Admin? Admin { get; set; }


    }
}
