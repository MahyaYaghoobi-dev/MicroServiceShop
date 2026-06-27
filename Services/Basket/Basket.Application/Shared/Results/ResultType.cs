namespace Catalog.Application.Shared.Results;


    public enum ResultType
    {
        Success,
        NotFound,
        BadRequest,
        ValidationError,
        Conflict,
        Unauthorized
    }
