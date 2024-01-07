module "vpc" {
  source = "terraform-aws-modules/vpc/aws"

  name = "spend"
  cidr = "10.0.0.0/16"

  azs             = ["us-east-1a"]
  private_subnets = ["10.0.1.0/24"]
  public_subnets  = ["10.0.101.0/24"]

  enable_nat_gateway = false
  enable_vpn_gateway = false
}

resource "aws_security_group" "nat" {
  name   = "nat"
  vpc_id = module.vpc.vpc_id

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

module "fck-nat" {
  source = "RaJiska/fck-nat/aws"

  name                 = "spend-nat"
  vpc_id               = module.vpc.vpc_id
  subnet_id            = "${element(module.vpc.public_subnets, 0)}"
  update_route_table   = true
  route_table_id       = module.vpc.private_route_table_ids[0]
  use_cloudwatch_agent = true
}