using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleFlag.AspNetCore.Endpoints.Dtos;

namespace SimpleFlag.AspNetCore.Endpoints;

/// <summary>
/// Represents the endpoints for handling feature flags in the SimpleFlag.AspNetCore namespace.
/// </summary>
internal class SimpleFlagEndpointsHandler
{
    private readonly SimpleFlagEndpoints _simpleFlagEndpoints;
    /// <summary>
    /// Initializes a new instance of the SimpleFlagEndpoints class.
    /// </summary>
    /// <param name="simpleFlagClient">The SimpleFlag client used for handling feature flags.</param>
    public SimpleFlagEndpointsHandler(ISimpleFlagClient simpleFlagClient)
    {
        _simpleFlagEndpoints = new SimpleFlagEndpoints(simpleFlagClient);
    }

    #region FeatureFlag

    /// <summary>
    /// Handles the GetFeatureFlagByDomainDelegate endpoint by calling the corresponding method in the GetFeatureFlagsEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task GetFeatureFlagsDelegateAsync(HttpContext context)
    {
        try
        {
            var domain = context.Request.RouteValues["domain"] as string;

            var cancellationToken = context.RequestAborted;

            var result = await _simpleFlagEndpoints.GetFeatureFlagsAsync(domain ?? string.Empty, cancellationToken);

            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsJsonAsync(result);
        }
        catch (Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while retrieving the feature flag by domain.",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    /// <summary>
    /// Handles the AddFeatureFlagDelegate endpoint by calling the corresponding method in the AddFeatureFlagEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task AddFeatureFlagDelegateAsync(HttpContext context)
    {
        try
        {
            // Deserialize the FeatureFlag from the request body
            var featureFlagDto = await context.Request.ReadFromJsonAsync<CreateFeatureFlagRequest>();

            if (featureFlagDto == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid feature flag data.");
                return;
            }

            // Get the cancellation token from the context
            var cancellationToken = context.RequestAborted;

            var result = await _simpleFlagEndpoints.AddFeatureFlagAsync(featureFlagDto, cancellationToken);

            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsJsonAsync(result);
        }
        catch (Exception ex)
        {
            // Return ProblemDetails with the exception details
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while adding the feature flag.",
                Detail = ex.Message,
                Status = ex is SimpleFlagExistException ? StatusCodes.Status409Conflict : StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    /// <summary>
    /// Handles the UpdateFeatureFlagDelegateAs endpoint by calling the corresponding method in the UpdateFeatureFlagEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task UpdateFeatureFlagDelegateAsync(HttpContext context)
    {
        try
        {
            var id = context.Request.RouteValues["id"] as string;
            if (!Guid.TryParse(id, out var flagId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid flag id.");
                return;
            }

            // Deserialize the FeatureFlag from the request body
            var featureFlagDto = await context.Request.ReadFromJsonAsync<UpdateFeatureFlagRequest>();

            if (featureFlagDto == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid feature flag data.");
                return;
            }

            // Get the cancellation token from the context
            var cancellationToken = context.RequestAborted;

            var result = await _simpleFlagEndpoints.UpdateFeatureFlagAsync(flagId, featureFlagDto, cancellationToken);

            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsJsonAsync(result);
        }
        catch (Exception ex)
        {
            // Return ProblemDetails with the exception details
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while updating the feature flag.",
                Detail = ex.Message,
                Status = ex is SimpleFlagExistException ? StatusCodes.Status409Conflict : StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    /// <summary>
    /// Handles the DeleteFeatureFlagDelegate endpoint by calling the corresponding method in the DeleteFeatureFlagEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task DeleteFeatureFlagDelegateAsync(HttpContext context)
    {
        try
        {
            // get flag id from the route
            var id = context.Request.RouteValues["id"] as string;
            if (!Guid.TryParse(id, out var flagId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid flag id.");
                return;
            }

            // Get the cancellation token from the context
            var cancellationToken = context.RequestAborted;

            await _simpleFlagEndpoints.DeleteFeatureFlagAsync(flagId, cancellationToken);

            context.Response.StatusCode = StatusCodes.Status204NoContent;
        }
        catch (Exception ex)
        {
            // Return ProblemDetails with the exception details
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while deleting the feature flag.",
                Detail = ex.Message,
                Status = ex is SimpleFlagExistException ? StatusCodes.Status409Conflict : StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
    #endregion FeatureFlag

    #region Segment
    /// <summary>
    /// Handles the AddSegmentDelegate endpoint by calling the corresponding method in the AddSegmentEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task AddSegmentDelegateAsync(HttpContext context)
    {
        try
        {
            // Deserialize the Segment from the request body
            var segmentDto = await context.Request.ReadFromJsonAsync<CreateSegmentRequest>();

            if (segmentDto == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid segment data.");
                return;
            }

            // Get the cancellation token from the context
            var cancellationToken = context.RequestAborted;

            var result = await _simpleFlagEndpoints.AddSegmentAsync(segmentDto, cancellationToken);

            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsJsonAsync(result);
        }
        catch (Exception ex)
        {
            // Return ProblemDetails with the exception details
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while adding the segment.",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    /// <summary>
    /// Handles the GetFeatureFlagDelegate endpoint by calling the corresponding method in the GetFeatureFlagEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task GetSegmentsDelegateAsync(HttpContext context)
    {
        try
        {
            var cancellationToken = context.RequestAborted;

            var result = await _simpleFlagEndpoints.GetSegmentsAsync(cancellationToken);

            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsJsonAsync(result);
        }
        catch (Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while retrieving the segments.",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    /// <summary>
    /// Handles the GetSegmentDelegate endpoint by calling the corresponding method in the GetSegmentEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task UpdateSegmentDelegateAsync(HttpContext context)
    {
        try
        {
            var id = context.Request.RouteValues["id"] as string;
            if (!Guid.TryParse(id, out var segmentId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid segment id.");
                return;
            }

            var segmentDto = await context.Request.ReadFromJsonAsync<UpdateSegmentRequest>();

            if (segmentDto == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid segment data.");
                return;
            }

            var cancellationToken = context.RequestAborted;

            var result = await _simpleFlagEndpoints.UpdateSegmentAsync(segmentId, segmentDto, cancellationToken);

            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsJsonAsync(result);
        }
        catch (Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while updating the segment.",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    /// <summary>
    /// Handles the UpdateSegmentDelegate endpoint by calling the corresponding method in the UpdateSegmentEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task DeleteSegmentDelegateAsync(HttpContext context)
    {
        try
        {
            // Get segment id from the route
            var id = context.Request.RouteValues["id"] as string;
            if (!Guid.TryParse(id, out var segmentId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid segment id.");
                return;
            }

            // Get the cancellation token from the context
            var cancellationToken = context.RequestAborted;

            await _simpleFlagEndpoints.DeleteSegmentAsync(segmentId, cancellationToken);

            context.Response.StatusCode = StatusCodes.Status204NoContent;
        }
        catch (Exception ex)
        {
            // Return ProblemDetails with the exception details
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while deleting the segment.",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    #endregion Segment

    #region Users

    /// <summary>
    /// Handles the GetUsersDelegate endpoint by calling the corresponding method in the GetUsersEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task GetUsersDelegateAsync(HttpContext context)
    {
        try
        {
            var segment = context.Request.RouteValues["segment"] as string;

            var cancellationToken = context.RequestAborted;

            var result = await _simpleFlagEndpoints.GetUsersAsync(segment, cancellationToken);

            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsJsonAsync(result);
        }
        catch (Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while retrieving the users from the segment.",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    /// <summary>
    /// Handles the AddUsersDelegate endpoint by calling the corresponding method in the AddUsersEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task AddUsersDelegateAsync(HttpContext context)
    {
        try
        {
            // Deserialize the Users from the request body
            var usersDto = await context.Request.ReadFromJsonAsync<AddUsersRequest>();

            if (usersDto == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid users data.");
                return;
            }

            // Get the cancellation token from the context
            var cancellationToken = context.RequestAborted;

            var result = await _simpleFlagEndpoints.AddUsersAsync(usersDto, cancellationToken);

            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsJsonAsync(result);
        }
        catch (Exception ex)
        {
            // Return ProblemDetails with the exception details
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while adding the users.",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    /// <summary>
    /// Handles the UpdateUsersDelegate endpoint by calling the corresponding method in the UpdateUsersEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task UpdateUsersDelegateAsync(HttpContext context)
    {
        try
        {
            var usersDto = await context.Request.ReadFromJsonAsync<UpdateUsersRequest>();

            if (usersDto == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid users data.");
                return;
            }

            var cancellationToken = context.RequestAborted;

            var result = await _simpleFlagEndpoints.UpdateUsersAsync(usersDto, cancellationToken);

            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsJsonAsync(result);
        }
        catch (Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while updating the users.",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    /// <summary>
    /// Handles the RemoveUsersDelegate endpoint by calling the corresponding method in the RemoveUsersEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task RemoveUsersDelegateAsync(HttpContext context)
    {
        try
        {
            // Deserialize the Users from the request body
            var usersDto = await context.Request.ReadFromJsonAsync<RemoveUsersRequest>();

            if (usersDto == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid users data.");
                return;
            }

            // Get the cancellation token from the context
            var cancellationToken = context.RequestAborted;

            await _simpleFlagEndpoints.DeleteUsersAsync(usersDto, cancellationToken);

            context.Response.StatusCode = StatusCodes.Status204NoContent;
        }
        catch (Exception ex)
        {
            // Return ProblemDetails with the exception details
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while removing the users.",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    #endregion Users

    #region Segment to FeatureFlag
    /// <summary>
    /// Handles the CleanUsersDelegate endpoint by calling the corresponding method in the CleanUsersEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task AddSegmentToFeatureFlagDelegateAsync(HttpContext context)
    {
        try
        {
            var segment = context.Request.RouteValues["segment"] as string;
            if (!Guid.TryParse(segment, out var parsedSegmentId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid segment name.");
                return;
            }

            var flagId = context.Request.RouteValues["flagId"] as string;
            if (!Guid.TryParse(flagId, out var parsedFlagId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid flag id.");
                return;
            }

            // Get the cancellation token from the context
            var cancellationToken = context.RequestAborted;

            await _simpleFlagEndpoints.AddSegmentToFeatureFlagAsync(segment, parsedFlagId, cancellationToken);

            context.Response.StatusCode = StatusCodes.Status204NoContent;
        }
        catch (Exception ex)
        {
            // Return ProblemDetails with the exception details
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while adding the segment to the feature flag.",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    #endregion Segment to FeatureFlag

    #region Users to Segment
    /// <summary>
    /// Handles the RemoveSegmentFromFeatureFlagDelegate endpoint by calling the corresponding method in the RemoveSegmentFromFeatureFlagEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task AddUsersToSegmentDelegateAsync(HttpContext context)
    {
        try
        {
            var segment = context.Request.RouteValues["segment"] as string;
            if (string.IsNullOrEmpty(segment))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid segment.");
                return;
            }

            // Deserialize the Users from the request body
            var usersDto = await context.Request.ReadFromJsonAsync<AddUsersToSegmentRequest>();

            if (usersDto == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid users data.");
                return;
            }

            // Get the cancellation token from the context
            var cancellationToken = context.RequestAborted;

            await _simpleFlagEndpoints.AddUsersToSegmentAsync(usersDto, segment, cancellationToken);

            context.Response.StatusCode = StatusCodes.Status204NoContent;
        }
        catch (Exception ex)
        {
            // Return ProblemDetails with the exception details
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while adding the users to the segment.",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    /// <summary>
    /// Handles the RemoveUsersFromSegmentDelegate endpoint by calling the corresponding method in the RemoveUsersFromSegmentEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task RemoveUsersFromSegmentDelegateAsync(HttpContext context)
    {
        try
        {
            var segment = context.Request.RouteValues["segment"] as string;
            if (string.IsNullOrEmpty(segment))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid segment.");
                return;
            }

            // Deserialize the Users from the request body
            var usersDto = await context.Request.ReadFromJsonAsync<RemoveUsersFromSegmentRequest>();

            if (usersDto == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid users data.");
                return;
            }

            // Get the cancellation token from the context
            var cancellationToken = context.RequestAborted;

            await _simpleFlagEndpoints.DeleteUsersFromSegmentAsync(segment, usersDto, cancellationToken);

            context.Response.StatusCode = StatusCodes.Status204NoContent;
        }
        catch (Exception ex)
        {
            // Return ProblemDetails with the exception details
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while removing the users from the segment.",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    /// <summary>
    /// Handles the CleanUsersOnSegmentDelegate endpoint by calling the corresponding method in the CleanUsersOnSegmentEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task CleanUsersOnSegmentDelegateAsync(HttpContext context)
    {
        try
        {
            var segment = context.Request.RouteValues["segment"] as string;
            if (string.IsNullOrEmpty(segment))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid segment.");
                return;
            }

            // Get the cancellation token from the context
            var cancellationToken = context.RequestAborted;

            await _simpleFlagEndpoints.CleanUsersOnSegmentAsync(segment, cancellationToken);

            context.Response.StatusCode = StatusCodes.Status204NoContent;
        }
        catch (Exception ex)
        {
            // Return ProblemDetails with the exception details
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while cleaning the users on the segment.",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
    #endregion Users to Segment

    #region Domain
    /// <summary>
    /// Handles the GetDomainsDelegate endpoint by calling the corresponding method in the GetDomainsEndpoint class.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task GetDomainsDelegateAsync(HttpContext context)
    {
        try
        {
            var cancellationToken = context.RequestAborted;

            var result = await _simpleFlagEndpoints.GetDomainsAsync(cancellationToken);

            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsJsonAsync(result);
        }
        catch (Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while retrieving the domains.",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    #endregion Domain
}
