namespace SearcherApplication.Models.ViewModels
{
    public class EmptySearchViewModel
    {
        public string ErrorMessage { get; set; }

        public EmptySearchViewModel(string message)
        {
            ErrorMessage = message;
        }
    }
}