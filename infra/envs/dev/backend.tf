terraform {
  backend "s3" {
    # Replace with the state_bucket_name output from infra/bootstrap
    bucket         = "luminosec-terraform-state-REPLACE_WITH_ACCOUNT_ID"
    key            = "envs/dev/terraform.tfstate"
    region         = "eu-central-1"
    dynamodb_table = "luminosec-terraform-locks"
    encrypt        = true
  }
}
