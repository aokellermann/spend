locals {
  package = "../../../../api/Graph/src/Graph/bin/Release/net6.0/Graph.zip"
}

resource "aws_kms_key_policy" "example" {
  key_id = aws_kms_key.key.id
  policy = jsonencode({
    Id        = "graph_key"
    Statement = [
      {
        Effect    = "Allow"
        Principal = {
          AWS = "*"
        }
        Action   = "kms:*"
        Resource = "*"
      },
    ]
    Version = "2012-10-17"
  })
}


resource "aws_kms_key" "key" {
  description = "KMS key for graph"
}

data "aws_iam_policy_document" "assume_role" {
  statement {
    effect = "Allow"

    principals {
      type        = "Service"
      identifiers = ["lambda.amazonaws.com"]
    }

    actions = ["sts:AssumeRole"]
  }
}

resource "aws_iam_role" "iam_for_lambda" {
  name                = "graph_role"
  assume_role_policy  = data.aws_iam_policy_document.assume_role.json
  managed_policy_arns = [
    "arn:aws:iam::aws:policy/service-role/AWSLambdaBasicExecutionRole",
    "arn:aws:iam::aws:policy/service-role/AWSLambdaVPCAccessExecutionRole"
  ]
  inline_policy {
    name = "graph-parameterstore-read"

    policy = jsonencode({
      Version   = "2012-10-17"
      Statement = [
        {
          Action   = ["kms:Decrypt"]
          Effect   = "Allow"
          Resource = aws_kms_key.key.arn
        },
        {
          Action = [
            "ssm:GetParametersByPath",
            "ssm:GetParameters",
            "ssm:GetParameter"
          ]
          Effect   = "Allow"
          Resource = "*"
        },
      ]
    })
  }
}

data "aws_ssm_parameter" "auth_audience" {
  name = "/api/graph/Auth/Audience"
}

data "aws_ssm_parameter" "auth_authority" {
  name = "/api/graph/Auth/Authority"
}

data "aws_ssm_parameter" "plaid_client_id" {
  name = "/api/graph/Plaid/ClientId"
}

data "aws_ssm_parameter" "plaid_secret" {
  name = "/api/graph/Plaid/Secret"
}

data "aws_ssm_parameter" "mongo_conn" {
  name = "/api/graph/Mongo/VpcConnectionString"
}

data "aws_ssm_parameter" "mongo_password" {
  name = "/api/graph/Mongo/Password"
}

# run dotnet lambda package to create the zip file
resource "aws_lambda_function" "graph_lambda" {
  function_name    = "graph"
  description      = "GraphQL API"
  filename         = local.package
  source_code_hash = filebase64sha256(local.package)
  handler          = "Graph"
  runtime          = "dotnet6"
  memory_size      = "256"
  timeout          = "30"
  vpc_config {
    security_group_ids          = [aws_security_group.graph.id]
    subnet_ids                  = data.aws_subnets.private.ids
    ipv6_allowed_for_dual_stack = false
  }

  role = aws_iam_role.iam_for_lambda.arn

  kms_key_arn = aws_kms_key.key.arn

  environment {
    variables = {
      ASPNETCORE_ENVIRONMENT     = var.env
      Plaid__ClientId            = data.aws_ssm_parameter.plaid_client_id.value
      Plaid__Secret              = data.aws_ssm_parameter.plaid_secret.value
      Auth__Authority            = data.aws_ssm_parameter.auth_authority.value
      Auth__Audience             = data.aws_ssm_parameter.auth_audience.value
      Mongo__VpcConnectionString = data.aws_ssm_parameter.mongo_conn.value
      Mongo__Password            = data.aws_ssm_parameter.mongo_password.value
    }
  }
}

resource "aws_api_gateway_rest_api" "api_gateway" {
  name = "Graph gateway"
}

resource "aws_api_gateway_resource" "proxy" {
  rest_api_id = "${aws_api_gateway_rest_api.api_gateway.id}"
  parent_id   = "${aws_api_gateway_rest_api.api_gateway.root_resource_id}"
  path_part   = "{proxy+}"
}

resource "aws_api_gateway_method" "proxy" {
  rest_api_id   = "${aws_api_gateway_rest_api.api_gateway.id}"
  resource_id   = "${aws_api_gateway_resource.proxy.id}"
  http_method   = "ANY"
  authorization = "NONE"
}

resource "aws_api_gateway_integration" "lambda" {
  rest_api_id = "${aws_api_gateway_rest_api.api_gateway.id}"
  resource_id = "${aws_api_gateway_method.proxy.resource_id}"
  http_method = "${aws_api_gateway_method.proxy.http_method}"

  integration_http_method = "POST"
  type                    = "AWS_PROXY"
  uri                     = "${aws_lambda_function.graph_lambda.invoke_arn}"
}

resource "aws_api_gateway_method" "proxy_root" {
  rest_api_id   = "${aws_api_gateway_rest_api.api_gateway.id}"
  resource_id   = "${aws_api_gateway_rest_api.api_gateway.root_resource_id}"
  http_method   = "ANY"
  authorization = "NONE"
}

resource "aws_api_gateway_integration" "lambda_root" {
  rest_api_id = "${aws_api_gateway_rest_api.api_gateway.id}"
  resource_id = "${aws_api_gateway_method.proxy_root.resource_id}"
  http_method = "${aws_api_gateway_method.proxy_root.http_method}"

  integration_http_method = "POST"
  type                    = "AWS_PROXY"
  uri                     = "${aws_lambda_function.graph_lambda.invoke_arn}"
}

resource "aws_api_gateway_deployment" "gateway_deploy" {
  depends_on = [
    aws_api_gateway_integration.lambda,
    aws_api_gateway_integration.lambda_root,
  ]

  rest_api_id = "${aws_api_gateway_rest_api.api_gateway.id}"
  stage_name  = "api"
}

resource "aws_lambda_permission" "apigw" {
  statement_id  = "AllowAPIGatewayInvoke"
  action        = "lambda:InvokeFunction"
  function_name = "${aws_lambda_function.graph_lambda.function_name}"
  principal     = "apigateway.amazonaws.com"

  # The /*/* portion grants access from any method on any resource
  # within the API Gateway "REST API".
  source_arn = "${aws_api_gateway_rest_api.api_gateway.execution_arn}/*/*"
}