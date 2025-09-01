// <copyright file="TokenPair.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Application.Abstractions.Security;

public record TokenPair(string accessToken, DateTime accessTokenExpiresAtUtc, string refreshToken);