# Frends.Community.SnowflakeData

frends Community Task for Retriving data from [Snowflake](https://www.snowflake.com/)

[![Actions Status](https://github.com/CommunityHiQ/Frends.Community.SnowflakeData/workflows/PackAndPushAfterMerge/badge.svg)](https://github.com/CommunityHiQ/Frends.Community.SnowflakeData/actions) ![MyGet](https://img.shields.io/myget/frends-community/v/Frends.Community.SnowflakeData) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) 

- [Installing](#installing)
- [Tasks](#tasks)
     - [Echo](#Echo)
- [Building](#building)
- [Contributing](#contributing)
- [Change Log](#change-log)

# Installing

You can install the Task via frends UI Task View or you can find the NuGet package from the following NuGet feed
https://www.myget.org/F/frends-community/api/v3/index.json and in Gallery view in MyGet https://www.myget.org/feed/frends-community/package/nuget/Frends.Community.SnowflakeData

# Tasks

## ExecuteProcedure

Execute a StoredProcedure

### Properties

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| Execute | `string` | Call a stored procedure | `CALL sv_proc1(:arg1, 127.4);` -- obs. snowflake expect parameters to be in form `:arg` in Query |
| Parameters | `Array` | Parameters for the procedure | { Name = "arg1" , Value = #var.arg1 } |
| Connection String | `Secret` | Connection string for the Snowflake instace | 

### Options

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| Command Timeout Seconds | `int` | Timeout | `300` |
| IsolationLevel | `System.Data.IsolationLevel` | Isolation level ( can be empty , default empty) | `` |

### Returns

A result object with parameters.

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| [result] | `JToken` | an JArray of JTokens if there is only one resultset or JArray of JArrays of JTokens if there are multiple resultsets | `[
	{
		"NRO": "010",
		"KOODI": null
	},
	{
		"NRO": "011",
		"KOODI": null
	}
	
]` |

Usage:
To fetch result use syntax:

`#result`


## ExecuteQuery

Execute a Query

### Properties

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| Query | `string` | Query string | `SELECT * FROM table where arg1 = :arg1;` -- obs. snowflake expect parameters to be in form `:arg` in Query |
| Parameters | `Array` | Parameters for the procedure | { Name = "arg1" , Value = #var.arg1 } |
| Connection String | `Secret` | Connection string for the Snowflake instace | 

### Options

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| Command Timeout Seconds | `int` | Timeout | `300` |
| IsolationLevel | `System.Data.IsolationLevel` | Isolation level ( can be empty , default empty) | `` |

### Returns

A result object with parameters.

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| [result] | `JToken` | an JArray of JTokens if there is only one resultset or JArray of JArrays of JTokens if there are multiple resultsets | `[
	{
		"NRO": "010",
		"KOODI": null
	},
	{
		"NRO": "011",
		"KOODI": null
	}
	
]` |

Usage:
To fetch result use syntax:

`#result`


# Building

Clone a copy of the repository

`git clone https://github.com/CommunityHiQ/Frends.Community.SnowflakeData.git`

Rebuild the project

`dotnet build`

Run tests

`dotnet test`

Create a NuGet package

`dotnet pack --configuration Release`

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repository on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

# Change Log

| Version | Changes |
| ------- | ------- |
| 0.0.1   | Development still going on |
| 0.0.2   | Documentation changes |
| 0.0.3   | upgraded Snowflake.Data package to 1.2.9 |
| 0.2.0   | Targeted to .NET Standard 2.0, .NET Framework 4.7.1, .NET6 and .NET8, and updated System.ComponentModel.Annotations to 5.0.0 |
