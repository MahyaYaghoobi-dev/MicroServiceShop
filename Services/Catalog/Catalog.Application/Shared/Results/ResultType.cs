namespace Catalog.Application.Shared.Result;


    public enum ResultType
    {
        Success,
        NotFound,
        BadRequest,
        ValidationError,
        Conflict,
        Unauthorized
    }
