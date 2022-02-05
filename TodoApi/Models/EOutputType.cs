namespace TodoApi.Models
{
    public enum EOutputType
    {
        Success = 1,
        InvalidInput,
        BusinessValidation,
        Failure,
        NotFound,
        IntegrationError
    }
}
