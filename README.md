# Checkout.com: Payment Gateway

[![Build status](https://ci.appveyor.com/api/projects/status/tb6wkrtxn850livm?svg=true)](https://ci.appveyor.com/project/dittu/copaymentgateway)
[![Quality gate](https://sonarcloud.io/api/project_badges/quality_gate?project=dittu_COPaymentGateWay)](https://sonarcloud.io/dashboard?id=dittu_COPaymentGateWay)

### Prerequisites

### AWS 
- IAM User with DynamoDB Read,Write and S3 Put permissions

#### Dynamo Table Details

``` 
TableName: Payments
HashKey Name: Identifier
HashKey Type: ScalarAttributeType.S
```

## Initialisation
- Clone repository
- Run WebApi Project
- Api Documentation available via Swagger at https://localhost:4535/swagger/index.html

## Implementation Details

### Supported Card Types
- Visa
- Master

### UnitTest
- NUnit

### Logging
- Serilog To S3Buckets

### Application Metrics
- AppMetric (Promethous, Grafana)

### Authentication
- JwtBearer (Expires March 2022)
```
eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxIiwiaWF0IjoxNjE0NTg2Mjk4LCJleHAiOjE2NDYxMjIyOTgsImF1ZCI6IlVzZXIiLCJzdWIiOiJqcm9ja2V0QGV4YW1wbGUuY29tIiwiR2l2ZW5OYW1lIjoiQWRpdHlhIiwiU3VybmFtZSI6IkFyaXNldHR5IiwiUm9sZSI6IkFkbWluIiwiTWVyY2hhbnRJZCI6IkhPaGxSZmRPcjUifQ.5vWk9u624SHgBo8IPWzlGgs3EEDEE1aXWVQzThgQsAw
```

### Datastorage
- DynamoDB

