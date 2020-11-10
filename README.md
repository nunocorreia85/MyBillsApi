![Build Status](https://github.com/nunocorreia85/MyBillsApi/workflows/BuildTest/badge.svg?branch=master) [![Coverage Status](https://coveralls.io/repos/github/nunocorreia85/MyBillsApi/badge.svg?branch=master)](https://coveralls.io/github/nunocorreia85/MyBillsApi?branch=master) [![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/mikuam/TicketStore/blob/master/LICENSE)

# MyBills API
This is a project showing how to build a Azure Functions API in .Net Core 3.1


Guide manual:
In powershell run the following:

Set environment variable:

- $env:SqlConnectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=mybillsdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"

update database:
- dotnet ef database update -p src\Infrastructure -s src\Api

In case any domein entity is changed please run this command:

add migration InitialCreate:
- dotnet ef migrations add InitialCreate -p src\Infrastructure -s src\Api -o Persistence\Migrations


## Architecture

![diagram concept](https://viewer.diagrams.net/?highlight=FFFFFF&edit=_blank&layers=1&nav=1&title=temp.drawio#R5VjbjtowEP0aHlkRO9weWaCXh0pVV2q3j97EJNY6dtY4EPr1nWA7d9otAlbqwkM8xzNj%2B5xxEmeAl0n%2BUZE0%2FiJDygdoFOYDvBoghOY%2BgkuBHAziedgikWKhxSrggf2iFhxZNGMh3TYctZRcs7QJBlIIGugGRpSS%2B6bbRvLmqCmJaAd4CAjvoj9YqGODztC0wj9RFsVuZG8yNz0Jcc52JduYhHJfg%2FB6gJdKSm1aSb6kvGDP8WLiPpzoLSemqNCvCVjnj4vJi%2Fi2EWyNnpMd%2FT4kQ5tlR3hmF2wnqw%2BOAcgCZINxv4%2BZpg8pCYqePQgOWKwTDpYHTbJNjQIbllMY9H7DOF9KLtUxEV4c%2F4DbManSND%2B5GK%2BkCIqLyoRqdQAXGzAdmwhbVsi39r7SCM8s8XFNnxIkti6iMnVFHTQse%2F%2FAJLohk8vj7zJMev6oQSV2O69GJRr3UFmCF6cSX5PKC1CGRs3qw9Nu9Xmoh7ISvDhlfg9lE66L0pGwqDp3k5dMuo7h9njDXYADQmledUIrKq4rmRAmXC6YmklnOjuqAKW6Sf9WK%2FlMXekKKWirmi1EOIsEmAFIQAG%2FLwRicBte2I6EhSE%2FpbeSmQgLdVejC22KeXNT%2BKhH4d5NcS2Bx1cS%2BLPYKAIqZYHOFH33Qo%2F7tvJNhZ5eSehFyt69umjafbbdVt3Z1dRNOfCsmSxu1iQp6BRP29S4vG%2FVsf%2FWqs9f8UIjwkVxXKlYrQkBC1eHx7rxs6DrbuzMVW7pM9bBWTnTZRi0a1FgVUGFcWgIQMPOsahFP8xeZiqgf38l1kRFVP%2FB74ScNbn61HKYohzqftecbp%2BEdoSvkh13Wu5OBPPmo37SqgKzTBtVP1%2B1Evm4lchrJTI8dBIdK6pc9vlF5ob7r6rsraqidb70sX83Pq8s2uernlRnFwaY1fcD4159hsHr3w%3D%3D)
