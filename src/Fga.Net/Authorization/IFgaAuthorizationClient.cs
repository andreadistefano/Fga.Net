﻿#region License
/*
   Copyright 2021-2022 Hawxy

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */
#endregion

namespace Fga.Net.Authorization;

/// <summary>
/// The FGA Authorization Client
/// </summary>
public interface IFgaAuthorizationClient : IDisposable
{
    /// <summary>
    /// Return all the IDs of the authorization models for a certain store.
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="continuationToken"></param>
    /// <param name="ct"></param>
    /// <returns>Contain an array of all authorization model IDs, sorted in descending order of creation.</returns>
    Task<AuthorizationModelsResponse?> GetAuthorizationModelsAsync(int? pageSize, string? continuationToken, CancellationToken ct = default);


    /// <summary>
    /// The check API will return whether the user has a certain relationship with an object in a certain store.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<CheckTupleResponse?> CheckAsync(CheckTupleRequest request, CancellationToken ct = default);

    /// <summary>
    /// The POST read API will return the tuples for a certain store that matches a query filter specified in the body. Tuples and type definitions allow Auth0 FGA to determine whether a relationship exists between an object and an user.
    /// In the body: <example><code>Object is mandatory. An object can be a full object (e.g., type:object_id) or type only (e.g., type:).</code></example>
    /// <example><code>User is mandatory in the case the object is type only.</code></example>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<ReadTupleResponse?> ReadAsync(ReadTupleRequest request, CancellationToken ct = default);

    /// <summary>
    /// The POST write API will update the tuples for a certain store. Tuples and type definitions allow Auth0 FGA to determine whether a relationship exists between an object and an user.
    /// Path parameter store_id is required.In the body, writes adds new tuples while deletes remove existing tuples.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task WriteAsync(WriteTupleRequest request, CancellationToken ct = default);

    /// <summary>
    /// Returns the store's settings, including environment tag and an array of Auth0 FGA's allowed 3rd party token issuers.
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<StoreSettingsResponse?> GetStoreSettingsAsync(CancellationToken ct = default);

    /// <summary>
    /// Updates the environment tag to differentiate between development, staging, and production environments.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task PatchStoreSettingsAsync(UpdateStoreSettingsRequest request, CancellationToken ct = default);

    /// <summary>
    /// Configure the system so that tokens issued by the specified 3rd party token issuer will be allowed for Auth0 FGA's read and write operations. 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<AddTokenIssuerResponse?> AddTokenIssuersAsync(AddTokenIssuersRequest request, CancellationToken ct = default);

    /// <summary>
    /// Remove the 3rd party token issuer as a token issuer that are allowed by Auth0 FGA. 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task DeleteTokenIssuerAsync(string id, CancellationToken ct = default);
}