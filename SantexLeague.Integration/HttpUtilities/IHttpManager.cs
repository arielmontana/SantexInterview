using System.Collections.Generic;
using System.Threading.Tasks;

namespace SantexLeague.Integration.HttpUtilities
{
    public interface IHttpManager
    {
        Task<T> Get<T>(string urlCommand);

        //Task<T> Post<T, U>(string urlCommand, U serializableEntity);
    }
}