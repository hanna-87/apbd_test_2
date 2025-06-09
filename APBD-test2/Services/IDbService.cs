

using APBD_test2.DTOs;

namespace APBD_test2.Services;

public interface IDbService
{
    Task<int> CreateCustomerAsync(CreateCustomerDto request);
    Task<GetCustomerInfoDto> GetCustomerInfoAsync(int id); 
}