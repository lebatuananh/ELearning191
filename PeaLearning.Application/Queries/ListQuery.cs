namespace PeaLearning.Application.Queries
{
    public abstract class ListQuery
    {
        public int Skip { get; private set; }
        public int Take { get; private set; }
        public string Query { get; private set; }

        protected ListQuery(int skip, int take, string query = null)
        {
            Skip = skip;
            Take = take;
            Query = query;
        }
    }
}
