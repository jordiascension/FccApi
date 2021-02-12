using Fcc.Aeat.Core.Data.Connection;
using Fcc.Aeat.Factura.Contracts.Models;
using Fcc.Aeat.Factura.Queries.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
//sobretot posar using Dapper
using Dapper;

namespace Fcc.Aeat.Factura.Queries.Impl
{
    public class FacturaQueries : IFacturaQueries
    {
        private readonly ConnectionString _connectionString;

        public FacturaQueries(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Factura.Contracts.Models.Factura>>
            GetAll(FacturaRequest facturaRequest)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString.Value))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Nif", facturaRequest.Nif);


                var facturasDto = (await conn
                                            .QueryAsync<Factura.Contracts.Models.Factura>
                                            ("Select * from Factura where Nif = @Nif",
                                            queryParameters))
                                            .ToList();

                return facturasDto;
            }
        }
    }
}
