using System.Threading.Tasks;

namespace FunctionApp4.Repositories;

public interface IRepository
{
    Task<bool> DeletePhotoAsync(string photoName);
}