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
            EnsureArg.IsNotNull(errors);
            EnsureArg.IsNotNull(results);

            Errors = errors;
            Results = results;
        }

        public List<ExportJobOutputResult> Errors { get; }

        public List<ExportJobOutputResult> Results { get; }
    }
}
