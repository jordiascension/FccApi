﻿using MediatR;
using System;

namespace Fcc.Aeat.Factura.Contracts.Commands
{
    public class FacturaAddCommand : IRequest
    {
        public string Pais { get; set; }
        public string Nif { get; set; }
        public decimal Importe { get; set; }
        public decimal Base { get; set; }
        public byte Iva { get; set; }
        public DateTime Fecha { get; set; }
    }
}
