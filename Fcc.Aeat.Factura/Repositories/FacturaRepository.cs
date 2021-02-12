using Fcc.Aeat.Core.Data.Connection;
using Fcc.Aeat.Factura.Contracts.Repositories;
using Microsoft.Data.SqlClient;
using System;
using Dapper;
using System.Threading.Tasks;
using Fcc.Aeat.Factura.Contracts.Models;

namespace Fcc.Aeat.Factura.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly ConnectionString _connectionString;

        public FacturaRepository(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Add(FacturaRequest factura)
        {
            using (var conn = new SqlConnection(_connectionString.Value))
            {
                const string query =
                    "INSERT INTO FACTURA (Nif,Pais,Importe,Base,Iva,Fecha) VALUES " +
                    "(@Nif,@Pais,@Importe,@Base,@Iva,@Fecha)";
                await conn.ExecuteAsync(query,
                    new
                    {
                        Nif = factura.Nif,
                        Pais = factura.Pais,
                        Importe = factura.Importe,
                        Base = factura.Base,
                        Iva = factura.Iva,
                        Fecha = factura.Fecha
                    });
            }
        }
    }
}
