using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.Email.Model;

[Table("email_logs")]
public class EmailLog
{
    [Key]
    [Required]
    [Column("email_log_id")]
    public int EmailLogId { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("log")]
    public string Log { get; set; }

    [Column("sent_date")]
    public DateTime SentDate { get; set; }
}
