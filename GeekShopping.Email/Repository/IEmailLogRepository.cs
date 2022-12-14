using GeekShopping.Email.Messages;

namespace GeekShopping.Email.Repository;

public interface IEmailLogRepository
{
    Task LogEmail(UpdatePaymentResultMessage message);
}
