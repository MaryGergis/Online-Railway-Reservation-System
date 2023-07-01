using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace New_Train_Reservation.Models
{
    public enum Classes
    {
        Russie, VIP
    }
    public class Trains
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Train_Number { get; set; }
        [Required]
        public int Number_of_available_tickets { get; set; }
        [Required]
        public int Driver_ID { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public string Pickup_Station { get; set; }
        [Required]
        public DateTime Date_Pickup { get; set; }
        
        public int? Number_of_stoppages { get; set; }
        [Required]
        public Classes classes { get; set; }

    }
}
