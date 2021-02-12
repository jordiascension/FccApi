using Fcc.Aeat.Factura.Contracts.Models;
using System.Threading.Tasks;


namespace Fcc.Aeat.Factura.Contracts.Repositories
{
    public interface IFacturaRepository
    {
        Task Add(FacturaRequest factura);
    }
}
