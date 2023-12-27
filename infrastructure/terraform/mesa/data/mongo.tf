resource "mongodbatlas_project" "project" {
  name = "spend-dev"
  org_id = var.mongo_org
}

resource "mongodbatlas_cluster" "mongo" {
  project_id   = var.mongo_project
  name         = "spend-dev"

  provider_name               = "TENANT"
  provider_instance_size_name = "M0"
}

resource "mongodbatlas_project_ip_access_list" "allow_me" {
  project_id = var.mongo_project

  ip_address = "198.55.239.10"
}

# todo: aws sg

#resource "mongodbatlas_project_ip_access_list" "allow_me_aws" {
#  project_id = var.mongo_project
#
#  aws_security_group = aws_security_group.allow_me.id
#}
