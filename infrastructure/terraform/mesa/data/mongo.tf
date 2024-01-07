data "mongodbatlas_organizations" "all" {
  page_num       = 1
  items_per_page = 1
}

data "mongodbatlas_organization" "spend" {
  org_id = data.mongodbatlas_organizations.all.results[0].id
}

resource "mongodbatlas_project" "project" {
  name   = "spend-dev"
  org_id = data.mongodbatlas_organization.spend.org_id
}

resource "mongodbatlas_cluster" "mongo" {
  project_id = mongodbatlas_project.project.id
  name       = "spend-dev"

  provider_name               = "TENANT"
  provider_instance_size_name = "M0"
}

resource "mongodbatlas_serverless_instance" "spend" {
  project_id = mongodbatlas_project.project.id
  name       = "spend-dev-serverless"

  provider_settings_backing_provider_name = "AWS"
  provider_settings_provider_name         = "SERVERLESS"
  provider_settings_region_name           = "US_EAST_1"
}

resource "mongodbatlas_privatelink_endpoint_serverless" "spend" {
  project_id    = mongodbatlas_project.project.id
  instance_name = mongodbatlas_serverless_instance.spend.name
  provider_name = "AWS"
}


resource "mongodbatlas_privatelink_endpoint_service_serverless" "spend" {
  project_id                 = mongodbatlas_project.project.id
  instance_name              = mongodbatlas_serverless_instance.spend.name
  endpoint_id                = mongodbatlas_privatelink_endpoint_serverless.spend.endpoint_id
  cloud_provider_endpoint_id = aws_vpc_endpoint.mongo.id
  provider_name              = mongodbatlas_privatelink_endpoint_serverless.spend.provider_name
  comment                    = "create"
}

resource "mongodbatlas_project_ip_access_list" "allow_me" {
  project_id = mongodbatlas_project.project.id

  ip_address = "209.6.125.244"
}

resource "mongodbatlas_project_ip_access_list" "allow_nj" {
  project_id = mongodbatlas_project.project.id

  ip_address = "66.30.234.130"
}

# todo: aws sg

#resource "mongodbatlas_project_ip_access_list" "allow_me_aws" {
#  project_id = var.mongo_project
#
#  aws_security_group = aws_security_group.allow_me.id
#}
