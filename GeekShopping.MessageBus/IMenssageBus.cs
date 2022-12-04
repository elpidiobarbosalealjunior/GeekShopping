namespace GeekShopping.MessageBus;

public interface IMenssageBus
{
    Task PublicMessage(BaseMessage message, string queueName);
}