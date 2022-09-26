﻿using ModelsDB;

namespace Services.Storages;
public interface IClientStorage : IStorage<ClientDB>
{    
    public BankContext Data { get; }
    void AddAccount(Guid id, AccountDB account);
    ClientDB Get(Guid id);
    void DeleteAccount(Guid id, AccountDB account);    
}