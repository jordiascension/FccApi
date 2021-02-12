using Fcc.Aeat.Factura.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fcc.Aeat.Factura.Queries.Contracts
{
    public interface IFacturaQueries
    {
        Task<IEnumerable<Factura.Contracts.Models.Factura>>
            GetAll(FacturaRequest facturaRequest);
    }
}
