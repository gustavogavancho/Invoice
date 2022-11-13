using Invoice.Entities.Models;

namespace Invoice.Contracts.Repositories;

public interface IDespatchRepository
{
    void CreateDespatch(Despatch despatch);
    void DeleteDespatch(Despatch despatch);
    Task<Despatch> GetDespatchAsync(Guid id, bool trackChanges);
    Task<IEnumerable<Despatch>> GetDespatchesAsync(bool trackChanges);
    Task<Despatch> GetDespatchBySerieAsync(string serie, int serialNumber, int correlativeNumber, bool trackChanges);
}
