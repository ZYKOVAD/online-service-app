﻿using Microsoft.IdentityModel.Tokens;
using online_service_app_auth.db_layer;
using online_service_app_auth.models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace online_service_app_auth
{
    public class jwtProvider
    {
        public string GenerateToken(IUser user)
        {
            var privateKey = @"-----BEGIN RSA PRIVATE KEY-----
MIIEnwIBAAKCAQBHpw5FPR+2hizdJ+Xu1es7h1inFu7L9FmHoQF81XPYDJIIKnl4
DPpZPVm6yNs+G5qNVDEquXKsqcguihPg45OO5dONdra5uxTlXfbz8+u7nNiYvLYG
mrEOEIJFyHRv8hIzxhDzDbkheFTGTSf7gxmwaElDK394WA46QlvdRPGoo8Ohc4ss
CFPbNXqhN9G6daAFex6eyoEkZEJrUsJckDMQi8gCItKJdyETcOCSVUF5nU2jB2Js
HBcQ917G6ZFxl/DqGvpqzTkjFsWbcPJ7VXQ/YTOhWNu9arXAWENz1j5IQyby0h9U
FrZJ/tXs+t1jRdRPH2ocboJz1JinXbRuuPbVAgMBAAECggEAOM+RlwBBtrxnsenC
ez2NRyOm/MRIDdntaoYT4eBz+ybsWiEjMjZIAJ2OGXTLsFC+l7PbT969t3zeRVgh
LrwB8Nj0hUNM2bIlfT6lYQNTMuvHKELnqBDatSYNxDqNZ3ztRbOTGMMWuBzqSlg+
jUKDCcWF4QYKCWb30OVwZN4XJPfsRmCCVt/UeGGavTrWgGasNLdXf11Bp1KDKkSM
xJmZXrviL4m4Gaci2WRNw1XjHw2SdoJHy5VyCd33s8e7iagOM0kGNYUNp8X+tehA
ApRzeI37ON1r/8t/y8BRyldHQq+YYjPq8GJ6skoZ7GFvvyKVUikl8dGlbY7QHC4J
tbpQwQKBgQCOgnTH03zGCn8XcvunsF3hVezR6b8EXF4a38nWuiLwhfkt4eM56DNC
WwLPgcWS9XShYIzXFWPdCHRBLwIAx5fkXsfZ6ci4H57lFHOxYsGMjGf5yHl9HGQw
tcTm6MAQq7Ru8UCYoribSkmI6gIWkkY1/XDTG6mYPjhwg/CbueXKKQKBgQCAtuuQ
sVzIh8cNxIK3LsBVMFxzUauKUScAzsQECRtW23ZoNjPBF0XMfTcod9sOv9PO7uii
Yuv8/DmaEnGTFMXsvajS9xqUXIjSi75j6eZvv9CuNgrtVEKtKvkPavgjIzqa8xen
e7qS6Q+vQczH4LQAZUw2pGzypdaoZQTq46/0zQJ/Q+nnvVMMI1TdPE+PnlpuYvSP
53l9jpZ2Qlhj1/DctcMi/Lqpju8ZfoXg0QMHJQHUkxNwCxAmhy+AN5AvZC+6Mxr4
IuFGxnhLQOB+QgrOMiy3YFIpk3X0SUJWk7RX05oAYt6V6ieh0l+Uq6rZU+hhjVk6
a2DDcco1G2nFQHFXEQKBgCt5nWdqAOTat6k2ztAAwoYDp7Zrx30YnAvxytNyIAHe
bsgURrbZnYHMaW0JzrvUnz4uA/WVZBA3QV2BDUuYoKGuDe/z9s5V0wEKomws/OCR
8XJmXpp682p6MVW708RKiej8Yuj3KbWlct7HYtvAUgwwKuXp88KhsTd/p4GPFkeh
AoGACQXFZjZF/ES/rT5wFEyVgzyYfaD8xw+6u1wxVLnunqR4hNoZSHCA4l5ERFhg
ojufMnr3EMoY0jwmpOjslCbQQeLKkYSLdsWWX9gofbic3E5m1dsKqbAzYu1P4XxD
T9Ons4owZUdFx0sb8qJtC8xgV6sshfIhuoVIK5bgTaF2gkk=
-----END RSA PRIVATE KEY-----";

            using var rsa = RSA.Create();
            rsa.ImportFromPem(privateKey);

            Claim[] claims =
                [
                    new Claim("Id", user.Id.ToString()),
                    new Claim("typeUser", user.GetType().ToString())
                ];

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
            );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
