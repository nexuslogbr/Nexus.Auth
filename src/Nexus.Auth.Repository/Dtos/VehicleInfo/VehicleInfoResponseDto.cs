namespace Nexus.Auth.Repository.Dtos.VehicleInfo;

public class VehicleInfoResponseDto
{
    public int Id { get; set; }
    public bool Blocked { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime ChangeDate { get; set; }

    public string Requester { get; set; } // Frotista
    public DateTime Invoicing { get; set; } // Faturamento
    public int Aging { get; set; } // Envelhecimento
    public DateTime Decal { get; set; } // Decalque
    public string Plate { get; set; } // Placa
    public DateTime PlateRec { get; set; } // Rec. Placa
    public DateTime CrlvRec { get; set; } // Rec. CRLV
    public DateTime LetterRec { get; set; } // Rec. Carta Fisco
    public DateTime BasicAccess { get; set; } // Acess. Basico
    public string Carpet { get; set; } // Tapete
    public string Revision { get; set; } // Revisão
    public DateTime Licensed { get; set; } // Emplacado
    public DateTime CustomerKit { get; set; } // Fim Kit Cliente
    public DateTime DocApply { get; set; } // Aplica. Doc
    public DateTime VpcEnd { get; set; } // FIM VPC
    public DateTime Dispatched { get; set; } // Expedido
    public string Street { get; set; } // Rua
    public int Parking { get; set; } // Vaga

    public int ChassisId { get; set; }
    public string ChassisNumber { get; set; }
}