﻿using Invoice.Entities;

namespace Invoice.Contracts;

public interface ISenderRepository
{
    Task CreateSender(Sender sender);
    Task<Sender> GetSender(Guid id);
    Task<List<Sender>> GetSenders();
}