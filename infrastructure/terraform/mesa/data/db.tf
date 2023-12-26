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
resource "aws_db_instance" "spend_db" {
  allocated_storage                     = "20"
  auto_minor_version_upgrade            = "true"
  availability_zone                     = "us-east-1b"
  backup_retention_period               = "1"
  backup_target                         = "region"
  backup_window                         = "09:21-09:51"
  ca_cert_identifier                    = "rds-ca-2019"
  copy_tags_to_snapshot                 = "true"
  customer_owned_ip_enabled             = "false"
  db_name                               = "spenddb"
  deletion_protection                   = "true"
  engine                                = "postgres"
  engine_version                        = "16.1"
  iam_database_authentication_enabled   = "false"
  identifier                            = "spend"
  instance_class                        = "db.t4g.micro"
  iops                                  = "0"
  kms_key_id                            = aws_kms_key.db_key.arn
  license_model                         = "postgresql-license"
  maintenance_window                    = "sat:03:12-sat:03:42"
  max_allocated_storage                 = "1000"
  monitoring_interval                   = "0"
  multi_az                              = "false"
  network_type                          = "IPV4"
  option_group_name                     = "default:postgres-16"
  parameter_group_name                  = "default.postgres16"
  performance_insights_enabled          = "false"
  performance_insights_retention_period = "0"
  port                                  = "5432"
  publicly_accessible                   = "true"
  storage_encrypted                     = "true"
  storage_throughput                    = "0"
  storage_type                          = "gp2"
  username                              = "postgres"
  vpc_security_group_ids                = [aws_security_group.allow_me.id]
  skip_final_snapshot                   = true
  apply_immediately                     = false
}
