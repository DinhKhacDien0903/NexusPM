// <copyright file="AppPolicies.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Application.Abstractions.Authorization
{
    /// <summary>
    /// Defines the application policies used for authorization.
    /// </summary>
    public static class AppPolicies
    {
        /// <summary>
        /// Policy for tenant owners.
        /// </summary>
        public const string TenantOwner = "TenantOwner";

        /// <summary>
        /// Policy for tenant administrators.
        /// </summary>
        public const string TenantAdmin = "TenantAdmin";

        /// <summary>
        /// Policy for tenant members.
        /// </summary>
        public const string TenantMember = "TenantMember";

        /// <summary>
        /// Policy for tenant billing.
        /// </summary>
        public const string TenantBilling = "TenantBilling";
    }
}