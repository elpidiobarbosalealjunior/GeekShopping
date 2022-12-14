﻿using GeekShopping.Email.Messages;
using GeekShopping.Email.Model;
using GeekShopping.Email.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.Repository;

public class EmailLogRepository : IEmailLogRepository
{
    private readonly DbContextOptions<MySQLContext> _context;

    public EmailLogRepository(DbContextOptions<MySQLContext> context)
    {
        _context = context;
    }

    public async Task LogEmail(UpdatePaymentResultMessage message)
    {
        EmailLog emailLog = new EmailLog()
        {
            Email = message.Email ?? "",
            SentDate = DateTime.Now,
            Log = $"Order - {message.OrderId} has been created successfully!"
        };
        await using var _db = new MySQLContext(_context);
        _db.EmailLogs.Add(emailLog);
        await _db.SaveChangesAsync();
    }
}
