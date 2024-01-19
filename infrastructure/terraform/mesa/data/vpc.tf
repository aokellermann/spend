data "aws_vpc" "spend" {
  tags = {
    Name   = "spend"
  }
}

data "aws_subnets" "subnets" {
  filter {
    name   = "tag:Name"
    values = ["spend-private-us-east-1a"]
  }
}

resource "aws_security_group" "mongo_endpoint" {
  name   = "mongo"
  vpc_id = data.aws_vpc.spend.id

  ingress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = [
      "0.0.0.0/0"
    ]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = [
      "0.0.0.0/0"
    ]
  }
}

resource "aws_vpc_endpoint" "mongo" {
  vpc_id             = data.aws_vpc.spend.id
  service_name       = mongodbatlas_privatelink_endpoint_serverless.spend.endpoint_service_name
  vpc_endpoint_type  = "Interface"
  subnet_ids         = data.aws_subnets.subnets.ids
  security_group_ids = [aws_security_group.mongo_endpoint.id]
}
