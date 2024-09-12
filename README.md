# Mortein API

This API is deployed to AWS using the `dotnet lambda` tool which provisions most of the required
infrastructure, including an API gateway and Lambda function; however, it does not create the S3
bucket in which it stores the application code. This bucket must already be provisioned with the
name used in the [Deploy API](./.github/workflows/deploy-api.yaml) workflow.
