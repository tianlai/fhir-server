// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using EnsureThat;
using Microsoft.Health.Fhir.Core.Messages.Export;
using Newtonsoft.Json;

namespace Microsoft.Health.Fhir.Core.Features.Export
{
    public class ExportJobRecord
    {
        private const string PartitionKey = "ExportJob";

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

        public DateTimeOffset QueuedTimeStamp { get; }

        public DateTimeOffset LastModified { get; }

        public DateTimeOffset StartTimeStamp { get; }

        public int NumberOfConsecutiveFailures { get; }

        public int TotalNumberOfFailures { get; }

        public ExportRequest Request { get; }

        public ExportJobProgress Progress { get; }

        public ExportJobOutput Output { get; }

        public void DummyMethod()
        {
            if (JobStatus == ExportJobStatus.Queued)
            {
                throw new NotImplementedException();
            }
        }
    }
}
