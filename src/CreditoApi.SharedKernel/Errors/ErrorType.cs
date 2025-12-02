namespace CreditoApi.SharedKernel.Errors;

/// <summary>
/// Represents the types of errors that can occur during application execution.
/// </summary>
/// <remarks>This enumeration categorizes errors into distinct types to facilitate error handling and
/// reporting. Each value corresponds to a specific kind of error scenario, such as validation failures or resource
/// conflicts.</remarks>
public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    NotFound = 2,
    Conflict = 3,
    Problem = 4
}
