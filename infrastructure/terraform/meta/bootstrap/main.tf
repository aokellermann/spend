module "bootstrap" {
  source = "trussworks/bootstrap/aws"

  region        = "us-east-1"
  account_alias = var.account_alias
}