# spend

Spend is a personal finance tracker app intended to compete with services like Intuit Mint.

The goal is to be legible, high compatibility, cheap, and lightweight. 

## App

As it is based on [flutter](https://flutter.dev/), the clientside application supports all
platforms. 

The app is very much so under construction. It was copied from
[EnergyInfoSwiss](https://github.com/SFOE/EnergyInfoSwiss-MobileApp) since their architecture
is pretty similar to what we are aiming for. Most of what you see in the app is still 
EnergyInfoSwiss functionality, which will slowly be replaced.

Current app features:
1. Auth using [AWS Cognito](https://aws.amazon.com/cognito/)
2. Linking financial accounts using [Plaid Link](https://plaid.com/docs/link/)

## API

Most of this service's functionality will be querying transactions in different ways.
Spend exposes a GraphQL API to enable flexibility.

Features:
1. Auth using [AWS Cognito](https://aws.amazon.com/cognito/)
2. Linking financial accounts using [Plaid Link](https://plaid.com/docs/link/)
3. Syncing transactions through Plaid
4. Automatic and manual transaction categorization
5. GraphQL API built with [Hot Chocolate](https://github.com/ChilliCream/graphql-platform)
6. Plaid API exposed in GraphQL schema
7. Database access through MongoDB

GraphQL schema can be pulled from https://iclhluo2w9.execute-api.us-east-1.amazonaws.com/api/graphql/sdl

## DB

Everything is currently persisted in MongoDB using Atlas' serverless cluster, which is
nearly free. Ideally nearly everything but the raw Plaid transaction would be stored in
Postgres, but I don't want to pay for a dedicated server at the moment. 

## Infrastucture

All infrastructure is built using terraform modules. Ideally we would have ansible
playbooks to augment terraform, but I'm trying to avoid premature over-automation.

We use S3 as a terraform backend.

Features:
1. Auth using [AWS Cognito](https://aws.amazon.com/cognito/)
2. VPC
3. NAT gateway using [fck-nat](https://github.com/AndrewGuenther/fck-nat) which is 10x cheaper than standard NAT gateway
4. MongoDB Atlas serverless
5. PrivateLink (VPC endpoint) connection to AWS
6. GraphQL API deployed on lambda with API gateway

# Contributing

See the [contributing guide](CONTRIBUTING.md).