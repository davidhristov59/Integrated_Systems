using EShopApplication.Domain.DomainModels;
using EShopApplication.Domain.IdentityModels;

namespace EShopApplication.Repository.Interface;

public interface IUserRepository
{
    IEnumerable<EShopApplicationUser> GetAll();
    EShopApplicationUser Get(string id);
    void Insert(EShopApplicationUser entity);
    void Update(EShopApplicationUser entity);
    void Delete(EShopApplicationUser entity);
}