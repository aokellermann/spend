resource "aws_cognito_user_pool" "users" {
  account_recovery_setting {
    recovery_mechanism {
      name     = "verified_email"
      priority = "1"
    }

    recovery_mechanism {
      name     = "verified_phone_number"
      priority = "2"
    }
  }

  admin_create_user_config {
    allow_admin_create_user_only = "false"
  }

  auto_verified_attributes = ["email"]
  deletion_protection      = "INACTIVE"

  email_configuration {
    email_sending_account = "COGNITO_DEFAULT"
  }

  mfa_configuration = "OFF"
  name              = "users"

  password_policy {
    minimum_length                   = "8"
    require_lowercase                = "false"
    require_numbers                  = "false"
    require_symbols                  = "false"
    require_uppercase                = "false"
    temporary_password_validity_days = "7"
  }

  schema {
    attribute_data_type      = "String"
    developer_only_attribute = "false"
    mutable                  = "true"
    name                     = "email"
    required                 = "true"

    string_attribute_constraints {
      max_length = "2048"
      min_length = "0"
    }
  }

  user_attribute_update_settings {
    attributes_require_verification_before_update = ["email"]
  }

  username_configuration {
    case_sensitive = "false"
  }

  verification_message_template {
    default_email_option = "CONFIRM_WITH_CODE"
    email_message        = "Your verification code is {####}"
    email_subject        = "Your verification code"
  }
}

resource "aws_cognito_user_pool_client" "user_pool_client_web" {
  user_pool_id = aws_cognito_user_pool.users.id

  allowed_oauth_flows                  = ["code"]
  allowed_oauth_flows_user_pool_client = "true"
  allowed_oauth_scopes                 = [
    "aws.cognito.signin.user.admin", "email", "openid", "phone", "profile"
  ]
  auth_session_validity                         = "3"
  callback_urls                                 = ["myapp://"]
  enable_propagate_additional_user_context_data = "false"
  enable_token_revocation                       = "true"
  logout_urls                                   = ["myapp://"]
  name                                          = "client_web"
  supported_identity_providers                  = ["COGNITO", aws_cognito_identity_provider.google_idp.provider_type]

  depends_on = [aws_cognito_identity_provider.google_idp]
}

resource "aws_cognito_user_pool_client" "user_pool_client_app" {
  user_pool_id = aws_cognito_user_pool.users.id

  allowed_oauth_flows                  = ["code"]
  allowed_oauth_flows_user_pool_client = "true"
  allowed_oauth_scopes                 = [
    "aws.cognito.signin.user.admin", "email", "openid", "phone", "profile"
  ]
  callback_urls                                 = ["myapp://"]
  enable_propagate_additional_user_context_data = "false"
  enable_token_revocation                       = "true"
  logout_urls                                   = ["myapp://"]
  name                                          = "client_app"
  refresh_token_validity                        = "30"
  supported_identity_providers                  = ["COGNITO", aws_cognito_identity_provider.google_idp.provider_type]

  depends_on = [aws_cognito_identity_provider.google_idp]
}

resource "aws_cognito_user_pool_client" "user_pool_client_api" {
  user_pool_id = aws_cognito_user_pool.users.id

  allowed_oauth_flows                  = ["code"]
  allowed_oauth_flows_user_pool_client = "true"
  allowed_oauth_scopes                 = [
    "aws.cognito.signin.user.admin", "email", "openid", "phone", "profile"
  ]
  callback_urls                                 = ["myapp://"]
  enable_propagate_additional_user_context_data = "false"
  enable_token_revocation                       = "true"
  logout_urls                                   = ["myapp://"]
  name                                          = "client_api"
  refresh_token_validity                        = "30"
  supported_identity_providers                  = ["COGNITO", aws_cognito_identity_provider.google_idp.provider_type]

  depends_on = [aws_cognito_identity_provider.google_idp]
}

resource "aws_cognito_user_pool_domain" "users_domain" {
  user_pool_id = aws_cognito_user_pool.users.id
  domain       = var.user_pool_domain
}