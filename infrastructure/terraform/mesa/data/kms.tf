resource "aws_kms_key" "db_key" {

}

resource "aws_kms_alias" "db_key_alias" {
  name          = "alias/db_key"
  target_key_id = aws_kms_key.db_key.key_id
}
