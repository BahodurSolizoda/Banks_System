using Infrastructure.Responses;

namespace Infrastructure.Interfaces;

public interface IGenericService<T>
{
    Task<ApiResponse<List<T>>> GetAll();
    Task<ApiResponse<T>> GetById(int id);
    Task<ApiResponse<bool>> Add(T data);
    Task<ApiResponse<bool>> Update(T data);
    Task<ApiResponse<bool>> Delete(int id);
    
    //Get all through condition
    Task<ApiResponse<List<T>>> GetByCondition(string condition);
    
    //Existance
    Task<ApiResponse<bool>> Exists(int id);
    
    //Counting
    Task<ApiResponse<int>> Count();
    
    //Partial changing
    Task<ApiResponse<bool>> UpdatePartial(int id, string propertyName, object newValue);
    
    
    
}