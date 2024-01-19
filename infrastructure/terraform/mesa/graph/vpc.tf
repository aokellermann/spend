data "aws_vpc" "spend" {
  tags = {
    Name   = "spend"
  }
}

data "aws_subnets" "private" {
  filter {
    name   = "tag:Name"
    values = ["spend-private-us-east-1a"]
  }
}

resource "aws_security_group" "graph" {
  name   = "graph"
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