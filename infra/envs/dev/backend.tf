terraform {
  backend "s3" {
    bucket         = "luminosec-terraform-state-033855898717"
    key            = "envs/dev/terraform.tfstate"
    region         = "eu-central-1"
    dynamodb_table = "luminosec-terraform-locks"
    encrypt        = true
  }
}
