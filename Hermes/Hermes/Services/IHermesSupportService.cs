using System;

namespace Hermes.Services
{
    public interface IHermesSupportService
    {
        string GetUniqueIdentifier();
        Guid HermesIdentifier();
    }
}
