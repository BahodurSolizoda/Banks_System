using Infrastructure.Responses;

namespace Infrastructure.Interfaces;

public interface IGenericService<T>
{
    ApiResponse<List<T>> GetAll();
    ApiResponse<T> GetById(int id);
    ApiResponse<bool> Add(T data);
    ApiResponse<bool> Update(T data);
    ApiResponse<bool> Delete(int id);
    
    //Get all through condition
    ApiResponse<List<T>> GetByCondition(string condition);
    
    //Existance
    ApiResponse<bool> Exists(int id);
    
    //Counting
    ApiResponse<int> Count();
    
    //Partial changing
    ApiResponse<bool> UpdatePartial(int id, string propertyName, object newValue);
    
    
}