namespace ProjetoReferencia.Domain.Constantes
{
    public class DescriptionSwagger
    {
        public static class DescriptionBike
        {
            public const string PostTag = "Cadastrar uma nova moto";
            public const string GetAllTag = "Consultar motos existentes";
            public const string PutTag = "Modificar a placa de uma moto";
            public const string GetByIdTag = "Consultar motos existentes por Id";
            public const string DeleteTag = "Remover uma moto";
        }

        public static class DescriptionDeliveryDriver
        {
            public const string PostTag = "Cadastrar entregadores";
            public const string PostEnviarFotoCNHTag = "Enviar foto da CNH";

        }

        public static class DescriptionRental
        {
            public const string PostTag = "Alugar uma moto";
            public const string GetByIdTag = "Consultar locação por id";
            public const string PutTag = "Informar data de devolução e calcular valor";
        }
    }
}
