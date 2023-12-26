data "aws_ssm_parameter" "google_idp_client_id" {
  name = "/google_idp/client_id"
}

data "aws_ssm_parameter" "google_idp_client_secret" {
  name = "/google_idp/client_secret"
}
