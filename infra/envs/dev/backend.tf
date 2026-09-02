terraform {
  backend "s3" {
    bucket       = "luminosec-terraform-state-033855898717-eu-central-1-an"
    key          = "envs/dev/terraform.tfstate"
    region       = "eu-central-1"
    use_lockfile = true
    encrypt      = true
  }
}
