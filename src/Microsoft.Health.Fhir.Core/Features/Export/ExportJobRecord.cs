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
        public ExportJobRecord(ExportRequest exportRequest)
        {
            EnsureArg.IsNotNull(exportRequest, nameof(exportRequest));

            Request = exportRequest;

            JobStatus = ExportJobStatus.Queued;
            Id = Guid.NewGuid().ToString();
            Progress = new ExportJobProgress("query", 1);
            Output = new ExportJobOutput();
        }

        [JsonProperty("id")]
        public string Id { get; }

        public ExportJobStatus JobStatus { get; }

        public Uri RequestUri { get; }

        public DateTimeOffset QueuedTimeStamp { get; }

        public DateTimeOffset LastModified { get; }

        public DateTimeOffset StartTimeStamp { get; }

        public DateTimeOffset EndTimeStamp { get; }

        public int NumberOfConsecutiveFailures { get; }

        public int TotalNumberOfFailures { get; }

        [JsonProperty("partitionKey")]
        public string PartitionKey { get; } = "ExportJob";

        public ExportRequest Request { get; }

        public ExportJobProgress Progress { get; private set; }

        public ExportJobOutput Output { get; }

        public void UpdateJobProgress(ExportJobProgress progress)
        {
            EnsureArg.IsNotNullOrEmpty(progress?.Query, nameof(progress));

            Progress = progress;
        }
    }
}
