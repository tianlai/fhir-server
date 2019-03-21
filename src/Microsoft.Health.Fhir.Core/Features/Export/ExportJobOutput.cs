// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using EnsureThat;

namespace Microsoft.Health.Fhir.Core.Features.Export
{
    public class ExportJobOutput
    {
        public ExportJobOutput()
            : this(new List<ExportJobOutputResult>(), new List<ExportJobOutputResult>())
        {
        }

        public ExportJobOutput(List<ExportJobOutputResult> errors, List<ExportJobOutputResult> results)
        {
            EnsureArg.IsNotNull(errors, nameof(errors));
            EnsureArg.IsNotNull(results, nameof(results));

            Errors = errors;
            Results = results;
        }

        public List<ExportJobOutputResult> Errors { get; }

        public List<ExportJobOutputResult> Results { get; }

        public void AddError(ExportJobOutputResult error)
        {
            EnsureArg.IsNotNull(error, nameof(error));

            Errors.Add(error);
        }

        public void AddResult(ExportJobOutputResult result)
        {
            EnsureArg.IsNotNull(result, nameof(result));

            Results.Add(result);
        }
    }
}
