using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace New_Train_Reservation.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int Card_Number { get; set; }
        public DateTime Expiration_date { get; set; }

        public int CVV { get; set; }
        public string User_Phone_number { get; set; }

        public string Current_bank_account { get; set; }

        public double? Total_amount_in_card { get; set; }
        public double Booking_Fee { get; set; }

        public virtual Users? users { get; set; }
    }
    
}
