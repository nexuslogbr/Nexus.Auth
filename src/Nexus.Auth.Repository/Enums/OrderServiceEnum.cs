namespace Nexus.Auth.Repository.Enums
{
    public class StringValueAttribute : Attribute
    {
        public string Value { get; }

        public StringValueAttribute(string value)
        {
            Value = value;
        }
    }

    public enum OrderServiceStatusEnum
    {
        [StringValue("Gerada")]
        Created = 1,

        [StringValue("Aguardando Item")]
        WaitItem,

        [StringValue("Liberada")]
        Released,

        [StringValue("Em Acessorização")]
        Accessory,

        [StringValue("Concluído")]
        Completed,

        [StringValue("Expedido")]
        Dispatched
    }

    public enum OrderServiceServiceStatusEnum
    {
        [StringValue("Liberado")]
        Released = 1,

        [StringValue("Concluído")]
        Completed,

        [StringValue("Atrasado")]
        Delayed,

        [StringValue("Avariado")]
        Damaged,

        [StringValue("Avariado Concluído")]
        DamagedCompled,

        [StringValue("Não Concluído")]
        NotCompleted
    }

    public enum OrderServiceDamageDegreeEnum
    {
        [StringValue("Danos Leves")]
        Light = 1,

        [StringValue("Danos Graves")]
        Serious,
    }
}
