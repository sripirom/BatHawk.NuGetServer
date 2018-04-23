﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 
using System.Web.Http;
using NuGet.Server.V2.Controllers;
using NuGet.Server.Core.Infrastructure;
using CommonServiceLocator;

namespace BatHawk.NuGetServer.Controllers
{
    /// <summary>
    /// Controller that exposes Program.NuGetPrivateRepository as NuGet OData feed
    /// that allows read/download access for all authenticated users.
    /// delete/upload is not allowed (no authenticationservice is passed to NuGetODataControllers constructor)
    /// </summary>
    [Authorize]
    public class NuGetPrivateODataController : NuGetODataController
    {
        public NuGetPrivateODataController()
            : base(ServiceLocator.Current.GetInstance<IServerPackageRepository>("NuGetPrivate"))
            // Replace line above with the one below to allow upload/delete for all authenticated users
            // : base(Program.NuGetPrivateRepository, new ApiKeyPackageAuthenticationService(false, null))
        {
        }
    }
}
       