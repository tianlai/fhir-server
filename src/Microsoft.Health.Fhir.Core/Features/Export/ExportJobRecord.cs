// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using EnsureThat;
using Newtonsoft.Json;

namespace Microsoft.Health.Fhir.Core.Features.Export
{
    public class ExportJobRecord
    {
        public ExportJobRecord(Uri requestUri)
        {
            EnsureArg.IsNotNull(requestUri, nameof(requestUri));

            RequestUri = requestUri;
            JobStatus = ExportJobStatus.Queued;
            Id = Guid.NewGuid().ToString();
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; }

        public ExportJobStatus JobStatus { get; }

        public Uri RequestUri { get; }

        public void DummyMethod()
        {
            if (JobStatus == ExportJobStatus.Queued)
            {
                throw new NotImplementedException();
            }
        }
    }
}
