namespace Hermes.Services
{
    public interface IHermesToastService
    {
        void ShortAlert(string msg);
        void LongAlert(string msg);
    }
}
