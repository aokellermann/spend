resource "aws_cognito_identity_provider" "google_idp" {
  user_pool_id  = aws_cognito_user_pool.users.id
  provider_name = "Google"
  provider_type = "Google"

  provider_details = {
    authorize_scopes = "profile email"
    client_id        = data.aws_ssm_parameter.google_idp_client_id.value
    client_secret    = data.aws_ssm_parameter.google_idp_client_secret.value

    attributes_url                = "https://people.googleapis.com/v1/people/me?personFields="
    attributes_url_add_attributes = true
    authorize_url                 = "https://accounts.google.com/o/oauth2/v2/auth"
    oidc_issuer                   = "https://accounts.google.com"
    token_request_method          = "POST"
    token_url                     = "https://www.googleapis.com/oauth2/v4/token"
  }

  attribute_mapping = {
    username = "sub"
    email    = "email"
  }
}
