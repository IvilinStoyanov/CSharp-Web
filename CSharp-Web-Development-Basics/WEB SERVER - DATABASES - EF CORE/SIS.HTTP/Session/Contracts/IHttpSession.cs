namespace SIS.HTTP.Session.Contracts
{
   public interface IHttpSession
    {
        string Id { get; }

        object GetParameters(string name);

        bool ContainsParameters(string name);

        void AddParameter(string name, object parameter);

        void ClearParameters();
    }
}
