// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Net;
using Hl7.Fhir.Model;
using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.Health.Fhir.Api.Controllers;
using Microsoft.Health.Fhir.Api.Features.ActionResults;
using Microsoft.Health.Fhir.Core.Configs;
using Microsoft.Health.Fhir.Core.Exceptions;
using Microsoft.Health.Fhir.Core.Features.Context;
using NSubstitute;
using Xunit;

namespace Microsoft.Health.Fhir.Api.UnitTests.Controllers
{
    public class ExportControllerTests
    {
        private ExportController _exportEnabledController;

        public ExportControllerTests()
        {
            _exportEnabledController = GetController(new ExportConfiguration() { Enabled = true });
        }

        [Fact]
        public async void GivenAnExportRequest_WhenDisabled_ThenBadRequestResponseShouldBeReturned()
        {
            var exportController = GetController(new ExportConfiguration() { Enabled = false });

            await Assert.ThrowsAsync<RequestNotValidException>(() => exportController.Export());
        }

        [Fact]
        public async void GivenAnExportRequest_WhenEnabled_ThenNotImplementedResponseShouldBeReturned()
        {
            var result = await _exportEnabledController.Export() as FhirResult;

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.NotImplemented, result.StatusCode);
        }

        [Fact]
        public void GivenAnExportByResourceTypeRequest_WhenResourceTypeIsNotPatient_ThenBadRequestResponseShouldBeReturned()
        {
            Assert.Throws<RequestNotValidException>(() => _exportEnabledController.ExportResourceType("Observation"));
        }

        [Fact]
        public void GivenAnExportByResourceTypeRequest_WhenResourceTypeIsPatient_ThenNotImplementedResponseShouldBeReturned()
        {
            var result = _exportEnabledController.ExportResourceType(ResourceType.Patient.ToString()) as FhirResult;

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.NotImplemented, result.StatusCode);
        }

        [Fact]
        public void GivenAnExportResourceTypeIdRequest_WhenResourceTypeIsNotGroup_ThenBadRequestResponseShouldBeReturned()
        {
            Assert.Throws<RequestNotValidException>(() => _exportEnabledController.ExportResourceTypeById("Patient", "id"));
        }

        [Fact]
        public void GivenAnExportByResourceTypeIdRequest_WhenResourceTypeIsGroup_ThenNotImplementedResponseShouldBeReturned()
        {
            var result = _exportEnabledController.ExportResourceTypeById(ResourceType.Group.ToString(), "id") as FhirResult;

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.NotImplemented, result.StatusCode);
        }

        private ExportController GetController(ExportConfiguration exportConfig)
        {
            var operationConfig = new OperationsConfiguration()
            {
                Export = exportConfig,
            };

            IOptions<OperationsConfiguration> optionsOperationConfiguration = Substitute.For<IOptions<OperationsConfiguration>>();
            optionsOperationConfiguration.Value.Returns(operationConfig);

            return new ExportController(
                Substitute.For<IMediator>(),
                Substitute.For<IFhirRequestContextAccessor>(),
                optionsOperationConfiguration,
                NullLogger<ExportController>.Instance);
        }
    }
}
