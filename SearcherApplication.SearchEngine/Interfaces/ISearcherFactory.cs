namespace SearcherApplication.SearchEngine.Interfaces
{
    public interface ISearcherFactory
    {
        ISearcher CreateBingSearcher();

        ISearcher CreateGoogleSearcher();
    }
}
