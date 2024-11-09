namespace PruebaEjemploAPI_Backend.Transversal.Common
{
    public interface IAppLogger<T> where T : class
    {
        public void LogInfo(string message, params object[] args);

        public void LogWarning(string message, params object[] args);

        public void LogError(string message, params object[] args);
    }
}