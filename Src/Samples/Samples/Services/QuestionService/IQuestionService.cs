namespace Samples.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IQuestionService
    {
        Task<IList<string>> GetByTaskId(int id);
    }
}
