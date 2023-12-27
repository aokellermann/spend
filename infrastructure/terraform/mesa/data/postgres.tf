resource "aws_security_group" "allow_me" {
  name   = "allow_me"
  description = "Allow my IP address"

  ingress {
    description = "My IP Address"
    from_port   = "5432"
    to_port     = "5432"
    protocol    = "tcp"
    cidr_blocks = ["198.55.239.10/32"]
  }
}
