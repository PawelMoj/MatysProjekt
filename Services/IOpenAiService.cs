namespace MatysProjekt.Services
{
    public interface IOpenAiService
    {
        Task<string> CopleteSentence(string text); 
    }
}
