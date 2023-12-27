terraform {
  backend "s3" {}
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
    mongodbatlas = {
      source = "mongodb/mongodbatlas"
      version = "~> 1.0"
    }
  }
}

provider "aws" {
  region = "us-east-1"
}

provider "mongodbatlas" {
  public_key = var.mongo_key_public
  private_key = var.mongo_key_private
}