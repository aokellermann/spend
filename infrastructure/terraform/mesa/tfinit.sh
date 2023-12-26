#!/usr/bin/env bash

mod=$1

terraform -chdir="$mod" init -backend-config="../../meta/config.s3.tfbackend" -backend-config="key=$mod/terraform.tfstate"