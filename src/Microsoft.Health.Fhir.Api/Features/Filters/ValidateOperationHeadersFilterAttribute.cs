﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using EnsureThat;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Health.Fhir.Core.Features.Validation;
using Microsoft.Net.Http.Headers;

namespace Microsoft.Health.Fhir.Api.Features.Filters
{
    /// <summary>
    /// A filter that validates the Accept and Prefer headers in the request and short-circuits the pipeline if they are invalid.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    internal class ValidateOperationHeadersFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            EnsureArg.IsNotNull(context, nameof(context));

            if (!context.HttpContext.Request.Headers.TryGetValue(HeaderNames.Accept, out var acceptHeaderValue) ||
                acceptHeaderValue.Count != 1 ||
                !string.Equals(acceptHeaderValue[0], "application/fhir+json", StringComparison.Ordinal))
            {
                var error = new OperationOutcome.IssueComponent()
                {
                    Severity = OperationOutcome.IssueSeverity.Error,
                    Code = OperationOutcome.IssueType.Invalid,
                    Diagnostics = Resources.UnsupportedAcceptHeader,
                };

                throw new ResourceNotValidException(new OperationOutcome.IssueComponent[] { error });
            }

            if (!context.HttpContext.Request.Headers.TryGetValue("Prefer", out var preferHeaderValue) ||
                preferHeaderValue.Count != 1 ||
                !string.Equals(preferHeaderValue[0], "respond-async", StringComparison.Ordinal))
            {
                var error = new OperationOutcome.IssueComponent()
                {
                    Severity = OperationOutcome.IssueSeverity.Error,
                    Code = OperationOutcome.IssueType.Invalid,
                    Diagnostics = Resources.UnsupportedPreferHeader,
                };

                throw new ResourceNotValidException(new OperationOutcome.IssueComponent[] { error });
            }
        }
    }
}
