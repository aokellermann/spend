resource "aws_kms_key" "parameter_store_key" {

}

resource "aws_kms_alias" "parameter_store_key_alias" {
  name          = "alias/parameter_store_key"
  target_key_id = aws_kms_key.parameter_store_key.key_id
}
