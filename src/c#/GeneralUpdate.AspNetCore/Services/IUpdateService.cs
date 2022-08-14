﻿using GeneralUpdate.Core.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeneralUpdate.AspNetCore.Services
{
    public interface IUpdateService
    {

        /// <summary>
        /// Verify whether the current version of the client needs to be updated.
        /// </summary>
        /// <param name="clientType">1:ClientApp 2:UpdateApp</param>
        /// <param name="clientVersion">Current version of the client</param>
        /// <param name="serverLastVersion">The latest version of the server.</param>
        /// <param name="clientAppkey">The appkey agreed by the client and server.</param>
        /// <param name="appSecretKey">Appkey is stored in the database.</param>
        /// <param name="isForce">Whether to force all versions to be updated.</param>
        /// <param name="getUrlsAction"></param>
        /// <returns>Json object.</returns>
        Task<string> UpdateValidateTaskAsync(int clientType, string clientVersion, string serverLastVersion, string clientAppkey,string appSecretKey, bool isForce, Func<int, string, Task<List<VersionDTO>>> getUrlsAction);
    }
}