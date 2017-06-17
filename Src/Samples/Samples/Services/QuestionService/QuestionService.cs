namespace Samples.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class QuestionService : IQuestionService
    {
        private IDictionary<int, IList<string>> _questionMap = new Dictionary<int, IList<string>>
        {
            {1, new List<string>{"Question 1", "Question 2", "Question 3" } },
            {2, new List<string>{"Question 4", "Question 5" } },
            {3, new List<string>{"Question 6", "Question 7", "Question 8", "Question 9" } },
            {4, new List<string>{"Question 10", "Question 11", "Question 12", "Question 13" } }
        };

        public async Task<IList<string>> GetByTaskId(int id)
        {
            // simulate the network delay
            await Task.Delay(300);

            return _questionMap.ContainsKey(id) ? _questionMap[id] : new List<string>();
        }
    }
}
