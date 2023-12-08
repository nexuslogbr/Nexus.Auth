﻿
namespace Nexus.Auth.Repository.Dtos.OrderService
{
    public class OrderServiceDto
    {
        public string? Customer { get; set; } // Cliente
        public string? RequesterCode { get; set; } // Código Solicitante
        public string? Requester { get; set; } // Solicitante
        public string? Manufacturer { get; set; } // Fabricante
        public string? Model { get; set; } // Modelo
        public string? ChassisNumber { get; set; } // Chassi
        public DateTime? Invoicing { get; set; } // Data Faturamento
        public List<string>? Services { get; set; } // Serviço
        public string? Street { get; set; } // Rua
        public int? Parking { get; set; } // Vaga
        public string? Plate { get; set; } // Placa
    }
}
