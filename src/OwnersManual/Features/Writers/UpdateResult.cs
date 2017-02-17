namespace OwnersManual.Features.Writers
{
    public class UpdateResult
    {
        public UpdateResult(ContentResult contentResult)
        {
            ContentResult = contentResult;
        }

        public ContentResult ContentResult { get; }
    }
}
