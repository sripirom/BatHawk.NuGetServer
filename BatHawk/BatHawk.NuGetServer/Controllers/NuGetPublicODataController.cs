// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 
using CommonServiceLocator;
using NuGet.Server.Core.Infrastructure;
using NuGet.Server.V2.Controllers;

namespace BatHawk.NuGetServer.Controllers
{
    /// <summary>
    /// Controller that exposes Program.NuGetPublicRepository as NuGet OData feed
    /// that allows unauthenticated read/download access.
    /// Delete/upload requires user supplied ApiKey to match Program.ApiKey.
    /// </summary>
    public class NuGetPublicODataController : NuGetODataController
    {
        public NuGetPublicODataController(IPackageAuthenticationService packageAuthenticationService)
            : base(ServiceLocator.Current.GetInstance<IServerPackageRepository>("NuGetPublic"), packageAuthenticationService)
        {
           
        }
    }
}
